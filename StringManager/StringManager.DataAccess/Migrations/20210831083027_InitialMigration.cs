using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StringManager.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StringsSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfStrings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringsSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    WaveLenght = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyMaintanance = table.Column<int>(type: "int", nullable: false),
                    PlayStyle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfStrings = table.Column<int>(type: "int", nullable: false),
                    ScaleLenghtBass = table.Column<int>(type: "int", nullable: false),
                    ScaleLenghtTreble = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instruments_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Strings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StringType = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    SpecificWeight = table.Column<double>(type: "float", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Strings_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyInstruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    HoursPlayedWeekly = table.Column<int>(type: "int", nullable: false),
                    GuitarPlace = table.Column<int>(type: "int", nullable: false),
                    LastDeepCleaning = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastStringChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextStringChange = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyInstruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyInstruments_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyInstruments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StringsInSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    StringsSetId = table.Column<int>(type: "int", nullable: true),
                    StringId = table.Column<int>(type: "int", nullable: true),
                    ToneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringsInSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringsInSets_Strings_StringId",
                        column: x => x.StringId,
                        principalTable: "Strings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StringsInSets_StringsSets_StringsSetId",
                        column: x => x.StringsSetId,
                        principalTable: "StringsSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StringsInSets_Tones_ToneId",
                        column: x => x.ToneId,
                        principalTable: "Tones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstalledStrings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    MyInstrumentId = table.Column<int>(type: "int", nullable: true),
                    StringId = table.Column<int>(type: "int", nullable: true),
                    ToneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalledStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstalledStrings_MyInstruments_MyInstrumentId",
                        column: x => x.MyInstrumentId,
                        principalTable: "MyInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstalledStrings_Strings_StringId",
                        column: x => x.StringId,
                        principalTable: "Strings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstalledStrings_Tones_ToneId",
                        column: x => x.ToneId,
                        principalTable: "Tones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstalledStrings_MyInstrumentId",
                table: "InstalledStrings",
                column: "MyInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalledStrings_StringId",
                table: "InstalledStrings",
                column: "StringId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalledStrings_ToneId",
                table: "InstalledStrings",
                column: "ToneId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_ManufacturerId",
                table: "Instruments",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_MyInstruments_InstrumentId",
                table: "MyInstruments",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MyInstruments_UserId",
                table: "MyInstruments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Strings_ManufacturerId",
                table: "Strings",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_StringsInSets_StringId",
                table: "StringsInSets",
                column: "StringId");

            migrationBuilder.CreateIndex(
                name: "IX_StringsInSets_StringsSetId",
                table: "StringsInSets",
                column: "StringsSetId");

            migrationBuilder.CreateIndex(
                name: "IX_StringsInSets_ToneId",
                table: "StringsInSets",
                column: "ToneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstalledStrings");

            migrationBuilder.DropTable(
                name: "StringsInSets");

            migrationBuilder.DropTable(
                name: "MyInstruments");

            migrationBuilder.DropTable(
                name: "Strings");

            migrationBuilder.DropTable(
                name: "StringsSets");

            migrationBuilder.DropTable(
                name: "Tones");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
