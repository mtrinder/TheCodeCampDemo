using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCodeCamp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camps",
                columns: table => new
                {
                    CampId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Moniker = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camps", x => x.CampId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VenueName = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    CityTown = table.Column<string>(nullable: true),
                    StateProvince = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CampId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_Camps_CampId",
                        column: x => x.CampId,
                        principalTable: "Camps",
                        principalColumn: "CampId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talks",
                columns: table => new
                {
                    TalkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    SpeakerId = table.Column<int>(nullable: false),
                    CampId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talks", x => x.TalkId);
                    table.ForeignKey(
                        name: "FK_Talks_Camps_CampId",
                        column: x => x.CampId,
                        principalTable: "Camps",
                        principalColumn: "CampId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    SpeakerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    CompanyUrl = table.Column<string>(nullable: true),
                    BlogUrl = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    GitHub = table.Column<string>(nullable: true),
                    TalkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.SpeakerId);
                    table.ForeignKey(
                        name: "FK_Speakers_Talks_TalkId",
                        column: x => x.TalkId,
                        principalTable: "Talks",
                        principalColumn: "TalkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Camps",
                columns: new[] { "CampId", "EventDate", "Length", "LocationId", "Moniker", "Name" },
                values: new object[] { 1, new DateTime(2018, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "ATL2018", "Atlanta Code Camp" });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "LocationId", "Address1", "Address2", "Address3", "CampId", "CityTown", "Country", "PostalCode", "StateProvince", "VenueName" },
                values: new object[] { 1, "123 Main Street", null, null, 1, "Atlanta", "USA", "12345", "GA", "Atlanta Convention Center" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "TalkId", "Abstract", "CampId", "Level", "SpeakerId", "Title" },
                values: new object[] { 1, "Entity Framework from scratch in an hour. Probably cover it all", 1, 100, 1, "Entity Framework From Scratch" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "TalkId", "Abstract", "CampId", "Level", "SpeakerId", "Title" },
                values: new object[] { 2, "Thinking of good sample data examples is tiring.", 1, 200, 2, "Writing Sample Data Made Easy" });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "SpeakerId", "BlogUrl", "Company", "CompanyUrl", "FirstName", "GitHub", "LastName", "MiddleName", "TalkId", "Twitter" },
                values: new object[] { 1, "http://wildermuth.com", "Wilder Minds LLC", "http://wilderminds.com", "Shawn", "shawnwildermuth", "Wildermuth", null, 1, "shawnwildermuth" });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "SpeakerId", "BlogUrl", "Company", "CompanyUrl", "FirstName", "GitHub", "LastName", "MiddleName", "TalkId", "Twitter" },
                values: new object[] { 2, "http://shawnandresa.com", "Wilder Minds LLC", "http://wilderminds.com", "Resa", "resawildermuth", "Wildermuth", null, 2, "resawildermuth" });

            migrationBuilder.CreateIndex(
                name: "IX_Location_CampId",
                table: "Location",
                column: "CampId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_TalkId",
                table: "Speakers",
                column: "TalkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Talks_CampId",
                table: "Talks",
                column: "CampId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Talks");

            migrationBuilder.DropTable(
                name: "Camps");
        }
    }
}
