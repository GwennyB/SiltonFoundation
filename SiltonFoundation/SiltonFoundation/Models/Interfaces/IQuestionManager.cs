using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models.Interfaces
{
    // The respository design pattern is implemented throughout this app in order to add a layer of abstraction between the client and the data stores, thereby protecting the data stores from certain types of attacks and breaches.
    // Dependency injection is the means by which the RDP is implemented in this app, which offers the added benefit of ease of maintenance.
    // This service interface supports functionality that touches the 'Questions' table in the SiltonScholarship database. This service is injected into controllers throughout this app in order to facilitate their access to this table (rather than accessing the DB directly).
    public interface IQuestionManager
    {
        Task<Question> GetQuestionAsync(int ID);
        Task<List<Question>> GetAppQuestionsAsync(List<AppResponse> resp);
        Task<List<Question>> GetAllQuestionsAsync();
        Task<List<Question>> GetAllActiveQuestionsAsync();
        List<string> GetAllCategories(List<Question> questions);
    }
}
