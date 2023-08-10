﻿// <auto-generated />
using System;
using DespesaViagem.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    [DbContext(typeof(DespesaViagemContext))]
    [Migration("20230802125438_Corrigindo-Usuarios")]
    partial class CorrigindoUsuarios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("NumeroCasa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("TipoDeUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);

                    b.HasDiscriminator<string>("TipoDeUsuario").HasValue("Administrador");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.Despesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataDespesa")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescricaoDespesa")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("IdViagem")
                        .HasColumnType("int");

                    b.Property<string>("NomeDespesa")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("TipoDespesa")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("TotalDespesa")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdViagem");

                    b.ToTable("Despesas");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Viagens.Viagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Adiantamento")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal");

                    b.Property<DateTime>("DataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescricaoViagem")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3000)");

                    b.Property<int>("IdFuncionario")
                        .HasColumnType("int");

                    b.Property<string>("NomeViagem")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("StatusViagem")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar");

                    b.Property<decimal>("TotalDespesas")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("IdFuncionario");

                    b.ToTable("Viagens", (string)null);
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Funcionario", b =>
                {
                    b.HasBaseType("DespesaViagem.Shared.Models.Core.Helpers.Usuario");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<int>("IdGestor")
                        .HasColumnType("int");

                    b.Property<int>("IdViagem")
                        .HasColumnType("int");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasIndex("IdGestor");

                    b.ToTable("Usuarios", t =>
                        {
                            t.Property("CPF")
                                .HasColumnName("Funcionario_CPF");
                        });

                    b.HasDiscriminator().HasValue("Funcionario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Gustavo",
                            PasswordHash = "",
                            Sobrenome = "Rueda dos Reis",
                            TipoDeUsuario = "Funcionario",
                            Username = "",
                            CPF = "321.123.321-12",
                            IdGestor = 0,
                            IdViagem = 0,
                            Matricula = "A65SD1ASD"
                        });
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Gestor", b =>
                {
                    b.HasBaseType("DespesaViagem.Shared.Models.Core.Helpers.Usuario");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasDiscriminator().HasValue("Gestor");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaAlimentacao", b =>
                {
                    b.HasBaseType("DespesaViagem.Shared.Models.Despesas.Despesa");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NomeEstabelecimento")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<decimal>("ValorRefeicao")
                        .HasColumnType("decimal(10,2)");

                    b.ToTable("DespesasAlimentacao", (string)null);
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaDeslocamento", b =>
                {
                    b.HasBaseType("DespesaViagem.Shared.Models.Despesas.Despesa");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<long>("Quilometragem")
                        .HasColumnType("bigint");

                    b.Property<decimal>("ValorPorQuilometro")
                        .HasColumnType("decimal(10,2)");

                    b.ToTable("DespesasDeslocamento", (string)null);
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaHospedagem", b =>
                {
                    b.HasBaseType("DespesaViagem.Shared.Models.Despesas.Despesa");

                    b.Property<int>("IdEndereco")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeDias")
                        .HasColumnType("integer");

                    b.Property<decimal>("ValorDiaria")
                        .HasColumnType("decimal(10,2)");

                    b.HasIndex("IdEndereco");

                    b.ToTable("DespesasHospedagem", (string)null);
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaPassagem", b =>
                {
                    b.HasBaseType("DespesaViagem.Shared.Models.Despesas.Despesa");

                    b.Property<string>("Companhia")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime>("DataHoraEmbarque")
                        .HasColumnType("datetime");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Origem")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10,2)");

                    b.ToTable("DespesasPassagem", (string)null);
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.Despesa", b =>
                {
                    b.HasOne("DespesaViagem.Shared.Models.Viagens.Viagem", "Viagem")
                        .WithMany("Despesas")
                        .HasForeignKey("IdViagem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Viagem");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Viagens.Viagem", b =>
                {
                    b.HasOne("DespesaViagem.Shared.Models.Core.Helpers.Funcionario", "Funcionario")
                        .WithMany("Viagens")
                        .HasForeignKey("IdFuncionario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Funcionario", b =>
                {
                    b.HasOne("DespesaViagem.Shared.Models.Core.Helpers.Gestor", "Gestor")
                        .WithMany("Funcionarios")
                        .HasForeignKey("IdGestor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Gestor");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaAlimentacao", b =>
                {
                    b.HasOne("DespesaViagem.Shared.Models.Despesas.Despesa", null)
                        .WithOne()
                        .HasForeignKey("DespesaViagem.Shared.Models.Despesas.DespesaAlimentacao", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaDeslocamento", b =>
                {
                    b.HasOne("DespesaViagem.Shared.Models.Despesas.Despesa", null)
                        .WithOne()
                        .HasForeignKey("DespesaViagem.Shared.Models.Despesas.DespesaDeslocamento", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaHospedagem", b =>
                {
                    b.HasOne("DespesaViagem.Shared.Models.Despesas.Despesa", null)
                        .WithOne()
                        .HasForeignKey("DespesaViagem.Shared.Models.Despesas.DespesaHospedagem", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DespesaViagem.Shared.Models.Core.Helpers.Endereco", "Endereco")
                        .WithMany("DespesasHospedagem")
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Despesas.DespesaPassagem", b =>
                {
                    b.HasOne("DespesaViagem.Shared.Models.Despesas.Despesa", null)
                        .WithOne()
                        .HasForeignKey("DespesaViagem.Shared.Models.Despesas.DespesaPassagem", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Endereco", b =>
                {
                    b.Navigation("DespesasHospedagem");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Viagens.Viagem", b =>
                {
                    b.Navigation("Despesas");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Funcionario", b =>
                {
                    b.Navigation("Viagens");
                });

            modelBuilder.Entity("DespesaViagem.Shared.Models.Core.Helpers.Gestor", b =>
                {
                    b.Navigation("Funcionarios");
                });
#pragma warning restore 612, 618
        }
    }
}
