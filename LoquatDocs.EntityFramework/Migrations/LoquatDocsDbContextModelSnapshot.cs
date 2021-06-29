﻿// <auto-generated />
using LoquatDocs.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoquatDocs.EntityFramework.Migrations
{
    [DbContext(typeof(LoquatDocsDbContext))]
    partial class LoquatDocsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Document", b =>
                {
                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Path");

                    b.ToTable("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
