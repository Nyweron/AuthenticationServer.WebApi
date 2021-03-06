﻿// <auto-generated />
using System;
using AuthenticationServer.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthenticationServer.WebApi.Migrations
{
    [DbContext(typeof(AuthenticationServerDbContext))]
    partial class AuthenticationServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.AuthToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.GroupPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<int>("PermissionId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupsPermissions");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("PasswordAnswer")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("PasswordQuestion")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("ResetEmailSent");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.UserAuthToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthTokenId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AuthTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersAuthTokens");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersGroups");
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.GroupPermission", b =>
                {
                    b.HasOne("AuthenticationServer.WebApi.Entities.Group", "Groups")
                        .WithMany("GroupsPermissions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AuthenticationServer.WebApi.Entities.Permission", "Permissions")
                        .WithMany("GroupsPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.Password", b =>
                {
                    b.HasOne("AuthenticationServer.WebApi.Entities.User", "Users")
                        .WithMany("Passwords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.UserAuthToken", b =>
                {
                    b.HasOne("AuthenticationServer.WebApi.Entities.AuthToken", "AuthTokens")
                        .WithMany("UsersAuthTokens")
                        .HasForeignKey("AuthTokenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AuthenticationServer.WebApi.Entities.User", "Users")
                        .WithMany("UsersAuthTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AuthenticationServer.WebApi.Entities.UserGroup", b =>
                {
                    b.HasOne("AuthenticationServer.WebApi.Entities.Group", "Groups")
                        .WithMany("UsersGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AuthenticationServer.WebApi.Entities.User", "Users")
                        .WithMany("UsersGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
