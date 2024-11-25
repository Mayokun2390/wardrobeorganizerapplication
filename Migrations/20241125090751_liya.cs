using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WardrobeOrganizerApp.Migrations
{
    /// <inheritdoc />
    public partial class liya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("2a3ddc5e-4419-419b-bb93-4579ca32d35b"), "customer" },
                    { new Guid("46fe2739-8ed1-4d28-92a0-f1982d4a1da0"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { new Guid("af1f3b26-d83f-4c74-ac23-395beed7f2b2"), "admin@gmail.com", "$2a$11$Xwv9jCSzqfvK0V2DnRvy.eS7ZwQy.9JsDYNvSdPD4PtOFPa.y/BXa", "$2a$11$Xwv9jCSzqfvK0V2DnRvy.e", "Mayokun" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("432161cc-c353-4bc9-8c6b-af3cf4f6169d"), new Guid("46fe2739-8ed1-4d28-92a0-f1982d4a1da0"), new Guid("af1f3b26-d83f-4c74-ac23-395beed7f2b2") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2a3ddc5e-4419-419b-bb93-4579ca32d35b"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("432161cc-c353-4bc9-8c6b-af3cf4f6169d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("46fe2739-8ed1-4d28-92a0-f1982d4a1da0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("af1f3b26-d83f-4c74-ac23-395beed7f2b2"));

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
    }
}
