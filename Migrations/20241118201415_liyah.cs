using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WardrobeOrganizerApp.Migrations
{
    /// <inheritdoc />
    public partial class liyah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2f20ae80-dab4-4df7-b17a-920587eff79b"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("9adec0f8-354d-45ac-877b-e87d661af051"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4ee7135b-f5b4-43a3-9f12-af1a892555d1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8e9677b-9574-4a13-81ec-2215b7ed6051"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("31ba4bee-3414-44b6-bc85-51e4613990dc"), "customer" },
                    { new Guid("ed5b29e3-c635-44ca-a5f2-407da0b892e4"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { new Guid("420f72c1-19af-4426-84ce-25b0f70da172"), "admin@gmail.com", "$2a$11$QMExrOeiFS1ofxbPaFDGOOUmB0zrOTw/USQ0rc7YMJqQ8RSwbQT/u", "$2a$11$QMExrOeiFS1ofxbPaFDGOO", "Mayokun" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("d8af9543-6fb3-4714-a032-9ce7cb967b33"), new Guid("ed5b29e3-c635-44ca-a5f2-407da0b892e4"), new Guid("420f72c1-19af-4426-84ce-25b0f70da172") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("31ba4bee-3414-44b6-bc85-51e4613990dc"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8af9543-6fb3-4714-a032-9ce7cb967b33"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ed5b29e3-c635-44ca-a5f2-407da0b892e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("420f72c1-19af-4426-84ce-25b0f70da172"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2f20ae80-dab4-4df7-b17a-920587eff79b"), "customer" },
                    { new Guid("4ee7135b-f5b4-43a3-9f12-af1a892555d1"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { new Guid("b8e9677b-9574-4a13-81ec-2215b7ed6051"), "admin@gmail.com", "$2a$11$hPRL20CmmRF7AqXoWSo29utoqwxUmCChIblrHQksbCIpp0aGLaCoK", "$2a$11$hPRL20CmmRF7AqXoWSo29u", "Mayokun" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("9adec0f8-354d-45ac-877b-e87d661af051"), new Guid("4ee7135b-f5b4-43a3-9f12-af1a892555d1"), new Guid("b8e9677b-9574-4a13-81ec-2215b7ed6051") });
        }
    }
}
