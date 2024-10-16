﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(LipstickManagementContext))]
    [Migration("20241009061556_UpdateCategory")]
    partial class UpdateCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BussinessObject.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<bool?>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BussinessObject.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("BussinessObject.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BussinessObject.Customization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EngravingText")
                        .HasColumnType("text");

                    b.Property<int?>("LipstickId")
                        .HasColumnType("integer");

                    b.Property<string>("QrCodeUrl")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LipstickId");

                    b.ToTable("Customizations");
                });

            modelBuilder.Entity("BussinessObject.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LipstickId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LipstickId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("BussinessObject.ImageURL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LipstickId")
                        .HasColumnType("integer");

                    b.Property<string>("URL")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LipstickId");

                    b.ToTable("ImageURLs");
                });

            modelBuilder.Entity("BussinessObject.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("BussinessObject.Lipstick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("StockQuantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Usage")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("Lipsticks");
                });

            modelBuilder.Entity("BussinessObject.LipstickIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<int>("LipstickId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("LipstickId");

                    b.ToTable("LipstickIngredients");
                });

            modelBuilder.Entity("BussinessObject.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int?>("AddressId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LipstickId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("text");

                    b.Property<string>("ShippedAddress")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("ShippedFee")
                        .HasColumnType("double precision");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AddressId");

                    b.HasIndex("LipstickId");

                    b.HasIndex("PaymentId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("BussinessObject.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Method")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BussinessObject.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RoleName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BussinessObject.Account", b =>
                {
                    b.HasOne("BussinessObject.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BussinessObject.Address", b =>
                {
                    b.HasOne("BussinessObject.Account", "Account")
                        .WithMany("Addresses")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BussinessObject.Customization", b =>
                {
                    b.HasOne("BussinessObject.Lipstick", "Lipstick")
                        .WithMany("Customizations")
                        .HasForeignKey("LipstickId");

                    b.Navigation("Lipstick");
                });

            modelBuilder.Entity("BussinessObject.Feedback", b =>
                {
                    b.HasOne("BussinessObject.Account", "Account")
                        .WithMany("Feedbacks")
                        .HasForeignKey("AccountId");

                    b.HasOne("BussinessObject.Lipstick", "Lipstick")
                        .WithMany("Feedbacks")
                        .HasForeignKey("LipstickId");

                    b.Navigation("Account");

                    b.Navigation("Lipstick");
                });

            modelBuilder.Entity("BussinessObject.ImageURL", b =>
                {
                    b.HasOne("BussinessObject.Lipstick", "Lipstick")
                        .WithMany("ImageURLs")
                        .HasForeignKey("LipstickId");

                    b.Navigation("Lipstick");
                });

            modelBuilder.Entity("BussinessObject.Lipstick", b =>
                {
                    b.HasOne("BussinessObject.Category", "Category")
                        .WithOne("Lipstick")
                        .HasForeignKey("BussinessObject.Lipstick", "CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BussinessObject.LipstickIngredient", b =>
                {
                    b.HasOne("BussinessObject.Ingredient", "Ingredient")
                        .WithMany("LipstickIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BussinessObject.Lipstick", "Lipstick")
                        .WithMany("LipstickIngredients")
                        .HasForeignKey("LipstickId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Lipstick");
                });

            modelBuilder.Entity("BussinessObject.OrderDetail", b =>
                {
                    b.HasOne("BussinessObject.Account", "Account")
                        .WithMany("OrderDetails")
                        .HasForeignKey("AccountId");

                    b.HasOne("BussinessObject.Address", "Address")
                        .WithMany("OrderDetails")
                        .HasForeignKey("AddressId");

                    b.HasOne("BussinessObject.Lipstick", "Lipstick")
                        .WithMany("OrderDetails")
                        .HasForeignKey("LipstickId");

                    b.HasOne("BussinessObject.Payment", "Payment")
                        .WithMany("OrderDetails")
                        .HasForeignKey("PaymentId");

                    b.Navigation("Account");

                    b.Navigation("Address");

                    b.Navigation("Lipstick");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("BussinessObject.Account", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Feedbacks");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BussinessObject.Address", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BussinessObject.Category", b =>
                {
                    b.Navigation("Lipstick");
                });

            modelBuilder.Entity("BussinessObject.Ingredient", b =>
                {
                    b.Navigation("LipstickIngredients");
                });

            modelBuilder.Entity("BussinessObject.Lipstick", b =>
                {
                    b.Navigation("Customizations");

                    b.Navigation("Feedbacks");

                    b.Navigation("ImageURLs");

                    b.Navigation("LipstickIngredients");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BussinessObject.Payment", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BussinessObject.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
