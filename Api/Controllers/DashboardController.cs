using System.Security.Claims;
using ApiProAnalytics.Data;
using ApiProAnalytics.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = ApiProAnalytics.Models.Task;

namespace ApiProAnalytics.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _db;

    public DashboardController(AppDbContext db)
    {
        _db = db;
    }

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    

        

    // GET /api/dashboard/stats
    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStatsDto>> GetStats()
    {
        var snap = await _db.ProgressSnapshots
            .Where(p => p.UserId == CurrentUserId)
            .OrderByDescending(p => p.RecordedAt)
            .FirstOrDefaultAsync();

        if (snap is null)
            return NotFound(new { message = "Нет данных" });

        return Ok(new DashboardStatsDto(
            snap.OverallProgress,
            snap.ProgressChangePercent,
            snap.TasksDone,
            snap.TasksDoneChangePct,
            snap.TasksInProgress,
            snap.TasksInProgressChangePct,
            snap.TeamActiveCount,
            snap.EfficiencyIndex,
            snap.TimeSavedMinutes
        ));
    }

    // GET /api/dashboard/tasks?status=InProgress
    [HttpGet("tasks")]
    public async Task<ActionResult<List<TaskDto>>> GetTasks([FromQuery] string? status)
    {
        var query = _db.Tasks.Where(t => t.UserId == CurrentUserId);

        if (!string.IsNullOrWhiteSpace(status) && status != "All")
            query = query.Where(t => t.Status == status);

        var tasks = await query
            .OrderBy(t => t.DueDate)
            .Select(t => new TaskDto(t.Id, t.Title, t.Priority, t.Status, t.DueDate))
            .ToListAsync();

        return Ok(tasks);
    }

    // GET /api/dashboard/team
    [HttpGet("team")]
    public async Task<ActionResult<List<TeamMemberDto>>> GetTeam()
    {
        var members = await _db.TeamMembers
            .Where(m => m.IsActive)
            .Select(m => new TeamMemberDto(m.Id, m.FullName, m.Role, m.AvatarUrl))
            .ToListAsync();

        return Ok(members);
    }

    // GET /api/dashboard/activity
    [HttpGet("activity")]
    public async Task<ActionResult<List<ActivityLogDto>>> GetActivity()
{
    var activity = await _db.ActivityLogs
        .Include(a => a.TeamMember)
        .OrderByDescending(a => a.CreatedAt)
        .Take(10)
        .Select(a => new ActivityLogDto(
            a.Id,                          
            a.TeamMember.FullName,         
            a.TeamMember.AvatarUrl,        
            a.ActionType,                  
            a.Description,                 
            a.ProjectName,                 
            a.CreatedAt                    
        ))
        .ToListAsync();

    return Ok(activity);
}

    // GET /api/dashboard/calendar?year=2024&month=5
    [HttpGet("calendar")]
    public async Task<ActionResult<List<CalendarEventDto>>> GetCalendar(
        [FromQuery] int year  = 0,
        [FromQuery] int month = 0)
    {
        if (year == 0)  year  = DateTime.Today.Year;
        if (month == 0) month = DateTime.Today.Month;

        // SQLite не поддерживает ORDER BY по TimeSpan — сортируем EventDate в SQL,
        // а StartTime — в памяти после выборки (AsEnumerable).
        var raw = await _db.CalendarEvents
            .Where(e => e.UserId == CurrentUserId
                     && e.EventDate.Year  == year
                     && e.EventDate.Month == month)
            .OrderBy(e => e.EventDate)
            .ToListAsync();

        var events = raw
            .OrderBy(e => e.EventDate)
            .ThenBy(e => e.StartTime)
            .Select(e => new CalendarEventDto(
                e.Id,
                e.Title,
                e.EventDate,
                e.StartTime.ToString(@"hh\:mm"),
                e.EndTime.ToString(@"hh\:mm"),
                e.Color
            ))
            .ToList();

        return Ok(events);
    }
}
