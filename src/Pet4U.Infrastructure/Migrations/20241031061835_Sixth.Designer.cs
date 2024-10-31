﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pet4U.Infrastructure;

#nullable disable

namespace Pet4U.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241031061835_Sixth")]
    partial class Sixth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("core")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Pet4U.Domain.PetManagement.AgregateRoot.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("address");

                    b.Property<DateOnly?>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("breed");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("color");

                    b.Property<DateOnly>("CreateDate")
                        .HasColumnType("date")
                        .HasColumnName("create_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Health")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("health");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<bool?>("IsNeutered")
                        .HasColumnType("boolean")
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nickname");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("phone");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("species");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid?>("volunteer_Id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("PetData", "Pet4U.Domain.PetManagement.AgregateRoot.Pet.PetData#PetData", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("pet_data_breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("pet_data_species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_Id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", "core");
                });

            modelBuilder.Entity("Pet4U.Domain.PetManagement.AgregateRoot.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<int?>("Experience")
                        .HasColumnType("integer")
                        .HasColumnName("experience");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Pet4U.Domain.PetManagement.AgregateRoot.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "Pet4U.Domain.PetManagement.AgregateRoot.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("last_name");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("midle_name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "Pet4U.Domain.PetManagement.AgregateRoot.Volunteer.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("phone");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", "core");
                });

            modelBuilder.Entity("Pet4U.Domain.SpeciesManagement.AgregateRoot.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", "core");
                });

            modelBuilder.Entity("Pet4U.Domain.SpeciesManagement.ValueObject.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breed");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breed_species_id");

                    b.ToTable("breed", "core");
                });

            modelBuilder.Entity("Pet4U.Domain.Volunteers.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<Guid?>("PetId")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_pet_photo");

                    b.HasIndex("PetId")
                        .HasDatabaseName("ix_pet_photo_pet_id");

                    b.ToTable("pet_photo", "core");
                });

            modelBuilder.Entity("Pet4U.Domain.PetManagement.AgregateRoot.Pet", b =>
                {
                    b.HasOne("Pet4U.Domain.PetManagement.AgregateRoot.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");
                });

            modelBuilder.Entity("Pet4U.Domain.PetManagement.AgregateRoot.Volunteer", b =>
                {
                    b.OwnsOne("Pet4U.Domain.ValueObjects.SocialNetworks", "SocialNetworks", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("volunteer_id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers", "core");

                            b1.ToJson("social_networks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_volunteer_id");

                            b1.OwnsMany("Pet4U.Domain.Volunteers.SocialNetwork", "Data", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworksVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("SocialNetworksVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers", "core");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworksVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_networks_volunteer_id");
                                });

                            b1.Navigation("Data");
                        });

                    b.OwnsOne("Pet4U.Domain.Volunteers.PaymentInfos", "PaymentInfos", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("volunteer_id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers", "core");

                            b1.ToJson("payment_infos");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_volunteer_id");

                            b1.OwnsMany("Pet4U.Domain.Volunteers.PaymentInfo", "Data", b2 =>
                                {
                                    b2.Property<Guid>("PaymentInfosVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("PaymentInfosVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers", "core");

                                    b2.WithOwner()
                                        .HasForeignKey("PaymentInfosVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_payment_infos_volunteer_id");
                                });

                            b1.Navigation("Data");
                        });

                    b.Navigation("PaymentInfos");

                    b.Navigation("SocialNetworks");
                });

            modelBuilder.Entity("Pet4U.Domain.SpeciesManagement.ValueObject.Breed", b =>
                {
                    b.HasOne("Pet4U.Domain.SpeciesManagement.AgregateRoot.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .HasConstraintName("fk_breed_species_species_id");
                });

            modelBuilder.Entity("Pet4U.Domain.Volunteers.PetPhoto", b =>
                {
                    b.HasOne("Pet4U.Domain.PetManagement.AgregateRoot.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("PetId")
                        .HasConstraintName("fk_pet_photo_pets_pet_id");
                });

            modelBuilder.Entity("Pet4U.Domain.PetManagement.AgregateRoot.Pet", b =>
                {
                    b.Navigation("PetPhotos");
                });

            modelBuilder.Entity("Pet4U.Domain.PetManagement.AgregateRoot.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Pet4U.Domain.SpeciesManagement.AgregateRoot.Species", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}