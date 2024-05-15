namespace Infrastructure.Configuration;

public interface ISplitIoOptions
{
    string SdkKey { get; set; }
    string TreatmentKey { get; set; }
}