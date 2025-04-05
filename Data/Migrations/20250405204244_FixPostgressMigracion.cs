﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apptienda.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPostgressMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_pago_t_order_OrdenId",
                table: "t_pago");

            migrationBuilder.DropIndex(
                name: "IX_t_pago_OrdenId",
                table: "t_pago");

            migrationBuilder.DropColumn(
                name: "OrdenId",
                table: "t_pago");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "t_preorden",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "t_order",
                newName: "UserName");

            migrationBuilder.AddColumn<int>(
                name: "pagoId",
                table: "t_order",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_order_pagoId",
                table: "t_order",
                column: "pagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_order_t_pago_pagoId",
                table: "t_order",
                column: "pagoId",
                principalTable: "t_pago",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_order_t_pago_pagoId",
                table: "t_order");

            migrationBuilder.DropIndex(
                name: "IX_t_order_pagoId",
                table: "t_order");

            migrationBuilder.DropColumn(
                name: "pagoId",
                table: "t_order");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "t_preorden",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "t_order",
                newName: "UserID");

            migrationBuilder.AddColumn<int>(
                name: "OrdenId",
                table: "t_pago",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_pago_OrdenId",
                table: "t_pago",
                column: "OrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_pago_t_order_OrdenId",
                table: "t_pago",
                column: "OrdenId",
                principalTable: "t_order",
                principalColumn: "id");
        }
    }
}
