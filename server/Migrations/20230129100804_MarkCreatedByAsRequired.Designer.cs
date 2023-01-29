﻿// <auto-generated />
using System;
using Dsj2TournamentsServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dsj2TournamentsServer.Migrations
{
    [DbContext(typeof(Dsj2TournamentsServerDbContext))]
    [Migration("20230129100804_MarkCreatedByAsRequired")]
    partial class MarkCreatedByAsRequired
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dsj2TournamentsServer.Models.Hill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_hills");

                    b.ToTable("hills", (string)null);
                });

            modelBuilder.Entity("Dsj2TournamentsServer.Models.Jump", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Crash")
                        .HasColumnType("boolean")
                        .HasColumnName("crash");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<int>("HillId")
                        .HasColumnType("integer")
                        .HasColumnName("hill_id");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric")
                        .HasColumnName("length");

                    b.Property<string>("Player")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("player");

                    b.Property<decimal>("Points")
                        .HasColumnType("numeric")
                        .HasColumnName("points");

                    b.Property<string>("ReplayCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("replay_code");

                    b.Property<string>("TournamentCode")
                        .HasColumnType("text")
                        .HasColumnName("tournament_code");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("integer")
                        .HasColumnName("tournament_id");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_jumps");

                    b.HasIndex("HillId")
                        .HasDatabaseName("ix_jumps_hill_id");

                    b.HasIndex("TournamentId")
                        .HasDatabaseName("ix_jumps_tournament_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_jumps_user_id");

                    b.ToTable("jumps", (string)null);
                });

            modelBuilder.Entity("Dsj2TournamentsServer.Models.Tournament.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer")
                        .HasColumnName("created_by_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("HillId")
                        .HasColumnType("integer")
                        .HasColumnName("hill_id");

                    b.Property<int?>("SettingsId")
                        .HasColumnType("integer")
                        .HasColumnName("settings_id");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Id")
                        .HasName("pk_tournaments");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_tournaments_created_by_id");

                    b.HasIndex("HillId")
                        .HasDatabaseName("ix_tournaments_hill_id");

                    b.HasIndex("SettingsId")
                        .HasDatabaseName("ix_tournaments_settings_id");

                    b.ToTable("tournaments", (string)null);
                });

            modelBuilder.Entity("Dsj2TournamentsServer.Models.Tournament.TournamentSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("LiveBoard")
                        .HasColumnType("boolean")
                        .HasColumnName("live_board");

                    b.HasKey("Id")
                        .HasName("pk_tournament_settings");

                    b.ToTable("tournament_settings", (string)null);
                });

            modelBuilder.Entity("Dsj2TournamentsServer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("user_id");

                    b.Property<string>("Username")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Dsj2TournamentsServer.Models.Jump", b =>
                {
                    b.HasOne("Dsj2TournamentsServer.Models.Hill", "Hill")
                        .WithMany()
                        .HasForeignKey("HillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_jumps_hills_hill_id");

                    b.HasOne("Dsj2TournamentsServer.Models.Tournament.Tournament", null)
                        .WithMany("Jumps")
                        .HasForeignKey("TournamentId")
                        .HasConstraintName("fk_jumps_tournaments_tournament_id");

                    b.HasOne("Dsj2TournamentsServer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_jumps_users_user_id");

                    b.Navigation("Hill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dsj2TournamentsServer.Models.Tournament.Tournament", b =>
                {
                    b.HasOne("Dsj2TournamentsServer.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tournaments_users_created_by_id");

                    b.HasOne("Dsj2TournamentsServer.Models.Hill", "Hill")
                        .WithMany()
                        .HasForeignKey("HillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tournaments_hills_hill_id");

                    b.HasOne("Dsj2TournamentsServer.Models.Tournament.TournamentSettings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId")
                        .HasConstraintName("fk_tournaments_tournament_settings_settings_id");

                    b.Navigation("CreatedBy");

                    b.Navigation("Hill");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Dsj2TournamentsServer.Models.Tournament.Tournament", b =>
                {
                    b.Navigation("Jumps");
                });
#pragma warning restore 612, 618
        }
    }
}
