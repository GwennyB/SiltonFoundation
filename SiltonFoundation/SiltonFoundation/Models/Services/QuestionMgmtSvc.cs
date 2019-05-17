using Microsoft.EntityFrameworkCore;
using SiltonFoundation.Data;
using SiltonFoundation.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models.Services
{
    // The respository design pattern is implemented throughout this app in order to add a layer of abstraction between the client and the data stores, thereby protecting the data stores from certain types of attacks and breaches.
    // Dependency injection is the means by which the RDP is implemented in this app, which offers the added benefit of ease of maintenance.
    // This service implements the IQuestionManager interface and provides all functionality that touches the 'Questions' table in the SiltonScholarship database. This service is injected into controllers throughout this app in order to facilitate their access to this table (rather than accessing the DB directly).

        // TODO: Add C,U,D methods when Questions Mgmt feature is ready to be built

    public class QuestionMgmtSvc : IQuestionManager
    {
        private ScholarshipDbContext _context { get; }

        public QuestionMgmtSvc(ScholarshipDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Collects and returns a list of Question objects with line-by-line association with a list of AppResponses
        /// </summary>
        /// <param name="resp"> list of AppResponse objects for which Question objects are to be collected </param>
        /// <returns> list of Question objects </returns>
        public async Task<List<Question>> GetAppQuestionsAsync(List<AppResponse> resp)
        {
            List<Question> questions = new List<Question>();
            foreach (AppResponse item in resp)
            {
                questions.Add(await _context.Questions.FindAsync(item.QuestionID));
            }
            return questions;
        }

        /// <summary>
        /// Collects and returns a list of all existing Question objects
        /// </summary>
        /// <returns> list of all (unfiltered) Question objects </returns>
        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            var query = await _context.Questions.ToListAsync<Question>();
            return query;
        }

        /// <summary>
        /// Collects and returns a list of unique Category strings reflected in list of Question objects
        /// Category string is collected from 'Category' property of Question class
        /// </summary>
        /// <param name="questions"> list of Question objects from which to collect Category strings </param>
        /// <returns> list of unique Category strings </returns>
        public List<string> GetAllCategories(List<Question> questions)
        {
            List<string> cats = new List<string>();
            foreach (Question item in questions)
            {
                if (!cats.Contains(item.Category))
                    cats.Add(item.Category);
            }
            return cats;
        }
        public async Task<List<Question>> GetAllActiveQuestionsAsync()
        {
            var query = (await GetAllQuestionsAsync()).Where(q => q.Active == true).ToList<Question>();
            return query;
        }

        /// <summary>
        /// Gets and returns a single question by its ID
        /// </summary>
        /// <param name="ID"> ID of question to find and return </param>
        /// <returns> Question object with specified ID </returns>
        public async Task<Question> GetQuestionAsync(int ID)
        {
            return await _context.Questions.FindAsync(ID);
        }

    }
}
