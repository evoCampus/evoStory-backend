using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoStory.Database.Migrations
{
    /// <inheritdoc />
    public partial class newItemsAndLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Choises_RequiredItemId",
                table: "Choises",
                column: "RequiredItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Choises_RewardItemId",
                table: "Choises",
                column: "RewardItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choises_Items_RequiredItemId",
                table: "Choises",
                column: "RequiredItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choises_Items_RewardItemId",
                table: "Choises",
                column: "RewardItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choises_Items_RequiredItemId",
                table: "Choises");

            migrationBuilder.DropForeignKey(
                name: "FK_Choises_Items_RewardItemId",
                table: "Choises");

            migrationBuilder.DropIndex(
                name: "IX_Choises_RequiredItemId",
                table: "Choises");

            migrationBuilder.DropIndex(
                name: "IX_Choises_RewardItemId",
                table: "Choises");
        }
    }
}
