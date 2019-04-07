using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCodeCamp.Migrations
{
    public partial class SpeakerTalks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Talks_TalkId",
                table: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_TalkId",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "SpeakerId",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "TalkId",
                table: "Speakers");

            migrationBuilder.CreateTable(
                name: "TalkSpeakers",
                columns: table => new
                {
                    TalkId = table.Column<int>(nullable: false),
                    SpeakerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalkSpeakers", x => new { x.TalkId, x.SpeakerId });
                    table.ForeignKey(
                        name: "FK_TalkSpeakers_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TalkSpeakers_Talks_TalkId",
                        column: x => x.TalkId,
                        principalTable: "Talks",
                        principalColumn: "TalkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TalkSpeakers_SpeakerId",
                table: "TalkSpeakers",
                column: "SpeakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TalkSpeakers");

            migrationBuilder.AddColumn<int>(
                name: "SpeakerId",
                table: "Talks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TalkId",
                table: "Speakers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Speakers",
                keyColumn: "SpeakerId",
                keyValue: 1,
                column: "TalkId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Speakers",
                keyColumn: "SpeakerId",
                keyValue: 2,
                column: "TalkId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Talks",
                keyColumn: "TalkId",
                keyValue: 1,
                column: "SpeakerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Talks",
                keyColumn: "TalkId",
                keyValue: 2,
                column: "SpeakerId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_TalkId",
                table: "Speakers",
                column: "TalkId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Talks_TalkId",
                table: "Speakers",
                column: "TalkId",
                principalTable: "Talks",
                principalColumn: "TalkId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
