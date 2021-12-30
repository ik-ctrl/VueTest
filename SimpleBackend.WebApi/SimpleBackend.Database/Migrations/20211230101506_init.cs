using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SimpleBackend.Database.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    TodoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UiKey = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Confirm = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.TodoId);
                },
                comment: "Основные задачи");

            migrationBuilder.CreateTable(
                name: "SubTodos",
                columns: table => new
                {
                    SubTodoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UiKey = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Confirm = table.Column<bool>(type: "boolean", nullable: false),
                    TodoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTodos", x => x.SubTodoId);
                    table.ForeignKey(
                        name: "FK_SubTodos_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Подзадачи главных задач");

            migrationBuilder.CreateIndex(
                name: "IX_SubTodos_TodoId",
                table: "SubTodos",
                column: "TodoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTodos");

            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
