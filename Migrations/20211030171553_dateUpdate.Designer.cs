﻿// <auto-generated />
using System;
using Atarefado.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Atarefado.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211030171553_dateUpdate")]
    partial class dateUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("Atarefado.Models.Tarefa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("data")
                        .HasColumnType("TEXT");

                    b.Property<string>("descricao")
                        .HasColumnType("TEXT");

                    b.Property<bool>("flag")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
