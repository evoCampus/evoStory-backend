using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoStory.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddConditionsAndStackability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequiredItemId",
                table: "Choises",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredItemId",
                table: "Choises");
        }
    }
}
