﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TuLote.Data;

#nullable disable

namespace TuLote.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TuLote.Models.Barrio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Localidad_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Localidad_Id");

                    b.ToTable("Barrios");
                });

            modelBuilder.Entity("TuLote.Models.Localidad", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("LocalidadId")
                        .HasColumnType("bigint");

                    b.Property<int>("Municipio_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocalidadId");

                    b.HasIndex("Municipio_Id");

                    b.ToTable("Localidades");
                });

            modelBuilder.Entity("TuLote.Models.Lote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Barrio_Id")
                        .HasColumnType("int");

                    b.Property<bool>("Disponible")
                        .HasColumnType("bit");

                    b.Property<string>("Etapa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Metros")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Orientacion")
                        .HasColumnType("int");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Barrio_Id");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("TuLote.Models.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Provincia_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("Provincia_Id");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("TuLote.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProvinciaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("Provincias");
                });

            modelBuilder.Entity("TuLote.Models.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Lote_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Lote_Id");

                    b.ToTable("Tipos");
                });

            modelBuilder.Entity("TuLote.Models.Barrio", b =>
                {
                    b.HasOne("TuLote.Models.Localidad", "Localidad")
                        .WithMany()
                        .HasForeignKey("Localidad_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Localidad");
                });

            modelBuilder.Entity("TuLote.Models.Localidad", b =>
                {
                    b.HasOne("TuLote.Models.Localidad", null)
                        .WithMany("localidades")
                        .HasForeignKey("LocalidadId");

                    b.HasOne("TuLote.Models.Municipio", "Municipio")
                        .WithMany("Localidades")
                        .HasForeignKey("Municipio_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipio");
                });

            modelBuilder.Entity("TuLote.Models.Lote", b =>
                {
                    b.HasOne("TuLote.Models.Barrio", "Barrio")
                        .WithMany()
                        .HasForeignKey("Barrio_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barrio");
                });

            modelBuilder.Entity("TuLote.Models.Municipio", b =>
                {
                    b.HasOne("TuLote.Models.Municipio", null)
                        .WithMany("Municipios")
                        .HasForeignKey("MunicipioId");

                    b.HasOne("TuLote.Models.Provincia", "Provincia")
                        .WithMany("Municipios")
                        .HasForeignKey("Provincia_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provincia");
                });

            modelBuilder.Entity("TuLote.Models.Provincia", b =>
                {
                    b.HasOne("TuLote.Models.Provincia", null)
                        .WithMany("Provincias")
                        .HasForeignKey("ProvinciaId");
                });

            modelBuilder.Entity("TuLote.Models.Tipo", b =>
                {
                    b.HasOne("TuLote.Models.Lote", "Lote")
                        .WithMany()
                        .HasForeignKey("Lote_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lote");
                });

            modelBuilder.Entity("TuLote.Models.Localidad", b =>
                {
                    b.Navigation("localidades");
                });

            modelBuilder.Entity("TuLote.Models.Municipio", b =>
                {
                    b.Navigation("Localidades");

                    b.Navigation("Municipios");
                });

            modelBuilder.Entity("TuLote.Models.Provincia", b =>
                {
                    b.Navigation("Municipios");

                    b.Navigation("Provincias");
                });
#pragma warning restore 612, 618
        }
    }
}
