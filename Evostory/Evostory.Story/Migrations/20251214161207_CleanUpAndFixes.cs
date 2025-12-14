using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoStory.Database.Migrations
{
    /// <inheritdoc />
    public partial class CleanUpAndFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Scenes_SceneId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_SceneId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "SceneId",
                table: "Contents");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "Scenes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_ContentId",
                table: "Scenes",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Contents_ContentId",
                table: "Scenes",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Contents_ContentId",
                table: "Scenes");

            migrationBuilder.DropIndex(
                name: "IX_Scenes_ContentId",
                table: "Scenes");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Scenes");

            migrationBuilder.AddColumn<Guid>(
                name: "SceneId",
                table: "Contents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Contents_SceneId",
                table: "Contents",
                column: "SceneId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Scenes_SceneId",
                table: "Contents",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
