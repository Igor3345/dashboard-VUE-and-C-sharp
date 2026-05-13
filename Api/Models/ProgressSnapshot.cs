namespace ApiProAnalytics.Models;

public class ProgressSnapshot
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public double OverallProgress { get; set; }
    public double ProgressChangePercent { get; set; } 
    public int TasksDone { get; set; }
    public double TasksDoneChangePct { get; set; }
    public int TasksInProgress { get; set; }
    public double TasksInProgressChangePct { get; set; }
    public int TeamActiveCount { get; set; }
    public double EfficiencyIndex { get; set; }
    public int TimeSavedMinutes { get; set; }
    public DateTime RecordedAt { get; set; }
}
