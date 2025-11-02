using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoStory.BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choice_Scenes_SceneId",
                table: "Choice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choice",
                table: "Choice");

            migrationBuilder.RenameTable(
                name: "Choice",
                newName: "Choises");

            migrationBuilder.RenameIndex(
                name: "IX_Choice_SceneId",
                table: "Choises",
                newName: "IX_Choises_SceneId");

            migrationBuilder.AddColumn<Guid>(
                name: "StoryId",
                table: "Scenes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choises",
                table: "Choises",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingSceneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_StoryId",
                table: "Scenes",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choises_Scenes_SceneId",
                table: "Choises",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Stories_StoryId",
                table: "Scenes",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choises_Scenes_SceneId",
                table: "Choises");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Stories_StoryId",
                table: "Scenes");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Scenes_StoryId",
                table: "Scenes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choises",
                table: "Choises");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Scenes");

            migrationBuilder.RenameTable(
                name: "Choises",
                newName: "Choice");

            migrationBuilder.RenameIndex(
                name: "IX_Choises_SceneId",
                table: "Choice",
                newName: "IX_Choice_SceneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choice",
                table: "Choice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_Scenes_SceneId",
                table: "Choice",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "Id");
        }
    }
}
