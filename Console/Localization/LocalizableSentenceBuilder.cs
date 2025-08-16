using CommandLine;
using CommandLine.Text;
using Console.Resources;

// ReSharper disable All
// This came from the CommandLine Parser codebase and was minimally changed to work with my project structure.
// All inspection problems are going to be ignored as it wasn't my code originally.
// https://github.com/commandlineparser/commandline/blob/master/demo/ReadText.LocalizedDemo/LocalizableSentenceBuilder.cs

namespace Console.Localization
{
    public sealed class LocalizableSentenceBuilder : SentenceBuilder
    {
        public override Func<string> RequiredWord
        {
            get { return () => CommandLineParser.SentenceRequiredWord; }
        }

        public override Func<string> OptionGroupWord
        {
            get { return () => CommandLineParser.SentenceOptionGroupWord; }
        }

        public override Func<string> ErrorsHeadingText
        {
            // Cannot be pluralized
            get { return () => CommandLineParser.SentenceErrorsHeadingText; }
        }

        public override Func<string> UsageHeadingText
        {
            get { return () => CommandLineParser.SentenceUsageHeadingText; }
        }

        public override Func<bool, string> HelpCommandText
        {
            get
            {
                return isOption => isOption
                    ? CommandLineParser.SentenceHelpCommandTextOption
                    : CommandLineParser.SentenceHelpCommandTextVerb;
            }
        }

        public override Func<bool, string> VersionCommandText
        {
            get { return _ => CommandLineParser.SentenceVersionCommandText; }
        }

        public override Func<Error, string> FormatError
        {
            get
            {
                return error =>
                {
                    switch (error.Tag)
                    {
                        case ErrorType.BadFormatTokenError:
                            return String.Format(CommandLineParser.SentenceBadFormatTokenError,
                                ((BadFormatTokenError)error).Token);
                        case ErrorType.MissingValueOptionError:
                            return String.Format(CommandLineParser.SentenceMissingValueOptionError,
                                ((MissingValueOptionError)error).NameInfo.NameText);
                        case ErrorType.UnknownOptionError:
                            return String.Format(CommandLineParser.SentenceUnknownOptionError,
                                ((UnknownOptionError)error).Token);
                        case ErrorType.MissingRequiredOptionError:
                            var errMisssing = ((MissingRequiredOptionError)error);
                            return errMisssing.NameInfo.Equals(NameInfo.EmptyName)
                                ? CommandLineParser.SentenceMissingRequiredOptionError
                                : String.Format(CommandLineParser.SentenceMissingRequiredOptionError,
                                    errMisssing.NameInfo.NameText);
                        case ErrorType.BadFormatConversionError:
                            var badFormat = ((BadFormatConversionError)error);
                            return badFormat.NameInfo.Equals(NameInfo.EmptyName)
                                ? CommandLineParser.SentenceBadFormatConversionErrorValue
                                : String.Format(CommandLineParser.SentenceBadFormatConversionErrorOption,
                                    badFormat.NameInfo.NameText);
                        case ErrorType.SequenceOutOfRangeError:
                            var seqOutRange = ((SequenceOutOfRangeError)error);
                            return seqOutRange.NameInfo.Equals(NameInfo.EmptyName)
                                ? CommandLineParser.SentenceSequenceOutOfRangeErrorValue
                                : String.Format(CommandLineParser.SentenceSequenceOutOfRangeErrorOption,
                                    seqOutRange.NameInfo.NameText);
                        case ErrorType.BadVerbSelectedError:
                            return String.Format(CommandLineParser.SentenceBadVerbSelectedError,
                                ((BadVerbSelectedError)error).Token);
                        case ErrorType.NoVerbSelectedError:
                            return CommandLineParser.SentenceNoVerbSelectedError;
                        case ErrorType.RepeatedOptionError:
                            return String.Format(CommandLineParser.SentenceRepeatedOptionError,
                                ((RepeatedOptionError)error).NameInfo.NameText);
                        case ErrorType.SetValueExceptionError:
                            var setValueError = (SetValueExceptionError)error;
                            return String.Format(CommandLineParser.SentenceSetValueExceptionError,
                                setValueError.NameInfo.NameText, setValueError.Exception.Message);
                    }

                    throw new InvalidOperationException();
                };
            }
        }

        public override Func<IEnumerable<MutuallyExclusiveSetError>, string> FormatMutuallyExclusiveSetErrors
        {
            get
            {
                return errors =>
                {
                    var bySet = from e in errors
                        group e by e.SetName
                        into g
                        select new { SetName = g.Key, Errors = g.ToList() };

                    var msgs = bySet.Select(set =>
                    {
                        var names = String.Join(
                            String.Empty,
                            (from e in set.Errors select String.Format("'{0}', ", e.NameInfo.NameText)).ToArray());
                        var namesCount = set.Errors.Count;

                        var incompat = String.Join(
                            String.Empty,
                            (from x in
                                    (from s in bySet
                                        where !s.SetName.Equals(set.SetName)
                                        from e in s.Errors
                                        select e)
                                    .Distinct()
                                select String.Format("'{0}', ", x.NameInfo.NameText)).ToArray());
                        return
                            String.Format(CommandLineParser.SentenceMutuallyExclusiveSetErrors,
                                names.Substring(0, names.Length - 2), incompat.Substring(0, incompat.Length - 2));
                    }).ToArray();
                    return string.Join(Environment.NewLine, msgs);
                };
            }
        }
    }
}