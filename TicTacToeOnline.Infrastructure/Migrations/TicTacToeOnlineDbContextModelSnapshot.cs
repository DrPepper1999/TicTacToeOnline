﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TicTacToeOnline.Infrastructure.Persistence;

#nullable disable

namespace TicTacToeOnline.Infrastructure.Migrations
{
    [DbContext(typeof(TicTacToeOnlineDbContext))]
    partial class TicTacToeOnlineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TicTacToeOnline.Domain.GameAggregate.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PlayerTurn")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("_currentMarkMove")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.PlayerAggregate.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Players", (string)null);
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.RoomAggregate.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.TeamAggregate.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mark")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Teams", (string)null);
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TicTacToeOnline.Infrastructure.Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", (string)null);
                });

            modelBuilder.Entity("TicTacToeOnline.Infrastructure.Persistence.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id", "Name");

                    b.ToTable("OutboxMessageConsumers", (string)null);
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.GameAggregate.Game", b =>
                {
                    b.OwnsOne("TicTacToeOnline.Domain.GameAggregate.Entities.Map", "Map", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("MapId");

                            b1.Property<Guid>("GameId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("CreatedDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Size")
                                .HasMaxLength(16)
                                .HasColumnType("integer");

                            b1.Property<DateTime>("UpdateDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("_fields")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int>("_fillCellCount")
                                .HasColumnType("integer");

                            b1.HasKey("Id", "GameId");

                            b1.HasIndex("GameId")
                                .IsUnique();

                            b1.ToTable("Map", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsMany("TicTacToeOnline.Domain.TeamAggregate.ValueObjects.TeamId", "TeamIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("GameId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("TeamId");

                            b1.HasKey("Id");

                            b1.HasIndex("GameId");

                            b1.ToTable("GameTeamIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.Navigation("Map")
                        .IsRequired();

                    b.Navigation("TeamIds");
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.PlayerAggregate.Player", b =>
                {
                    b.OwnsOne("TicTacToeOnline.Domain.Common.ValueObjects.AverageRating", "AverageRating", b1 =>
                        {
                            b1.Property<Guid>("PlayerId")
                                .HasColumnType("uuid");

                            b1.Property<int>("NumRatings")
                                .HasColumnType("integer");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision");

                            b1.HasKey("PlayerId");

                            b1.ToTable("Players");

                            b1.WithOwner()
                                .HasForeignKey("PlayerId");
                        });

                    b.OwnsMany("TicTacToeOnline.Domain.PlayerAggregate.ValueObjects.ConnectionInfo", "Connections", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<DateTime>("ConnectedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("ConnectionId")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<Guid>("PlayerId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("PlayerId");

                            b1.ToTable("ConnectionInfo", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PlayerId");
                        });

                    b.Navigation("AverageRating")
                        .IsRequired();

                    b.Navigation("Connections");
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.RoomAggregate.Room", b =>
                {
                    b.OwnsOne("TicTacToeOnline.Domain.Common.ValueObjects.GameSetting", "GameSetting", b1 =>
                        {
                            b1.Property<Guid>("RoomId")
                                .HasColumnType("uuid");

                            b1.HasKey("RoomId");

                            b1.ToTable("Rooms");

                            b1.WithOwner()
                                .HasForeignKey("RoomId");
                        });

                    b.OwnsMany("TicTacToeOnline.Domain.PlayerAggregate.ValueObjects.PlayerId", "PlayerIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("RoomId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("PlayerId");

                            b1.HasKey("Id");

                            b1.HasIndex("RoomId");

                            b1.ToTable("RoomPlayerIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RoomId");
                        });

                    b.OwnsMany("TicTacToeOnline.Domain.TeamAggregate.ValueObjects.TeamId", "TeamIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("RoomId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("TeamId");

                            b1.HasKey("Id");

                            b1.HasIndex("RoomId");

                            b1.ToTable("RoomTeamIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RoomId");
                        });

                    b.Navigation("GameSetting")
                        .IsRequired();

                    b.Navigation("PlayerIds");

                    b.Navigation("TeamIds");
                });

            modelBuilder.Entity("TicTacToeOnline.Domain.TeamAggregate.Team", b =>
                {
                    b.OwnsMany("TicTacToeOnline.Domain.PlayerAggregate.ValueObjects.PlayerId", "PlayerIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("TeamId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("PlayerId");

                            b1.HasKey("Id");

                            b1.HasIndex("TeamId");

                            b1.ToTable("TeamPlayerIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TeamId");
                        });

                    b.Navigation("PlayerIds");
                });
#pragma warning restore 612, 618
        }
    }
}
