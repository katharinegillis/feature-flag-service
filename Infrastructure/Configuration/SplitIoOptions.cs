namespace Infrastructure.Configuration;

public class SplitIoOptions
{
    public const string SplitIo = "SplitIo";
    
    public string SdkKey { get; set; } = string.Empty;
    public string TreatmentKey { get; set; } = string.Empty;
}