using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCodeCamp.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TalkSpeakers",
                columns: new[] { "TalkId", "SpeakerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 1, 2 },
                    { 2, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TalkSpeakers",
                keyColumns: new[] { "TalkId", "SpeakerId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TalkSpeakers",
                keyColumns: new[] { "TalkId", "SpeakerId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "TalkSpeakers",
                keyColumns: new[] { "TalkId", "SpeakerId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "TalkSpeakers",
                keyColumns: new[] { "TalkId", "SpeakerId" },
                keyValues: new object[] { 2, 2 });
        }
    }
}
