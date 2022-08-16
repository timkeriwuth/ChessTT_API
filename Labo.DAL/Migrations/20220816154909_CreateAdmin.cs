using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labo.DAL.Migrations
{
    public partial class CreateAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Elo", "Email", "EncodedPassword", "Gender", "IsDeleted", "Role", "Salt", "Username" },
                values: new object[] { new Guid("696a3a24-6562-481d-a1f5-2e8db7fe3a6f"), new DateTime(1982, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, "lykhun@gmail.com", new byte[] { 22, 88, 40, 3, 100, 246, 189, 205, 105, 223, 192, 49, 115, 19, 226, 44, 9, 250, 210, 122, 145, 248, 36, 232, 90, 176, 234, 8, 150, 44, 49, 125, 59, 48, 126, 90, 136, 61, 92, 180, 72, 128, 198, 51, 179, 142, 207, 23, 216, 71, 77, 129, 181, 116, 10, 40, 138, 213, 191, 174, 206, 90, 89, 37 }, "Male", false, "Admin", new Guid("4919a3c2-0e71-4a5a-a908-4687b8a4df47"), "Checkmate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("696a3a24-6562-481d-a1f5-2e8db7fe3a6f"));
        }
    }
}
