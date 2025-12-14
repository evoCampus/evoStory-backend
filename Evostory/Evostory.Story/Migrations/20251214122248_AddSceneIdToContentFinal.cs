using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoStory.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSceneIdToContentFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choises_Scenes_SceneId",
                table: "Choises");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Contents_ContentId",
                table: "Scenes");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Stories_StoryId",
                table: "Scenes");

            migrationBuilder.DropIndex(
                name: "IX_Scenes_ContentId",
                table: "Scenes");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Scenes");

            migrationBuilder.AlterColumn<Guid>(
                name: "StoryId",
                table: "Scenes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SceneId",
                table: "Contents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SceneId",
                table: "Choises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contents_SceneId",
                table: "Contents",
                column: "SceneId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Choises_Scenes_SceneId",
                table: "Choises",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Scenes_SceneId",
                table: "Contents",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Stories_StoryId",
                table: "Scenes",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choises_Scenes_SceneId",
                table: "Choises");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Scenes_SceneId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Stories_StoryId",
                table: "Scenes");

            migrationBuilder.DropIndex(
                name: "IX_Contents_SceneId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "SceneId",
                table: "Contents");

            migrationBuilder.AlterColumn<Guid>(
                name: "StoryId",
                table: "Scenes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "Scenes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SceneId",
                table: "Choises",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_ContentId",
                table: "Scenes",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choises_Scenes_SceneId",
                table: "Choises",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Contents_ContentId",
                table: "Scenes",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Stories_StoryId",
                table: "Scenes",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");
        }
    }
}
