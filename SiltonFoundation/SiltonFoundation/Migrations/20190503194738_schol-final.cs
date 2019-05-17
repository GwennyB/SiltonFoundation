using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiltonFoundation.Migrations
{
    public partial class scholfinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Prompt = table.Column<string>(nullable: false),
                    ResponseType = table.Column<int>(nullable: false),
                    ResponseOptions = table.Column<string>(nullable: true),
                    Score = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SFApps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    AppYear = table.Column<int>(nullable: false),
                    ApplyingFor = table.Column<int>(nullable: false),
                    Awarded = table.Column<bool>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    AdminEmail = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    AppStatus = table.Column<int>(nullable: false),
                    Closed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFApps", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppResponses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SFAppID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppResponses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AppResponses_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppResponses_SFApps_SFAppID",
                        column: x => x.SFAppID,
                        principalTable: "SFApps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastChange = table.Column<DateTime>(nullable: false),
                    Closed = table.Column<DateTime>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    SFAppID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    AppResponseID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evaluations_AppResponses_AppResponseID",
                        column: x => x.AppResponseID,
                        principalTable: "AppResponses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_SFApps_SFAppID",
                        column: x => x.SFAppID,
                        principalTable: "SFApps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "ID", "Active", "Category", "Prompt", "ResponseOptions", "ResponseType", "Score" },
                values: new object[,]
                {
                    { 1, true, "About", "Which grant do you wish to apply for?", "National, West Coast, Both", 2, false },
                    { 24, true, "Usage of Scholarship Funds", @"Provide a detailed budget for a complete use of an estimated $1000 scholarship allocation (Not including event tickets granted.) 
                No monies can be given to The Silton Foundation, Doug Silton or any other board members of TSF, and all monies must be used within one 12 month period from receiving the grant. 
                Examples: 
                $200 private coaching from Mrs. Amazing Instructor 
                $120 event ticket to Super Awesome Dance Event Not Included With My Grant 
                $180 shared hotel room for 3 nights at Superdance Event", null, 0, true },
                    { 23, true, "Dance Background", "WCS Conventions you hope to attend:", null, 0, true },
                    { 22, true, "Dance Background", "WCS Conventions you have attended (please list which years you attended):", null, 0, true },
                    { 21, true, "Dance Background", "Which dancer(s) has/have influenced you most and how/why?", null, 0, true },
                    { 20, true, "Dance Background", "How/When/Where did you start dancing West Coast Swing?", null, 0, true },
                    { 19, true, "Dance Background", "What do you enjoy doing when you're not dancing?", null, 0, true },
                    { 18, true, "Dance Background", "Do you regularly take lessons? Where/from whom?", null, 0, true },
                    { 17, true, "Dance Background", "Do you volunteer or help out in your local dance community? If yes, please explain.", null, 0, true },
                    { 16, true, "Dance Background", "Are you proficient in any other dance styles? If so, which ones, and how much time do you spend weekly on those other styles?", null, 0, true },
                    { 15, true, "Dance Background", "How much time do you spend weekly with West Coast Swing teaching?", null, 1, true },
                    { 14, true, "Dance Background", "How much time do you spend weekly with West Coast Swing practicing?", null, 1, true },
                    { 13, true, "Dance Background", "How much time do you spend weekly with West Coast Swing taking lessons?", null, 1, true },
                    { 12, true, "Dance Background", "How much time do you spend weekly with West Coast Swing social dancing?", null, 1, true },
                    { 11, true, "Dance Background", "What do you enjoy about West Coast Swing?", null, 0, true },
                    { 10, true, "About", @"Please upload a video of your dancing to YouTube and attach the video link below.
                (Suggestion: Store your files in your Google Drive account, and submit the 'Share' link.)", null, 1, true },
                    { 9, true, "About", @"Please submit a link for a clear headshot of yourself. 
                (Suggestion: Store your files in your Google Drive account, and submit the 'Share' link.)", null, 1, true },
                    { 8, true, "About", @"Please create an introduction video of yourself and share the video link below. 
                (Suggestion: Store your video on YouTube or Vimeo, and submit the 'Share' link.)", null, 1, true },
                    { 7, true, "About", "Do you generate any income as a dancer/instructor/performer?", null, 1, true },
                    { 6, true, "About", "Can you be claimed as a dependent?", "Yes, No", 2, true },
                    { 5, true, "About", "Annual Income:", null, 1, true },
                    { 4, true, "About", "Occupation(s):", null, 1, true },
                    { 3, true, "About", "Current state of residence:", null, 1, false },
                    { 2, true, "About", "Age", null, 1, false },
                    { 25, true, "References", "If you have any professionals in the West Coast Swing or other dance worlds that you would like to ask for references, please have them send you their reference letter and submit a link to it below along with their contact information (name, phone, and email). (This is not mandatory)", null, 0, true },
                    { 26, true, "Essay", "Please explaining why you should be chosen to receive TSF Scholarship. This essay is worth a majority of your application score. Please be as detailed as you would like. Information from this essay may be shared publicly.", null, 0, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppResponses_QuestionID",
                table: "AppResponses",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_AppResponses_SFAppID",
                table: "AppResponses",
                column: "SFAppID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_AppResponseID",
                table: "Evaluations",
                column: "AppResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QuestionID",
                table: "Evaluations",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_SFAppID",
                table: "Evaluations",
                column: "SFAppID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "AppResponses");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "SFApps");
        }
    }
}
