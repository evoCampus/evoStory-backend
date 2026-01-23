using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoStory.Database.Migrations
{
    /// <inheritdoc />
    public partial class TestingFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Choises_NextSceneId",
                table: "Choises",
                column: "NextSceneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choises_Scenes_NextSceneId",
                table: "Choises",
                column: "NextSceneId",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choises_Scenes_NextSceneId",
                table: "Choises");

            migrationBuilder.DropIndex(
                name: "IX_Choises_NextSceneId",
                table: "Choises");
        }
    }
}
