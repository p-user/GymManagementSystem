﻿// <auto-generated />
using System;
using Membership.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Membership.Data.Migrations
{
    [DbContext(typeof(MembershipDbContext))]
    [Migration("20250313102715_Discount_entity_added")]
    partial class Discount_entity_added
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Membership")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DiscountMembershipPlan", b =>
                {
                    b.Property<Guid>("ApplicablePlansId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiscountsApplicableCode")
                        .HasColumnType("varchar(50)");

                    b.HasKey("ApplicablePlansId", "DiscountsApplicableCode");

                    b.HasIndex("DiscountsApplicableCode");

                    b.ToTable("DiscountMembershipPlan", "Membership");
                });

            modelBuilder.Entity("Membership.Models.Discount", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("AppliesToAllPlans")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<int>("UsageCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("UsageLimit")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.ToTable("Discounts", "Membership");
                });

            modelBuilder.Entity("Membership.Models.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthenticationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Members", "Membership");
                });

            modelBuilder.Entity("Membership.Models.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscountCode")
                        .HasColumnType("varchar(50)");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GymMemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MembershipEndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MembershipPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("MembershipStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPricePayed")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("VisitsRemaining")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCode");

                    b.HasIndex("GymMemberId");

                    b.HasIndex("MembershipPlanId");

                    b.ToTable("Memberships", "Membership");
                });

            modelBuilder.Entity("Membership.Models.MembershipPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DurationInMonths")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxVisitsPerWeek")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("MembershipPlans", "Membership");
                });

            modelBuilder.Entity("DiscountMembershipPlan", b =>
                {
                    b.HasOne("Membership.Models.MembershipPlan", null)
                        .WithMany()
                        .HasForeignKey("ApplicablePlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Models.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountsApplicableCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Membership.Models.Membership", b =>
                {
                    b.HasOne("Membership.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountCode");

                    b.HasOne("Membership.Models.Member", "GymMember")
                        .WithMany("Memberships")
                        .HasForeignKey("GymMemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Membership.Models.MembershipPlan", "MembershipPlan")
                        .WithMany("Memberships")
                        .HasForeignKey("MembershipPlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("GymMember");

                    b.Navigation("MembershipPlan");
                });

            modelBuilder.Entity("Membership.Models.Member", b =>
                {
                    b.Navigation("Memberships");
                });

            modelBuilder.Entity("Membership.Models.MembershipPlan", b =>
                {
                    b.Navigation("Memberships");
                });
#pragma warning restore 612, 618
        }
    }
}
