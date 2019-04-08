using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToggleSystem.Infra.Data.Migrations
{
    public partial class ToggleUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToggleUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    ToggleId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ToggleValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToggleUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToggleUsers_ToggleValue",
                table: "ToggleUsers",
                column: "ToggleValue");

            migrationBuilder.CreateIndex(
                name: "IX_ToggleUsers_UserId",
                table: "ToggleUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToggleUsers");
        }
    }
}
