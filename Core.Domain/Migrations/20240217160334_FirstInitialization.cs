using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Domain.Migrations
{
    public partial class FirstInitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "card");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "card",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AccessTokenLifetimeInMins = table.Column<int>(type: "int", nullable: false),
                    AuthorizationCodeLifetimeInMins = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageId = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsoCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "card",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<byte>(type: "tinyint", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                schema: "card",
                columns: table => new
                {
                    CardId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "card",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "LanguageId", "CreatedAt", "CreatedBy", "Default", "IsActive", "IsoCode", "ModifiedAt", "ModifiedBy", "Name", "RecordStatus" },
                values: new object[,]
                {
                    { (byte)1, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), "seed", true, false, "en", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "English", (byte)0 },
                    { (byte)2, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), "seed", false, false, "sw", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "Swahili", (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingId", "CreatedAt", "CreatedBy", "Key", "ModifiedAt", "ModifiedBy", "RecordStatus", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "InstanceName", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "Nito POS" },
                    { 2, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "CurrencyCode", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "Ksh" },
                    { 3, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "CurrencyConversionUnit", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "100" },
                    { 4, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "MiniStatementSize", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "5" },
                    { 5, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "MinimumMonthsInFullStatement", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "1" },
                    { 6, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "MaximumMonthsInFullStatement", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "12" },
                    { 7, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "Helpline", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "0720720720" },
                    { 8, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "DateTimeFormat", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "d/M/yyyy h:mm:ss tt" },
                    { 9, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "TimeZone", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "E. Africa Standard Time" },
                    { 10, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "TokenLifetimeInMins", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "60" },
                    { 11, new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, "CodeLifetimeInMins", new DateTime(2021, 4, 23, 18, 40, 0, 0, DateTimeKind.Unspecified), null, (byte)0, "10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                schema: "card",
                table: "Cards",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards",
                schema: "card");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "card");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "card");
        }
    }
}
