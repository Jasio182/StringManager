using Microsoft.EntityFrameworkCore.Migrations;

namespace StringManager.DataAccess.Migrations
{
    public partial class AddedTuningsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tunings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfStrings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tunings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToneInTuning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToneId = table.Column<int>(type: "int", nullable: true),
                    TuningId = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToneInTuning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToneInTuning_Tones_ToneId",
                        column: x => x.ToneId,
                        principalTable: "Tones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ToneInTuning_Tunings_TuningId",
                        column: x => x.TuningId,
                        principalTable: "Tunings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToneInTuning_ToneId",
                table: "ToneInTuning",
                column: "ToneId");

            migrationBuilder.CreateIndex(
                name: "IX_ToneInTuning_TuningId",
                table: "ToneInTuning",
                column: "TuningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToneInTuning");

            migrationBuilder.DropTable(
                name: "Tunings");
        }
    }
}
