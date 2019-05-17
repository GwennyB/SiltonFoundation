using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models.Interfaces
{
    // The respository design pattern is implemented throughout this app in order to add a layer of abstraction between the client and the data stores, thereby protecting the data stores from certain types of attacks and breaches.
    // Dependency injection is the means by which the RDP is implemented in this app, which offers the added benefit of ease of maintenance.
    // This service interface supports functionality that touches the 'Evaluations' table in the SiltonScholarship database. This service is injected into controllers throughout this app in order to facilitate their access to this table (rather than accessing the DB directly).
    public interface IEvaluationManager
    {
        Task<Evaluation> CreateEvaluationAsync(string email, int appID);
        Task CreateEvaluationsForAppAsync(int appID);
        Task<Evaluation> GetEvaluationAsync(string email, int appID);
        Task<List<Evaluation>> GetAllEvaluationsAsync(int appID);

        Task<Evaluation> UpdateEvaluationAsync(Evaluation eval);
        Task DeleteEvaluationAsync(Evaluation eval);
    }
}
