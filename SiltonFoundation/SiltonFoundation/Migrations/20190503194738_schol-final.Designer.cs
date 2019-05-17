﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiltonFoundation.Data;

namespace SiltonFoundation.Migrations
{
    [DbContext(typeof(ScholarshipDbContext))]
    [Migration("20190503194738_schol-final")]
    partial class scholfinal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SiltonFoundation.Models.AppResponse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<int>("QuestionID");

                    b.Property<int>("SFAppID");

                    b.HasKey("ID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("SFAppID");

                    b.ToTable("AppResponses");
                });

            modelBuilder.Entity("SiltonFoundation.Models.Evaluation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppResponseID");

                    b.Property<DateTime>("Closed");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("LastChange");

                    b.Property<string>("Name");

                    b.Property<int>("QuestionID");

                    b.Property<int>("SFAppID");

                    b.Property<int>("Score");

                    b.HasKey("ID");

                    b.HasIndex("AppResponseID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("SFAppID");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("SiltonFoundation.Models.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("Prompt")
                        .IsRequired();

                    b.Property<string>("ResponseOptions");

                    b.Property<int>("ResponseType");

                    b.Property<bool>("Score");

                    b.HasKey("ID");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Active = true,
                            Category = "About",
                            Prompt = "Which grant do you wish to apply for?",
                            ResponseOptions = "National, West Coast, Both",
                            ResponseType = 2,
                            Score = false
                        },
                        new
                        {
                            ID = 2,
                            Active = true,
                            Category = "About",
                            Prompt = "Age",
                            ResponseType = 1,
                            Score = false
                        },
                        new
                        {
                            ID = 3,
                            Active = true,
                            Category = "About",
                            Prompt = "Current state of residence:",
                            ResponseType = 1,
                            Score = false
                        },
                        new
                        {
                            ID = 4,
                            Active = true,
                            Category = "About",
                            Prompt = "Occupation(s):",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 5,
                            Active = true,
                            Category = "About",
                            Prompt = "Annual Income:",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 6,
                            Active = true,
                            Category = "About",
                            Prompt = "Can you be claimed as a dependent?",
                            ResponseOptions = "Yes, No",
                            ResponseType = 2,
                            Score = true
                        },
                        new
                        {
                            ID = 7,
                            Active = true,
                            Category = "About",
                            Prompt = "Do you generate any income as a dancer/instructor/performer?",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 8,
                            Active = true,
                            Category = "About",
                            Prompt = @"Please create an introduction video of yourself and share the video link below. 
(Suggestion: Store your video on YouTube or Vimeo, and submit the 'Share' link.)",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 9,
                            Active = true,
                            Category = "About",
                            Prompt = @"Please submit a link for a clear headshot of yourself. 
(Suggestion: Store your files in your Google Drive account, and submit the 'Share' link.)",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 10,
                            Active = true,
                            Category = "About",
                            Prompt = @"Please upload a video of your dancing to YouTube and attach the video link below.
(Suggestion: Store your files in your Google Drive account, and submit the 'Share' link.)",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 11,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "What do you enjoy about West Coast Swing?",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 12,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "How much time do you spend weekly with West Coast Swing social dancing?",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 13,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "How much time do you spend weekly with West Coast Swing taking lessons?",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 14,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "How much time do you spend weekly with West Coast Swing practicing?",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 15,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "How much time do you spend weekly with West Coast Swing teaching?",
                            ResponseType = 1,
                            Score = true
                        },
                        new
                        {
                            ID = 16,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "Are you proficient in any other dance styles? If so, which ones, and how much time do you spend weekly on those other styles?",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 17,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "Do you volunteer or help out in your local dance community? If yes, please explain.",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 18,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "Do you regularly take lessons? Where/from whom?",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 19,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "What do you enjoy doing when you're not dancing?",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 20,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "How/When/Where did you start dancing West Coast Swing?",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 21,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "Which dancer(s) has/have influenced you most and how/why?",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 22,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "WCS Conventions you have attended (please list which years you attended):",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 23,
                            Active = true,
                            Category = "Dance Background",
                            Prompt = "WCS Conventions you hope to attend:",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 24,
                            Active = true,
                            Category = "Usage of Scholarship Funds",
                            Prompt = @"Provide a detailed budget for a complete use of an estimated $1000 scholarship allocation (Not including event tickets granted.) 
No monies can be given to The Silton Foundation, Doug Silton or any other board members of TSF, and all monies must be used within one 12 month period from receiving the grant. 
Examples: 
$200 private coaching from Mrs. Amazing Instructor 
$120 event ticket to Super Awesome Dance Event Not Included With My Grant 
$180 shared hotel room for 3 nights at Superdance Event",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 25,
                            Active = true,
                            Category = "References",
                            Prompt = "If you have any professionals in the West Coast Swing or other dance worlds that you would like to ask for references, please have them send you their reference letter and submit a link to it below along with their contact information (name, phone, and email). (This is not mandatory)",
                            ResponseType = 0,
                            Score = true
                        },
                        new
                        {
                            ID = 26,
                            Active = true,
                            Category = "Essay",
                            Prompt = "Please explaining why you should be chosen to receive TSF Scholarship. This essay is worth a majority of your application score. Please be as detailed as you would like. Information from this essay may be shared publicly.",
                            ResponseType = 0,
                            Score = true
                        });
                });

            modelBuilder.Entity("SiltonFoundation.Models.SFApp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminEmail");

                    b.Property<int>("Amount");

                    b.Property<int>("AppStatus");

                    b.Property<int>("AppYear");

                    b.Property<int>("ApplyingFor");

                    b.Property<bool>("Awarded");

                    b.Property<DateTime?>("Closed");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<DateTime>("LastChange");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("SFApps");
                });

            modelBuilder.Entity("SiltonFoundation.Models.AppResponse", b =>
                {
                    b.HasOne("SiltonFoundation.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiltonFoundation.Models.SFApp", "SFApp")
                        .WithMany()
                        .HasForeignKey("SFAppID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiltonFoundation.Models.Evaluation", b =>
                {
                    b.HasOne("SiltonFoundation.Models.AppResponse")
                        .WithMany("Evaluations")
                        .HasForeignKey("AppResponseID");

                    b.HasOne("SiltonFoundation.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiltonFoundation.Models.SFApp", "SFApp")
                        .WithMany()
                        .HasForeignKey("SFAppID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}