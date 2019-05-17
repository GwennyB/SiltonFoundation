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
    // This service implements the IAppResponseManager interface and provides all functionality that touches the 'AppResponses' table in the SiltonScholarship database. This service is injected into controllers throughout this app in order to facilitate their access to this table (rather than accessing the DB directly).
    public class AppResponseMgmtSvc : IAppResponseManager
    {
        private ScholarshipDbContext _context { get; }

        public AppResponseMgmtSvc(ScholarshipDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Creates a new AppResponse object to associate between specified SFApp and Quesiton
        /// </summary>
        /// <param name="appID"> ID of SFApp to associate with new AppResponse object </param>
        /// <param name="questionID"> ID of Question to associate with new AppResponse object </param>
        /// <returns> newly created AppResponse object </returns>
        public async Task<AppResponse> CreateAppResponseAsync(int appID, int questionID)
        {
            AppResponse response = new AppResponse()
            {
                SFAppID = appID,
                QuestionID = questionID
            };
            await _context.AppResponses.AddAsync(response);
            await _context.SaveChangesAsync();

            return await _context.AppResponses.FirstOrDefaultAsync(r => r.SFAppID == appID && r.QuestionID == questionID);
        }

        /// <summary>
        /// Finds and returns all AppResponse objects associated with specified SFApp ID
        /// </summary>
        /// <param name="appID"> ID of SFApp associated with AppResponse objects </param>
        /// <returns> list of AppResponse objects associated with specified SFApp ID </returns>
        public async Task<List<AppResponse>> GetAllAppResponsesAsync(int appID)
        {
            List<AppResponse> responses = await _context.AppResponses.Where(r => r.SFAppID == appID).ToListAsync();
            foreach (AppResponse resp in responses)
            {
                resp.Question = await _context.Questions.FindAsync(resp.QuestionID);
            }
            return responses;
        }

        /// <summary>
        /// Finds and returns a AppResponse object by record ID
        /// </summary>
        /// <param name="ID"> ID of AppResponse object to locate </param>
        /// <returns> AppResponse object with specified ID, or 'null' if not found </returns>
        public async Task<AppResponse> GetAppResponseAsync(int ID)
        {
            return await _context.AppResponses.FindAsync(ID);
        }

        /// <summary>
        /// Finds and returns a AppResponse object with specified SFApp ID and Question ID associations
        /// </summary>
        /// <param name="appID"> ID of SFApp associated with AppResponse </param>
        /// <param name="questionID"> ID of Question associated with AppResponse </param>
        /// <returns> AppResponse object, or 'null' if not found </returns>
        public async Task<AppResponse> GetAppResponseAsync(int appID, int questionID)
        {
            return await _context.AppResponses.FirstOrDefaultAsync(r => r.SFAppID == appID && r.QuestionID == questionID);
        }

        /// <summary>
        /// Updates a AppResponse object in DB and returns the updated object
        /// </summary>
        /// <param name="resp"> AppResponse object to update </param>
        /// <returns> updated AppResponse object from DB </returns>
        public async Task<bool> UpdateAppResponseAsync(AppResponse appResponse)
        {
            var query = await _context.AppResponses.FindAsync(appResponse.ID);
            if (query == null)
            {
                return false;
            }
            query.Answer = appResponse.Answer;
            _context.AppResponses.Update(query);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
