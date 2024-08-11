namespace Infrastructure.Configuration;

public class SplitOptions
{
    public const string Split = "Split";

    public string SdkKey { get; init; } = string.Empty;
    public string TreatmentKey { get; init; } = string.Empty;
}