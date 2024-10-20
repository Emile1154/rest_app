﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rest_server.repo;

#nullable disable

namespace rest_server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("rest_server.models.Answer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Msg")
                        .HasColumnType("int");

                    b.Property<int>("msgid")
                        .HasColumnType("int");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("Msg");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("rest_server.models.Msg", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Usr")
                        .HasColumnType("int");

                    b.Property<int>("usrid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Usr");

                    b.ToTable("Msgs");
                });

            modelBuilder.Entity("rest_server.models.Usr", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Usrs");
                });

            modelBuilder.Entity("rest_server.models.Answer", b =>
                {
                    b.HasOne("rest_server.models.Msg", "msg")
                        .WithMany("answers")
                        .HasForeignKey("Msg");

                    b.Navigation("msg");
                });

            modelBuilder.Entity("rest_server.models.Msg", b =>
                {
                    b.HasOne("rest_server.models.Usr", "usr")
                        .WithMany("msgs")
                        .HasForeignKey("Usr");

                    b.Navigation("usr");
                });

            modelBuilder.Entity("rest_server.models.Msg", b =>
                {
                    b.Navigation("answers");
                });

            modelBuilder.Entity("rest_server.models.Usr", b =>
                {
                    b.Navigation("msgs");
                });
#pragma warning restore 612, 618
        }
    }
}
