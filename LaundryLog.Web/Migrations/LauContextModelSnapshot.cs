﻿// <auto-generated />
using System;
using LaundryLog.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LaundryLog.Web.Migrations
{
    [DbContext(typeof(LauContext))]
    partial class LauContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LaundryLog.Web.Models.LauItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand");

                    b.Property<string>("Category");

                    b.Property<string>("Color");

                    b.Property<DateTime>("DateBought");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<double>("Price");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("LauItem");
                });

            modelBuilder.Entity("LaundryLog.Web.Models.LauLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateIn");

                    b.Property<DateTime>("DateOut");

                    b.Property<double>("Price");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("LauLog");
                });

            modelBuilder.Entity("LaundryLog.Web.Models.LauUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LauItemId");

                    b.Property<int>("LauLogId");

                    b.Property<string>("Notes");

                    b.Property<int>("Quantity");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("LauItemId");

                    b.HasIndex("LauLogId");

                    b.ToTable("LauUnit");
                });

            modelBuilder.Entity("LaundryLog.Web.Models.LauUnit", b =>
                {
                    b.HasOne("LaundryLog.Web.Models.LauItem", "LauItem")
                        .WithMany("LauUnits")
                        .HasForeignKey("LauItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LaundryLog.Web.Models.LauLog", "LauLog")
                        .WithMany("LauUnits")
                        .HasForeignKey("LauLogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
