using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sandbox.Wordbook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastViewed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastViewedAt",
                table: "Translations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastViewedAt",
                table: "Translations");
        }
    }
}
