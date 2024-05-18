namespace Infrastructure.Configuration;

public class SplitIoOptions
{
    public const string SplitIo = "SplitIo";
    
    public string SdkKey { get; init; } = string.Empty;
    public string TreatmentKey { get; init; } = string.Empty;
}