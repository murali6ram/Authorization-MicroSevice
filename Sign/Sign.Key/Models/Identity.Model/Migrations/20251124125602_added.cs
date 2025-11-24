using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Model.Migrations
{
    /// <inheritdoc />
    public partial class added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedOn = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtpManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OneTimePassword = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedOn = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtpManager_IdentityUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Email", "ModifiedBy", "ModifiedOn", "Name", "PhoneNumber", "Status", "UserName" },
                values: new object[] { new Guid("1234abcd-1234-1234-1234-ab1234567890"), "Super User", 1763818667L, "superuser@email.com", "Super User", 1763818667L, "Super User", "1234", "Active", "" });

            migrationBuilder.InsertData(
                table: "OtpManager",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IdentityUserId", "IsActive", "ModifiedBy", "ModifiedOn", "OneTimePassword", "Status" },
                values: new object[] { new Guid("abcd1234-abcd-1234-abcd-1234567890ef"), "Super User", new DateTime(1970, 1, 21, 9, 56, 58, 667, DateTimeKind.Utc), new Guid("1234abcd-1234-1234-1234-ab1234567890"), true, "Super User", 1763818667L, 1234, "Active" });

            migrationBuilder.CreateIndex(
                name: "IX_OtpManager_IdentityUserId",
                table: "OtpManager",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpManager");

            migrationBuilder.DropTable(
                name: "IdentityUsers");
        }
    }
}
