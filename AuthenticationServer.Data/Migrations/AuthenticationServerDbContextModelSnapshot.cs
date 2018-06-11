﻿// <auto-generated />
using System;
using AuthenticationServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthenticationServer.Data.Migrations
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

            modelBuilder.Entity("AuthenticationServer.Domain.AuthToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("AuthenticationServer.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("LastName");

                    b.Property<string>("Login");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuthenticationServer.Domain.UserAuthTokens", b =>
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

            modelBuilder.Entity("AuthenticationServer.Domain.UserAuthTokens", b =>
                {
                    b.HasOne("AuthenticationServer.Domain.AuthToken", "AuthTokens")
                        .WithMany("UsersAuthTokens")
                        .HasForeignKey("AuthTokenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AuthenticationServer.Domain.User", "Users")
                        .WithMany("UsersAuthTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
