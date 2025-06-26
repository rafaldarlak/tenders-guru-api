namespace Tenders.Guru.Infrastructure.Services;

public class TenderSyncOptions
{
    public const string SectionName = "TenderSync";
    
    public int IntervalMinutes { get; set; } = 60;
    public bool Enabled { get; set; } = true;
}