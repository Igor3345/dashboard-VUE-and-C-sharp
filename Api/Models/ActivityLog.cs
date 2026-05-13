namespace ApiProAnalytics.Models;

public class ActivityLog
{
    public int Id { get; set; }
    public int TeamMemberId { get; set; }
    public TeamMember? TeamMember { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
