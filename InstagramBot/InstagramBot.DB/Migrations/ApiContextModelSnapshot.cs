﻿// <auto-generated />
using System;
using InstagramBot.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InstagramBot.DB.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InstagramBot.DB.Entities.AppUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Login");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Login = "Admin",
                            Modified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "123456"
                        });
                });

            modelBuilder.Entity("InstagramBot.DB.Entities.InstagramUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountStatus");

                    b.Property<long>("AppUserId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Login");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Session");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("InstagramUsers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccountStatus = 0,
                            AppUserId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Login = "belarus.here",
                            Modified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "Gfhjkm63934710"
                        });
                });

            modelBuilder.Entity("InstagramBot.DB.Entities.QueueItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long>("InstagramUserId");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Parameters");

                    b.Property<int>("QueueStatus");

                    b.HasKey("Id");

                    b.HasIndex("InstagramUserId");

                    b.ToTable("QueueItems");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InstagramUserId = 1L,
                            Modified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            QueueType = 1
                        });
                });

            modelBuilder.Entity("InstagramBot.DB.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("InstagramBot.DB.Entities.User2Roles", b =>
                {
                    b.Property<long>("RoleId");

                    b.Property<long>("UserId");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("User2Roles");
                });

            modelBuilder.Entity("InstagramBot.DB.Entities.InstagramUser", b =>
                {
                    b.HasOne("InstagramBot.DB.Entities.AppUser", "AppUser")
                        .WithMany("InstagramUsers")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InstagramBot.DB.Entities.QueueItem", b =>
                {
                    b.HasOne("InstagramBot.DB.Entities.InstagramUser", "InstagramUser")
                        .WithMany("QueueItems")
                        .HasForeignKey("InstagramUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InstagramBot.DB.Entities.User2Roles", b =>
                {
                    b.HasOne("InstagramBot.DB.Entities.Role", "Role")
                        .WithMany("User2Roles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InstagramBot.DB.Entities.AppUser", "User")
                        .WithMany("User2Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
