﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sandbox.Wordbook.Persistence;

#nullable disable

namespace Sandbox.Wordbook.Persistence.Migrations
{
    [DbContext(typeof(WordbookContext))]
    partial class WordbookContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Sandbox.Wordbook.Domain.Translation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastViewedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SourceLang")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TargetLang")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Word", "SourceLang", "TargetLang")
                        .IsUnique();

                    b.ToTable("Translations", (string)null);
                });

            modelBuilder.Entity("Sandbox.Wordbook.Domain.TranslationResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PartOfSpeech")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Transcription")
                        .HasColumnType("text");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TranslationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TranslationId", "Translation", "PartOfSpeech")
                        .IsUnique();

                    b.ToTable("TranslationResults", (string)null);
                });

            modelBuilder.Entity("Sandbox.Wordbook.Domain.WordbookUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirebaseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("FirebaseId")
                        .IsUnique();

                    b.ToTable("WordbookUsers", (string)null);
                });

            modelBuilder.Entity("Sandbox.Wordbook.Domain.Translation", b =>
                {
                    b.HasOne("Sandbox.Wordbook.Domain.WordbookUser", null)
                        .WithMany("Translations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sandbox.Wordbook.Domain.TranslationResult", b =>
                {
                    b.HasOne("Sandbox.Wordbook.Domain.Translation", null)
                        .WithMany("TranslationResults")
                        .HasForeignKey("TranslationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sandbox.Wordbook.Domain.Translation", b =>
                {
                    b.Navigation("TranslationResults");
                });

            modelBuilder.Entity("Sandbox.Wordbook.Domain.WordbookUser", b =>
                {
                    b.Navigation("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}
