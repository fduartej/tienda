using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apptienda.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix2PosggressMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_order_t_pago_pagoId",
                table: "t_order");

            migrationBuilder.RenameColumn(
                name: "pagoId",
                table: "t_order",
                newName: "PagoId");

            migrationBuilder.RenameIndex(
                name: "IX_t_order_pagoId",
                table: "t_order",
                newName: "IX_t_order_PagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_order_t_pago_PagoId",
                table: "t_order",
                column: "PagoId",
                principalTable: "t_pago",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_order_t_pago_PagoId",
                table: "t_order");

            migrationBuilder.RenameColumn(
                name: "PagoId",
                table: "t_order",
                newName: "pagoId");

            migrationBuilder.RenameIndex(
                name: "IX_t_order_PagoId",
                table: "t_order",
                newName: "IX_t_order_pagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_order_t_pago_pagoId",
                table: "t_order",
                column: "pagoId",
                principalTable: "t_pago",
                principalColumn: "id");
        }
    }
}
