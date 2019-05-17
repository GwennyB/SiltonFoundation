// TODO: This service is not yet in use by the application. All methods require testing before implementing Evaluator functionality.


using Microsoft.AspNetCore.Identity;
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
    // This service implements the IEvaluationManager interface and provides all functionality that touches the 'Evaluations' table in the SiltonScholarship database. This service is injected into controllers throughout this app in order to facilitate their access to this table (rather than accessing the DB directly).
    public class EvaluationMgmtSvc : IEvaluationManager
    {
        private ScholarshipDbContext _context { get; }
        private readonly UserManager<AppUser> _user;
        private readonly IAppResponseManager _resp;

        public EvaluationMgmtSvc(ScholarshipDbContext context, UserManager<AppUser> user, IAppResponseManager resp)
        {
            _context = context;
            _user = user;
            _resp = resp;
        }

        /// <summary>
        /// Creates new evaluation for specified application ID and evaluator if no evaluation exists.
        /// If evaluation exists, returns the evaluation.
        /// </summary>
        /// <param name="email"> user's email </param>
        /// <returns> new (or existing) evaluation for specified appID and evaluator </returns>
        public async Task<Evaluation> CreateEvaluationAsync(string email, int appID)
        {
            var query = await GetEvaluationAsync(email, appID);
            // if no evaluation exists, create one
            if (query == null)
            {
                Evaluation eval = new Evaluation()
                {
                    SFAppID = appID,
                    SFApp = await _context.SFApps.FindAsync(appID),
                    Email = email,
                    LastChange = DateTime.Now,
                };
                await _context.Evaluations.AddAsync(eval);
                await _context.SaveChangesAsync();
                query = await GetEvaluationAsync(email, appID);
            }
            return query;
        }

        public async Task CreateEvaluationsForAppAsync(int appID)
        {
            IList<AppUser> evaluators = await _user.GetUsersInRoleAsync("Evaluator");
            List<AppResponse> responses = await _resp.GetAllAppResponsesAsync(appID);
            foreach (AppResponse resp in responses)
            {
                foreach (AppUser eval in evaluators)
                {
                    await CreateEvaluationAsync(eval.Email, appID);
                }
            }
        }

        /// <summary>
        /// Gets and returns evaluation for specified evaluator and application ID
        /// </summary>
        /// <param name="email"> evaluator's email </param>
        /// <param name="appID"> application ID </param>
        /// <returns> evaluation for specified appID and sponsor </returns>
        public async Task<Evaluation> GetEvaluationAsync(string email, int appID)
        {
            var query = await _context.Evaluations.FirstOrDefaultAsync(s => s.Email == email && s.SFAppID == appID);
            return query;
        }

        /// <summary>
        /// Gets and returns all evaluations for specified application ID
        /// </summary>
        /// <param name="appID"> application ID </param>
        /// <returns> evaluation for specified appID and sponsor </returns>
        public async Task<List<Evaluation>> GetAllEvaluationsAsync(int appID)
        {
            var query = await _context.Evaluations.Where(e => e.SFAppID == appID).ToListAsync();
            return query;
        }

        /// <summary>
        /// Updates specified evaluation
        /// </summary>
        /// <param name="eval"> evaluation to update </param>
        /// <returns> updated evaluation </returns>
        public async Task<Evaluation> UpdateEvaluationAsync(Evaluation eval)
        {
            _context.Evaluations.Update(eval);
            await _context.SaveChangesAsync();
            return await GetEvaluationAsync(eval.Email, eval.SFAppID);
        }

        /// <summary>
        /// Deletes specified evaluation
        /// </summary>
        /// <param name="eval"> evaluation to delete </param>
        /// <returns> task completion </returns>
        public async Task DeleteEvaluationAsync(Evaluation eval)
        {
            _context.Evaluations.Remove(eval);
            await _context.SaveChangesAsync();
        }

    }
}
