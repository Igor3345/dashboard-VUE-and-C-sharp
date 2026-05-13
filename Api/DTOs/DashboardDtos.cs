namespace ApiProAnalytics.DTOs;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string Token, string FullName, string Role);

public record DashboardStatsDto(
    double OverallProgress,
    double ProgressChangePercent,
    int TasksDone,
    double TasksDoneChangePct,
    int TasksInProgress,
    double TasksInProgressChangePct,
    int TeamActiveCount,
    double EfficiencyIndex,
    int TimeSavedMinutes
);

public record TaskDto(
    int Id,
    string Title,
    string Priority,
    string Status,
    DateTime DueDate
);

public record TeamMemberDto(
    int Id,
    string FullName,
    string Role,
    string? AvatarUrl
);

public record ActivityLogDto(
    int Id,
    string MemberName,
    string? AvatarUrl,
    string ActionType,
    string Description,
    string ProjectName,
    DateTime CreatedAt
);

public record CalendarEventDto(
    int Id,
    string Title,
    DateTime EventDate,
    string StartTime,
    string EndTime,
    string Color
);
