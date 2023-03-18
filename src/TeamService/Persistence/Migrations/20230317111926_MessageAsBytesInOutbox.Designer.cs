﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeamService.Persistence;

#nullable disable

namespace TeamService.Persistence.Migrations
{
    [DbContext(typeof(TeamContext))]
    [Migration("20230317111926_MessageAsBytesInOutbox")]
    partial class MessageAsBytesInOutbox
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ConsumerMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ConsumerName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("consumer_name");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("topic_name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("value");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_inbox");

                    b.HasIndex("ConsumerName")
                        .HasDatabaseName("ix_inbox_consumer_name");

                    b.HasIndex("StatusId")
                        .HasDatabaseName("ix_inbox_status_id");

                    b.ToTable("inbox", "event_sourcing");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ConsumerMessageHeader", b =>
                {
                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_id");

                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("MessageId", "Key")
                        .HasName("pk_inbox_message_headers");

                    b.ToTable("inbox_message_headers", "event_sourcing");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.MessageStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_message_statuses");

                    b.ToTable("message_statuses", "event_sourcing");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9027), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Не задан",
                            UpdatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9027), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9649), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Подготовлено к отправке в топик",
                            UpdatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9649), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9651), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Успешно отправлено в топик",
                            UpdatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9651), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9651), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Успешно обработано",
                            UpdatedAt = new DateTimeOffset(new DateTime(2023, 3, 17, 11, 19, 26, 411, DateTimeKind.Unspecified).AddTicks(9651), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        });
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ProducerMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<string>("ProducerName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("producer_name");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("topic_name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("value");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_outbox");

                    b.HasIndex("ProducerName")
                        .HasDatabaseName("ix_outbox_producer_name");

                    b.HasIndex("StatusId")
                        .HasDatabaseName("ix_outbox_status_id");

                    b.ToTable("outbox", "event_sourcing");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ProducerMessageHeader", b =>
                {
                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_id");

                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("MessageId", "Key")
                        .HasName("pk_outbox_message_headers");

                    b.ToTable("outbox_message_headers", "event_sourcing");
                });

            modelBuilder.Entity("TeamService.Persistence.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_games");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_games_name");

                    b.ToTable("games", "games");
                });

            modelBuilder.Entity("TeamService.Persistence.Entities.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_participants");

                    b.HasIndex("Nickname")
                        .IsUnique()
                        .HasDatabaseName("ix_participants_nickname");

                    b.ToTable("participants", "teams");
                });

            modelBuilder.Entity("TeamService.Persistence.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid")
                        .HasColumnName("game_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_teams");

                    b.HasIndex("GameId")
                        .HasDatabaseName("ix_teams_game_id");

                    b.ToTable("teams", "teams");
                });

            modelBuilder.Entity("TeamService.Persistence.Entities.TeamParticipant", b =>
                {
                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid")
                        .HasColumnName("team_id");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uuid")
                        .HasColumnName("participant_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int>("TeamRoleId")
                        .HasColumnType("integer")
                        .HasColumnName("team_role_id");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("TeamId", "ParticipantId")
                        .HasName("pk_teams_participants");

                    b.HasIndex("ParticipantId")
                        .HasDatabaseName("ix_teams_participants_participant_id");

                    b.HasIndex("TeamRoleId")
                        .HasDatabaseName("ix_teams_participants_team_role_id");

                    b.ToTable("teams_participants", "teams");
                });

            modelBuilder.Entity("TeamService.Persistence.Entities.TeamRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_team_roles");

                    b.ToTable("team_roles", "teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Игрок",
                            UpdatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Капитан",
                            UpdatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Тренер",
                            UpdatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Менеджер",
                            UpdatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = 0u
                        });
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ConsumerMessage", b =>
                {
                    b.HasOne("Shared.Infrastructure.EventSourcing.Persistence.Entities.MessageStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_inbox_message_status_status_id");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ConsumerMessageHeader", b =>
                {
                    b.HasOne("Shared.Infrastructure.EventSourcing.Persistence.Entities.ConsumerMessage", "Message")
                        .WithMany("Headers")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_inbox_message_headers_inbox_message_id");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ProducerMessage", b =>
                {
                    b.HasOne("Shared.Infrastructure.EventSourcing.Persistence.Entities.MessageStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_outbox_message_statuses_status_id");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ProducerMessageHeader", b =>
                {
                    b.HasOne("Shared.Infrastructure.EventSourcing.Persistence.Entities.ProducerMessage", "Message")
                        .WithMany("Headers")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_outbox_message_headers_outbox_message_id");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("TeamService.Persistence.Entities.Team", b =>
                {
                    b.HasOne("TeamService.Persistence.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_teams_game_game_id");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("TeamService.Persistence.Entities.TeamParticipant", b =>
                {
                    b.HasOne("TeamService.Persistence.Entities.Participant", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_teams_participants_participants_participant_id");

                    b.HasOne("TeamService.Persistence.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_teams_participants_teams_team_id");

                    b.HasOne("TeamService.Persistence.Entities.TeamRole", "TeamRole")
                        .WithMany()
                        .HasForeignKey("TeamRoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_teams_participants_teams_roles_team_role_id");

                    b.Navigation("Participant");

                    b.Navigation("Team");

                    b.Navigation("TeamRole");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ConsumerMessage", b =>
                {
                    b.Navigation("Headers");
                });

            modelBuilder.Entity("Shared.Infrastructure.EventSourcing.Persistence.Entities.ProducerMessage", b =>
                {
                    b.Navigation("Headers");
                });
#pragma warning restore 612, 618
        }
    }
}
