﻿// <auto-generated />
using System;
using BCCAlunos2024.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BCCAlunos2024.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20240902235910_inicial")]
    partial class inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BCCAlunos2024.Models.Aluno", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("aniversario")
                        .HasColumnType("datetime2");

                    b.Property<int>("cursoID")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<int>("periodo")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("cursoID");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("BCCAlunos2024.Models.Atendimento", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("alunoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("dataHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("salaID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("alunoID");

                    b.HasIndex("salaID");

                    b.ToTable("Atendimentos");
                });

            modelBuilder.Entity("BCCAlunos2024.Models.Curso", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.HasKey("id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("BCCAlunos2024.Models.Sala", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("equipamentos")
                        .HasColumnType("int");

                    b.Property<int>("situacao")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Salas");
                });

            modelBuilder.Entity("BCCAlunos2024.Models.Aluno", b =>
                {
                    b.HasOne("BCCAlunos2024.Models.Curso", "curso")
                        .WithMany()
                        .HasForeignKey("cursoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("curso");
                });

            modelBuilder.Entity("BCCAlunos2024.Models.Atendimento", b =>
                {
                    b.HasOne("BCCAlunos2024.Models.Aluno", "aluno")
                        .WithMany()
                        .HasForeignKey("alunoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCCAlunos2024.Models.Sala", "sala")
                        .WithMany()
                        .HasForeignKey("salaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("aluno");

                    b.Navigation("sala");
                });
#pragma warning restore 612, 618
        }
    }
}
