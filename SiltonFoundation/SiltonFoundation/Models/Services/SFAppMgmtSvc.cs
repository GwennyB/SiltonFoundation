using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiltonFoundation.Data;
using SiltonFoundation.Models.Interfaces;
using SiltonFoundation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models.Services
{
    // The respository design pattern is implemented throughout this app in order to add a layer of abstraction between the client and the data stores, thereby protecting the data stores from certain types of attacks and breaches.
    // Dependency injection is the means by which the RDP is implemented in this app, which offers the added benefit of ease of maintenance.
    // This service implements the ISFAppManager interface and provides all functionality that touches the 'SFApps' table in the SiltonScholarship database. This service is injected into controllers throughout this app in order to facilitate their access to this table (rather than accessing the DB directly).
    public class SFAppMgmtSvc : ISFAppManager
    {
        private ScholarshipDbContext _context { get; }
        private readonly IAppResponseManager _response;
        private readonly IQuestionManager _question;
        private readonly UserManager<AppUser> _user;

        public SFAppMgmtSvc(ScholarshipDbContext context, IQuestionManager question, IAppResponseManager response, UserManager<AppUser> user)
        {
            _context = context;
            _response = response;
            _question = question;
            _user = user;
        }

        /// <summary>
        /// Creates new application for specified user if no open application exists.
        /// If open application exists, returns the open application.
        /// </summary>
        /// <param name="email"> user's email </param>
        /// <returns> new (or existing open) application for specified user </returns>
        public async Task<SFApp> CreateSFAppAsync(string email)
        {
            var query = await GetSFAppAsync(email);
            AppUser applicant = await _user.FindByEmailAsync(email);
            // if no open application exists, create one
            if (query == null || query.Closed != null)
            {
                // create new SFApp object
                SFApp app = new SFApp()
                {
                    Name = $"{applicant.FirstName} {applicant.LastName}",
                    Email = email,
                    Created = DateTime.Now,
                    AppYear = DateTime.Now.Year,
                    LastChange = DateTime.Now,
                    AppStatus = AppStatus.Draft,
                    Closed = null
                };

                // add new SFApp to DB
                await _context.SFApps.AddAsync(app);
                await _context.SaveChangesAsync();

                // retrieve newly created app from DB
                SFApp newApp = await GetSFAppAsync(email);

                // build responses and associate with app ID
                foreach (Question question in await _question.GetAllActiveQuestionsAsync())
                {
                    await _response.CreateAppResponseAsync(newApp.ID, question.ID);
                }
            }
            // return newly created application
            return query;
        }

        /// <summary>
        /// Gets and returns open application for specified user
        /// </summary>
        /// <param name="email"> user's email </param>
        /// <returns> open application for specified user </returns>
        public async Task<SFApp> GetSFAppAsync(string email)
        {
            var query = await _context.SFApps.FirstOrDefaultAsync(s => s.Email == email && s.AppStatus != AppStatus.Complete);

            return query;
        }

        /// <summary>
        /// Gets and returns all application for specified year range
        /// </summary>
        /// <param name="start"> first year of range </param>
        /// <param name="end"> last year of range </param>
        /// <returns> list of applications for specified range of years </returns>
        public async Task<List<SFApp>> GetAllAppsAsync(int start, int end)
        {
            var query = await _context.SFApps.Where(d => d.AppYear >= start && d.AppYear <= end).ToListAsync();
            return query;
        }

        /// <summary>
        /// Updates specified application
        /// </summary>
        /// <param name="appl"> proposed SFApp object update </param>
        /// <returns> updated SFApp object as reflected in DB </returns>
        public async Task<SFApp> UpdateSFAppAsync(SFApp appl)
        {
            _context.SFApps.Update(appl);
            await _context.SaveChangesAsync();
            return await GetSFAppAsync(appl.Email);
        }

        /// <summary>
        /// Builds a new SFAppViewModel object from the SFApp associated with user who owns 'email'
        /// </summary>
        /// <param name="email"> email of user whose app to prepare as SFAppViewModel </param>
        /// <returns> SFAppViewModel for user's current SFApp, or 'null' if no open SFApp exists </returns>
        public async Task<SFAppViewModel> BuildSFAppVM(string email)
        {
            SFApp appl = await GetSFAppAsync(email);
            if (appl != null)
            {
                SFAppViewModel sFApp = new SFAppViewModel();
                sFApp.Appl = appl;
                sFApp.AppResponses = await _response.GetAllAppResponsesAsync(appl.ID);
                sFApp.Questions = await _question.GetAppQuestionsAsync(sFApp.AppResponses);
                sFApp.Categories = _question.GetAllCategories(sFApp.Questions);
                return sFApp;
            }
            return null;
        }

        // TODO: Build method to get Closed SFApps for viewing
    }
}
