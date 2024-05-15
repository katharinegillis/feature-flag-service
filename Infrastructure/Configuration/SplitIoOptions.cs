namespace Infrastructure.Configuration;

public record SplitIoOptions : ISplitIoOptions
{
    public string SdkKey { get; set; } = string.Empty;
    public string TreatmentKey { get; set; } = string.Empty;
}