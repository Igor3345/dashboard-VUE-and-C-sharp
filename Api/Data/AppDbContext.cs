using Microsoft.EntityFrameworkCore;
using ApiProAnalytics.Models;
using Task = ApiProAnalytics.Models.Task;

namespace ApiProAnalytics.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<CalendarEvent> CalendarEvents => Set<CalendarEvent>();
    public DbSet<ProgressSnapshot> ProgressSnapshots => Set<ProgressSnapshot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "ivan", PasswordHash = BCrypt(  "password123"), FullName = "Иван Иванов", Role = "Менеджер проекта", AvatarUrl = null },
            new User { Id = 2, Username = "anna",  PasswordHash = BCrypt("password123"), FullName = "Анна Петрова",    Role = "Дизайнер",          AvatarUrl = null },
            new User { Id = 3, Username = "sergey", PasswordHash = BCrypt("password123"), FullName = "Сергей Смирнов", Role = "Разработчик",       AvatarUrl = null }
        );

        // Seed TeamMembers
        modelBuilder.Entity<TeamMember>().HasData(
            new TeamMember { Id = 1, FullName = "Анна Петрова",     Role = "Дизайнер",    IsActive = true },
            new TeamMember { Id = 2, FullName = "Сергей Смирнов",   Role = "Разработчик", IsActive = true },
            new TeamMember { Id = 3, FullName = "Мария Кузнецова",  Role = "Аналитик",    IsActive = true },
            new TeamMember { Id = 4, FullName = "Дмитрий Волков",   Role = "Разработчик", IsActive = true },
            new TeamMember { Id = 5, FullName = "Елена Соколова",   Role = "Тестировщик", IsActive = true },
            new TeamMember { Id = 6, FullName = "Алексей Морозов",  Role = "DevOps",       IsActive = true },
            new TeamMember { Id = 7, FullName = "Наталья Козлова",  Role = "PM",           IsActive = true },
            new TeamMember { Id = 8, FullName = "Игорь Новиков",    Role = "Разработчик", IsActive = true },
            new TeamMember { Id = 9, FullName = "Ольга Попова",     Role = "Дизайнер",    IsActive = true },
            new TeamMember { Id = 10, FullName = "Максим Лебедев",  Role = "Разработчик", IsActive = true },
            new TeamMember { Id = 11, FullName = "Татьяна Орлова",  Role = "QA",           IsActive = true },
            new TeamMember { Id = 12, FullName = "Роман Киселёв",   Role = "Backend",      IsActive = true }
        );

        // Seed Tasks
        var baseDate = new DateTime(2024, 5, 21);
        modelBuilder.Entity<Task>().HasData(
            new Task { Id = 1, UserId = 1, Title = "Разработать дизайн системы",  Priority = "High",   Status = "InProgress", DueDate = baseDate.AddDays(3)  },
            new Task { Id = 2, UserId = 1, Title = "Верстка главной страницы",    Priority = "Medium", Status = "InProgress", DueDate = baseDate.AddDays(5)  },
            new Task { Id = 3, UserId = 1, Title = "Интеграция с API",            Priority = "Low",    Status = "InProgress", DueDate = baseDate.AddDays(7)  },
            new Task { Id = 4, UserId = 1, Title = "Написать unit-тесты",         Priority = "Medium", Status = "Review",     DueDate = baseDate.AddDays(2)  },
            new Task { Id = 5, UserId = 1, Title = "Настройка CI/CD",             Priority = "High",   Status = "Done",       DueDate = baseDate.AddDays(-2) },
            new Task { Id = 6, UserId = 1, Title = "Документация по API",         Priority = "Low",    Status = "Done",       DueDate = baseDate.AddDays(-5) },
            new Task { Id = 7, UserId = 2, Title = "Дизайн мобильного приложения",Priority = "High",   Status = "InProgress", DueDate = baseDate.AddDays(4)  },
            new Task { Id = 8, UserId = 3, Title = "Оптимизация базы данных",     Priority = "Medium", Status = "Review",     DueDate = baseDate.AddDays(1)  }
        );

        // Seed ActivityLogs
        var now = new DateTime(2024, 5, 21, 14, 0, 0);
        modelBuilder.Entity<ActivityLog>().HasData(
            new ActivityLog { Id = 1, TeamMemberId = 1, ActionType = "completed", Description = "завершила задачу",  ProjectName = "Дизайн главной страницы",   CreatedAt = now.AddHours(-2)  },
            new ActivityLog { Id = 2, TeamMemberId = 2, ActionType = "updated",   Description = "обновил проект",    ProjectName = "Мобильное приложение",      CreatedAt = now.AddHours(-4)  },
            new ActivityLog { Id = 3, TeamMemberId = 3, ActionType = "added",     Description = "добавила файл",     ProjectName = "Презентация проекта",       CreatedAt = now.AddHours(-6)  },
            new ActivityLog { Id = 4, TeamMemberId = 4, ActionType = "created",   Description = "создал задачу",     ProjectName = "Настройка аналитики",       CreatedAt = now.AddDays(-1)   }
        );

        // Seed CalendarEvents
        var may = new DateTime(2024, 5, 1);
        modelBuilder.Entity<CalendarEvent>().HasData(
            new CalendarEvent { Id = 1, UserId = 1, Title = "Планёрка команды",   EventDate = may.AddDays(20), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0),  Color = "#3B82F6" },
            new CalendarEvent { Id = 2, UserId = 1, Title = "Презентация проекта",EventDate = may.AddDays(20), StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 30, 0), Color = "#10B981" },
            new CalendarEvent { Id = 3, UserId = 1, Title = "Код-ревью",          EventDate = may.AddDays(22), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0),  Color = "#F59E0B" },
            new CalendarEvent { Id = 4, UserId = 1, Title = "Встреча с клиентом", EventDate = may.AddDays(23), StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0),  Color = "#8B5CF6" }
        );

        // Seed ProgressSnapshot
        modelBuilder.Entity<ProgressSnapshot>().HasData(
            new ProgressSnapshot
            {
                Id = 1, UserId = 1,
                OverallProgress = 75, ProgressChangePercent = 12,
                TasksDone = 142,      TasksDoneChangePct = 18,
                TasksInProgress = 28, TasksInProgressChangePct = -5,
                TeamActiveCount = 12,
                EfficiencyIndex = 8.9,
                TimeSavedMinutes = 24 * 60 + 15,
                RecordedAt = new DateTime(2024, 5, 21)
            }
        );
    }

    // Simple SHA-256 hash stand-in — real BCrypt handled via service
    private static string BCrypt(string password) =>
        Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(
            System.Text.Encoding.UTF8.GetBytes(password + "proanal_salt")));
}
