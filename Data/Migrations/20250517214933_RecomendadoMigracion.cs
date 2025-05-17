using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apptienda.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecomendadoMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "t_rating",
                newName: "UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecommended",
                table: "t_producto",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Cuotas",
                table: "t_pago",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "t_pago",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "t_pago",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecommended",
                table: "t_producto");

            migrationBuilder.DropColumn(
                name: "Cuotas",
                table: "t_pago");

            migrationBuilder.DropColumn(
                name: "DNI",
                table: "t_pago");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "t_pago");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "t_rating",
                newName: "UserName");
        }
    }
}
