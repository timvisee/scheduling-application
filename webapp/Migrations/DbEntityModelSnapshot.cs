﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using webapp.Models;
using webapp.Types;

namespace webapp.Migrations
{
    [DbContext(typeof(DbEntity))]
    partial class DbEntityModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("webapp.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateStart");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("EventId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("webapp.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Participant");

                    b.HasKey("GroupId");

                    b.ToTable("groups");
                });

            modelBuilder.Entity("webapp.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("LocationId");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("webapp.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Participant");

                    b.HasKey("ParticipantId");

                    b.HasIndex("Participant");

                    b.ToTable("participants");
                });

            modelBuilder.Entity("webapp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("FirstName");

                    b.Property<string>("Infix");

                    b.Property<string>("LastName");

                    b.Property<string>("Locale");

                    b.Property<int>("Participant");

                    b.Property<int>("Role");

                    b.Property<int>("Type");

                    b.Property<int?>("User");

                    b.HasKey("UserId");

                    b.HasIndex("User");

                    b.ToTable("users");
                });

            modelBuilder.Entity("webapp.Models.Participant", b =>
                {
                    b.HasOne("webapp.Models.Event")
                        .WithMany("Participants")
                        .HasForeignKey("Participant");
                });

            modelBuilder.Entity("webapp.Models.User", b =>
                {
                    b.HasOne("webapp.Models.Group")
                        .WithMany("Users")
                        .HasForeignKey("User");
                });
#pragma warning restore 612, 618
        }
    }
}
