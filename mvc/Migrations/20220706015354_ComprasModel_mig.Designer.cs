﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using mvc.Models;

#nullable disable

namespace mvc.Migrations
{
    [DbContext(typeof(Conexao))]
    [Migration("20220706015354_ComprasModel_mig")]
    partial class ComprasModel_mig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("mvc.Models.ComprasModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("protocolo");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long?>("cpf")
                        .HasColumnType("bigint")
                        .HasColumnName("cpf");

                    b.Property<string>("dt_compra")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("dt_compra");

                    b.Property<string>("dt_evento")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("dt_evento");

                    b.Property<string>("evento")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("evento");

                    b.Property<int>("id_evento")
                        .HasColumnType("integer")
                        .HasColumnName("id_evento");

                    b.Property<int>("ingressos")
                        .HasColumnType("integer")
                        .HasColumnName("qtd_ingressos");

                    b.Property<string>("pagamento")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("pagamento");

                    b.Property<int>("parcelas")
                        .HasColumnType("integer")
                        .HasColumnName("parcelas");

                    b.Property<double>("preco_total")
                        .HasColumnType("double precision")
                        .HasColumnName("preco_total");

                    b.HasKey("Id");

                    b.ToTable("db_compras");
                });

            modelBuilder.Entity("mvc.Models.SessaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("assentos")
                        .HasColumnType("integer")
                        .HasColumnName("assentos");

                    b.Property<int>("comprados")
                        .HasColumnType("integer")
                        .HasColumnName("comprados");

                    b.Property<string>("data")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<string>("evento")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("evento");

                    b.Property<string>("hr_fim")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("fim");

                    b.Property<string>("hr_inicio")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("inicio");

                    b.Property<string>("local")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("local");

                    b.Property<double>("preco")
                        .HasColumnType("double precision")
                        .HasColumnName("preco");

                    b.HasKey("Id");

                    b.ToTable("db_sessoes");
                });

            modelBuilder.Entity("mvc.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long?>("cpf")
                        .HasColumnType("bigint")
                        .HasColumnName("cpf");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("nascimento")
                        .HasColumnType("text")
                        .HasColumnName("nascimento");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Senha");

                    b.Property<char?>("sexo")
                        .HasColumnType("character(1)")
                        .HasColumnName("sexo");

                    b.Property<string>("tipo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Tipo");

                    b.HasKey("Id");

                    b.ToTable("db_usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}