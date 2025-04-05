using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apptienda.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPosggressMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_order_detail_t_order_ordenId",
                table: "t_order_detail");

            migrationBuilder.RenameColumn(
                name: "ordenId",
                table: "t_order_detail",
                newName: "OrdenId");

            migrationBuilder.RenameIndex(
                name: "IX_t_order_detail_ordenId",
                table: "t_order_detail",
                newName: "IX_t_order_detail_OrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_order_detail_t_order_OrdenId",
                table: "t_order_detail",
                column: "OrdenId",
                principalTable: "t_order",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_order_detail_t_order_OrdenId",
                table: "t_order_detail");

            migrationBuilder.RenameColumn(
                name: "OrdenId",
                table: "t_order_detail",
                newName: "ordenId");

            migrationBuilder.RenameIndex(
                name: "IX_t_order_detail_OrdenId",
                table: "t_order_detail",
                newName: "IX_t_order_detail_ordenId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_order_detail_t_order_ordenId",
                table: "t_order_detail",
                column: "ordenId",
                principalTable: "t_order",
                principalColumn: "id");
        }
    }
}
