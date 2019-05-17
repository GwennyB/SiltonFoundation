using Microsoft.EntityFrameworkCore;
using SiltonFoundation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Data
{
    // This class reflects the database context for the 'SiltonUser' database. Its build relies on Entity Framework Core (ORM). Data seeding is used to manage application questions until the Admin feature to do same is ready to deploy.
    public class ScholarshipDbContext : DbContext
    {
        public ScholarshipDbContext(DbContextOptions<ScholarshipDbContext> options) : base(options)
        {

        }

        // Seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Question>().HasData(
                new Question { ID = 1,  Active = true, Score = false, Category = "About", Prompt = "Which grant do you wish to apply for?", ResponseType = ResponseType.Select, ResponseOptions = "National, West Coast, Both"},
                new Question { ID = 2,  Active = true, Score = false, Category = "About", Prompt = "Age", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 3,  Active = true, Score = false, Category = "About", Prompt = "Current state of residence:", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 4, Active = true, Score = true, Category = "About", Prompt = "Occupation(s):", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 5, Active = true, Score = true, Category = "About", Prompt = "Annual Income:", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 6, Active = true, Score = true, Category = "About", Prompt = "Can you be claimed as a dependent?", ResponseType = ResponseType.Select, ResponseOptions = "Yes, No"},
                new Question { ID = 7, Active = true, Score = true, Category = "About", Prompt = "Do you generate any income as a dancer/instructor/performer?", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 8,  Active = true, Score = true, Category = "About", Prompt = "Please create an introduction video of yourself and share the video link below. \n(Suggestion: Store your video on YouTube or Vimeo, and submit the 'Share' link.)", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 9,  Active = true, Score = true, Category = "About", Prompt = "Please submit a link for a clear headshot of yourself. \n(Suggestion: Store your files in your Google Drive account, and submit the 'Share' link.)", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 10,  Active = true, Score = true, Category = "About", Prompt = "Please upload a video of your dancing to YouTube and attach the video link below.\n(Suggestion: Store your files in your Google Drive account, and submit the 'Share' link.)", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 11, Active = true, Score = true, Category = "Dance Background", Prompt = "What do you enjoy about West Coast Swing?", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 12, Active = true, Score = true, Category = "Dance Background", Prompt = "How much time do you spend weekly with West Coast Swing social dancing?", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 13, Active = true, Score = true, Category = "Dance Background", Prompt = "How much time do you spend weekly with West Coast Swing taking lessons?", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 14, Active = true, Score = true, Category = "Dance Background", Prompt = "How much time do you spend weekly with West Coast Swing practicing?", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 15, Active = true, Score = true, Category = "Dance Background", Prompt = "How much time do you spend weekly with West Coast Swing teaching?", ResponseType = ResponseType.Short_Text, ResponseOptions = null},
                new Question { ID = 16, Active = true, Score = true, Category = "Dance Background", Prompt = "Are you proficient in any other dance styles? If so, which ones, and how much time do you spend weekly on those other styles?", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 17, Active = true, Score = true, Category = "Dance Background", Prompt = "Do you volunteer or help out in your local dance community? If yes, please explain.", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 18, Active = true, Score = true, Category = "Dance Background", Prompt = "Do you regularly take lessons? Where/from whom?", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 19, Active = true, Score = true, Category = "Dance Background", Prompt = "What do you enjoy doing when you're not dancing?", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 20, Active = true, Score = true, Category = "Dance Background", Prompt = "How/When/Where did you start dancing West Coast Swing?", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 21, Active = true, Score = true, Category = "Dance Background", Prompt = "Which dancer(s) has/have influenced you most and how/why?", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 22, Active = true, Score = true, Category = "Dance Background", Prompt = "WCS Conventions you have attended (please list which years you attended):", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 23, Active = true, Score = true, Category = "Dance Background", Prompt = "WCS Conventions you hope to attend:", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 24, Active = true, Score = true, Category = "Usage of Scholarship Funds", Prompt = "Provide a detailed budget for a complete use of an estimated $1000 scholarship allocation (Not including event tickets granted.) \nNo monies can be given to The Silton Foundation, Doug Silton or any other board members of TSF, and all monies must be used within one 12 month period from receiving the grant. \nExamples: \n$200 private coaching from Mrs. Amazing Instructor \n$120 event ticket to Super Awesome Dance Event Not Included With My Grant \n$180 shared hotel room for 3 nights at Superdance Event", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 25, Active = true, Score = true, Category = "References", Prompt = "If you have any professionals in the West Coast Swing or other dance worlds that you would like to ask for references, please have them send you their reference letter and submit a link to it below along with their contact information (name, phone, and email). (This is not mandatory)", ResponseType = ResponseType.Long_Text, ResponseOptions = null},
                new Question { ID = 26, Active = true, Score = true, Category = "Essay", Prompt = "Please explaining why you should be chosen to receive TSF Scholarship. This essay is worth a majority of your application score. Please be as detailed as you would like. Information from this essay may be shared publicly.", ResponseType = ResponseType.Long_Text, ResponseOptions = null}
            );
        }

        // build tables
        public DbSet<SFApp> SFApps { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AppResponse> AppResponses { get; set; }

    }
}
