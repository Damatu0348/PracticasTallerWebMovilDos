using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Src.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Productos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contrasenha",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreCliente",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 225,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Contrasenha",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NombreCliente",
                table: "AspNetUsers");
        }
    }
}
