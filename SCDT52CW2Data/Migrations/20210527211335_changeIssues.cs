using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCDT52CW2Data.Migrations
{
    public partial class changeIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_TechnicalIssues_TechnicalIssueId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Updates_GeneralIssues_GeneralIssueId",
                table: "Updates");

            migrationBuilder.DropForeignKey(
                name: "FK_Updates_TechnicalIssues_TechnicalIssueId",
                table: "Updates");

            migrationBuilder.DropTable(
                name: "GeneralIssues");

            migrationBuilder.DropTable(
                name: "TechnicalIssues");

            migrationBuilder.DropIndex(
                name: "IX_Updates_GeneralIssueId",
                table: "Updates");

            migrationBuilder.DropIndex(
                name: "IX_Updates_TechnicalIssueId",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "GeneralIssueId",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "TechnicalIssueId",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Updates");

            migrationBuilder.RenameColumn(
                name: "UpdateType",
                table: "Updates",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "TechnicalIssueId",
                table: "Assets",
                newName: "IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_TechnicalIssueId",
                table: "Assets",
                newName: "IX_Assets_IssueId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Updates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IssueID",
                table: "Updates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isClosed = table.Column<bool>(type: "bit", nullable: false),
                    isTechnical = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Updates_IssueID",
                table: "Updates",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_Updates_UserId",
                table: "Updates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Issues_IssueId",
                table: "Assets",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Updates_AspNetUsers_UserId",
                table: "Updates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Updates_Issues_IssueID",
                table: "Updates",
                column: "IssueID",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Issues_IssueId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Updates_AspNetUsers_UserId",
                table: "Updates");

            migrationBuilder.DropForeignKey(
                name: "FK_Updates_Issues_IssueID",
                table: "Updates");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Updates_IssueID",
                table: "Updates");

            migrationBuilder.DropIndex(
                name: "IX_Updates_UserId",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "IssueID",
                table: "Updates");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Updates",
                newName: "UpdateType");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Assets",
                newName: "TechnicalIssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_IssueId",
                table: "Assets",
                newName: "IX_Assets_TechnicalIssueId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Updates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "GeneralIssueId",
                table: "Updates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalIssueId",
                table: "Updates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Updates",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "GeneralIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralIssues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalIssues", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Updates_GeneralIssueId",
                table: "Updates",
                column: "GeneralIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Updates_TechnicalIssueId",
                table: "Updates",
                column: "TechnicalIssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_TechnicalIssues_TechnicalIssueId",
                table: "Assets",
                column: "TechnicalIssueId",
                principalTable: "TechnicalIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Updates_GeneralIssues_GeneralIssueId",
                table: "Updates",
                column: "GeneralIssueId",
                principalTable: "GeneralIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Updates_TechnicalIssues_TechnicalIssueId",
                table: "Updates",
                column: "TechnicalIssueId",
                principalTable: "TechnicalIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
