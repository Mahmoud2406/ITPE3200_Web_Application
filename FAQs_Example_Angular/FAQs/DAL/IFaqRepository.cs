using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mappe3.DAL
{
    public interface IFaqRepository
    {
       Task<List<Questions>> FetchAllQuestions();
        Task<bool> UpdateRating(Ratings rating);
        Task<Ratings> FetchOneRating(int id);
        Task<bool> SaveQuestion(Questions question);
    }
}
