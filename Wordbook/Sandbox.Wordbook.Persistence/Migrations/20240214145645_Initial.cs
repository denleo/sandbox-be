using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sandbox.Wordbook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordbookUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirebaseId = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordbookUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceLang = table.Column<string>(type: "text", nullable: false),
                    TargetLang = table.Column<string>(type: "text", nullable: false),
                    Word = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_WordbookUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "WordbookUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslationResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Translation = table.Column<string>(type: "text", nullable: false),
                    PartOfSpeech = table.Column<string>(type: "text", nullable: false),
                    Transcription = table.Column<string>(type: "text", nullable: true),
                    TranslationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationResults_Translations_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslationResults_TranslationId_Translation_PartOfSpeech",
                table: "TranslationResults",
                columns: new[] { "TranslationId", "Translation", "PartOfSpeech" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translations_UserId_Word_SourceLang_TargetLang",
                table: "Translations",
                columns: new[] { "UserId", "Word", "SourceLang", "TargetLang" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordbookUsers_Email",
                table: "WordbookUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordbookUsers_FirebaseId",
                table: "WordbookUsers",
                column: "FirebaseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationResults");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "WordbookUsers");
        }
    }
}
