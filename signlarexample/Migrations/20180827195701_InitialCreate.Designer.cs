﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using signlarexample;

namespace signlarexample.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180827195701_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("signlarexample.Models.PushSubscription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Auth");

                    b.Property<string>("Endpoint");

                    b.Property<string>("P256");

                    b.HasKey("Id");

                    b.ToTable("PushSubscription");
                });
#pragma warning restore 612, 618
        }
    }
}