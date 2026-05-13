using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiProAnalytics.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionType = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_TeamMembers_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "TeamMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    OverallProgress = table.Column<double>(type: "REAL", nullable: false),
                    ProgressChangePercent = table.Column<double>(type: "REAL", nullable: false),
                    TasksDone = table.Column<int>(type: "INTEGER", nullable: false),
                    TasksDoneChangePct = table.Column<double>(type: "REAL", nullable: false),
                    TasksInProgress = table.Column<int>(type: "INTEGER", nullable: false),
                    TasksInProgressChangePct = table.Column<double>(type: "REAL", nullable: false),
                    TeamActiveCount = table.Column<int>(type: "INTEGER", nullable: false),
                    EfficiencyIndex = table.Column<double>(type: "REAL", nullable: false),
                    TimeSavedMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressSnapshots_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "Id", "AvatarUrl", "FullName", "IsActive", "Role" },
                values: new object[,]
                {
                    { 1, null, "Анна Петрова", true, "Дизайнер" },
                    { 2, null, "Сергей Смирнов", true, "Разработчик" },
                    { 3, null, "Мария Кузнецова", true, "Аналитик" },
                    { 4, null, "Дмитрий Волков", true, "Разработчик" },
                    { 5, null, "Елена Соколова", true, "Тестировщик" },
                    { 6, null, "Алексей Морозов", true, "DevOps" },
                    { 7, null, "Наталья Козлова", true, "PM" },
                    { 8, null, "Игорь Новиков", true, "Разработчик" },
                    { 9, null, "Ольга Попова", true, "Дизайнер" },
                    { 10, null, "Максим Лебедев", true, "Разработчик" },
                    { 11, null, "Татьяна Орлова", true, "QA" },
                    { 12, null, "Роман Киселёв", true, "Backend" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "FullName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, null, "Иван Иванов", "Hba0WEVnPlBQNRk8kAWFBHqqHAhqse4YQ/pCjWV52tI=", "Менеджер проекта", "ivan" },
                    { 2, null, "Анна Петрова", "Hba0WEVnPlBQNRk8kAWFBHqqHAhqse4YQ/pCjWV52tI=", "Дизайнер", "anna" },
                    { 3, null, "Сергей Смирнов", "Hba0WEVnPlBQNRk8kAWFBHqqHAhqse4YQ/pCjWV52tI=", "Разработчик", "sergey" }
                });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "ActionType", "CreatedAt", "Description", "ProjectName", "TeamMemberId" },
                values: new object[,]
                {
                    { 1, "completed", new DateTime(2024, 5, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), "завершила задачу", "Дизайн главной страницы", 1 },
                    { 2, "updated", new DateTime(2024, 5, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), "обновил проект", "Мобильное приложение", 2 },
                    { 3, "added", new DateTime(2024, 5, 21, 8, 0, 0, 0, DateTimeKind.Unspecified), "добавила файл", "Презентация проекта", 3 },
                    { 4, "created", new DateTime(2024, 5, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), "создал задачу", "Настройка аналитики", 4 }
                });

            migrationBuilder.InsertData(
                table: "CalendarEvents",
                columns: new[] { "Id", "Color", "EndTime", "EventDate", "StartTime", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "#3B82F6", new TimeSpan(0, 11, 0, 0, 0), new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "Планёрка команды", 1 },
                    { 2, "#10B981", new TimeSpan(0, 15, 30, 0, 0), new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 0, 0, 0), "Презентация проекта", 1 },
                    { 3, "#F59E0B", new TimeSpan(0, 12, 0, 0, 0), new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), "Код-ревью", 1 },
                    { 4, "#8B5CF6", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0), "Встреча с клиентом", 1 }
                });

            migrationBuilder.InsertData(
                table: "ProgressSnapshots",
                columns: new[] { "Id", "EfficiencyIndex", "OverallProgress", "ProgressChangePercent", "RecordedAt", "TasksDone", "TasksDoneChangePct", "TasksInProgress", "TasksInProgressChangePct", "TeamActiveCount", "TimeSavedMinutes", "UserId" },
                values: new object[] { 1, 8.9000000000000004, 75.0, 12.0, new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 142, 18.0, 28, -5.0, 12, 1455, 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "DueDate", "Priority", "Status", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "InProgress", "Разработать дизайн системы", 1 },
                    { 2, new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "InProgress", "Верстка главной страницы", 1 },
                    { 3, new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", "InProgress", "Интеграция с API", 1 },
                    { 4, new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Review", "Написать unit-тесты", 1 },
                    { 5, new DateTime(2024, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "Done", "Настройка CI/CD", 1 },
                    { 6, new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", "Done", "Документация по API", 1 },
                    { 7, new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "InProgress", "Дизайн мобильного приложения", 2 },
                    { 8, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Review", "Оптимизация базы данных", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_TeamMemberId",
                table: "ActivityLogs",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_UserId",
                table: "CalendarEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressSnapshots_UserId",
                table: "ProgressSnapshots",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "ProgressSnapshots");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
