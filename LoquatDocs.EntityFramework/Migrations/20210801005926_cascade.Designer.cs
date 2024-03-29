﻿// <auto-generated />
using System;
using LoquatDocs.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoquatDocs.EntityFramework.Migrations
{
    [DbContext(typeof(LoquatDocsDbContext))]
    [Migration("20210801005926_cascade")]
    partial class cascade
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Alias", b =>
                {
                    b.Property<string>("AliasName")
                        .HasColumnType("TEXT");

                    b.Property<string>("AliasGroupName")
                        .HasColumnType("TEXT");

                    b.HasKey("AliasName", "AliasGroupName");

                    b.HasIndex("AliasGroupName");

                    b.ToTable("Aliases");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.AliasGroup", b =>
                {
                    b.Property<string>("AliasGroupName")
                        .HasColumnType("TEXT");

                    b.HasKey("AliasGroupName");

                    b.ToTable("AliasGroups");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Document", b =>
                {
                    b.Property<string>("DocumentPath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DocumentAdded")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Groupname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DocumentPath");

                    b.HasIndex("Groupname");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Group", b =>
                {
                    b.Property<string>("Groupname")
                        .HasColumnType("TEXT");

                    b.HasKey("Groupname");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Groupname = "Miscellaneous"
                        });
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Invoice", b =>
                {
                    b.Property<string>("DocumentPath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("INTEGER");

                    b.HasKey("DocumentPath");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DocumentPath")
                        .HasColumnType("TEXT");

                    b.HasKey("TagId");

                    b.HasIndex("DocumentPath");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Alias", b =>
                {
                    b.HasOne("LoquatDocs.EntityFramework.Model.AliasGroup", "AliasGroup")
                        .WithMany("Aliases")
                        .HasForeignKey("AliasGroupName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AliasGroup");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Document", b =>
                {
                    b.HasOne("LoquatDocs.EntityFramework.Model.Group", "Group")
                        .WithMany("Documents")
                        .HasForeignKey("Groupname");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Invoice", b =>
                {
                    b.HasOne("LoquatDocs.EntityFramework.Model.Document", "Document")
                        .WithMany("Invoices")
                        .HasForeignKey("DocumentPath")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Tag", b =>
                {
                    b.HasOne("LoquatDocs.EntityFramework.Model.Document", "Document")
                        .WithMany("Tags")
                        .HasForeignKey("DocumentPath")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Document");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.AliasGroup", b =>
                {
                    b.Navigation("Aliases");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Document", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("LoquatDocs.EntityFramework.Model.Group", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
