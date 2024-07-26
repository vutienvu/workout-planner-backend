﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutPlanner.Helper;

#nullable disable

namespace WorkoutPlanner.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("WorkoutPlanner.Entity.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("exercise_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ExerciseId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("PauseDuration")
                        .HasColumnType("int")
                        .HasColumnName("pause_duration");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int")
                        .HasColumnName("workout_id");

                    b.HasKey("ExerciseId")
                        .HasName("pk_exercises");

                    b.HasIndex("WorkoutId")
                        .HasDatabaseName("ix_exercises_workout_id");

                    b.ToTable("exercises", (string)null);
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.ExerciseTerm", b =>
                {
                    b.Property<int>("ExerciseTermId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("exercise_term_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ExerciseTermId"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int")
                        .HasColumnName("exercise_id");

                    b.Property<DateTime>("TermDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("term_date");

                    b.Property<int>("TotalSets")
                        .HasColumnType("int")
                        .HasColumnName("total_sets");

                    b.HasKey("ExerciseTermId")
                        .HasName("pk_exercise_terms");

                    b.HasIndex("ExerciseId")
                        .HasDatabaseName("ix_exercise_terms_exercise_id");

                    b.ToTable("exercise_terms", (string)null);
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.Set", b =>
                {
                    b.Property<int>("SetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("set_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("SetId"));

                    b.Property<int>("ExerciseTermId")
                        .HasColumnType("int")
                        .HasColumnName("exercise_term_id");

                    b.Property<int>("Reps")
                        .HasColumnType("int")
                        .HasColumnName("reps");

                    b.Property<string>("RepsType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("reps_type");

                    b.Property<float>("Weight")
                        .HasColumnType("float")
                        .HasColumnName("weight");

                    b.Property<string>("WeightType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("weight_type");

                    b.HasKey("SetId")
                        .HasName("pk_sets");

                    b.HasIndex("ExerciseTermId")
                        .HasDatabaseName("ix_sets_exercise_term_id");

                    b.ToTable("sets", (string)null);
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.Workout", b =>
                {
                    b.Property<int>("WorkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("workout_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("WorkoutId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("WorkoutId")
                        .HasName("pk_workouts");

                    b.ToTable("workouts", (string)null);
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.Exercise", b =>
                {
                    b.HasOne("WorkoutPlanner.Entity.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exercises_workouts_workout_id");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.ExerciseTerm", b =>
                {
                    b.HasOne("WorkoutPlanner.Entity.Exercise", "Exercise")
                        .WithMany("ExerciseTerms")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exercise_terms_exercises_exercise_id");

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.Set", b =>
                {
                    b.HasOne("WorkoutPlanner.Entity.ExerciseTerm", "ExerciseTerm")
                        .WithMany("Sets")
                        .HasForeignKey("ExerciseTermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sets_exercise_terms_exercise_term_id");

                    b.Navigation("ExerciseTerm");
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.Exercise", b =>
                {
                    b.Navigation("ExerciseTerms");
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.ExerciseTerm", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("WorkoutPlanner.Entity.Workout", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
