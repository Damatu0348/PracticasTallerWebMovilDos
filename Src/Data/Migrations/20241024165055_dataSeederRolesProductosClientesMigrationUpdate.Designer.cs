﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Src.Data;

#nullable disable

namespace api.Src.Data.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241024165055_dataSeederRolesProductosClientesMigrationUpdate")]
    partial class dataSeederRolesProductosClientesMigrationUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("ClienteProducto", b =>
                {
                    b.Property<int>("ClientesIdCliente")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductosIdProducto")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClientesIdCliente", "ProductosIdProducto");

                    b.HasIndex("ProductosIdProducto");

                    b.ToTable("ClienteProducto");
                });

            modelBuilder.Entity("api.Src.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Contrasenha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FechaNacimiento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreCliente")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rut")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("TEXT");

                    b.HasKey("IdCliente");

                    b.HasIndex("RoleId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("api.Src.Models.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreProducto")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<int>("Precio")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StockActual")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoProducto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdProducto");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("api.Src.Models.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreRole")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdRole");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ClienteProducto", b =>
                {
                    b.HasOne("api.Src.Models.Cliente", null)
                        .WithMany()
                        .HasForeignKey("ClientesIdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Src.Models.Producto", null)
                        .WithMany()
                        .HasForeignKey("ProductosIdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Src.Models.Cliente", b =>
                {
                    b.HasOne("api.Src.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}