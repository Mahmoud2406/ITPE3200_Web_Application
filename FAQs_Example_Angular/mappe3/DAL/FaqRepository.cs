using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mappe3.DAL
{
        public class FaqRepository : IFaqRepository
    {
        private readonly FaqContext _db;

        public FaqRepository(FaqContext db)
        {
            _db = db;
        }





        public async Task<bool> SaveQuestion(Questions question)
        {
            try
            {
                var enQuestion = new Questions
                {
                    Question = question.Question,
                    Answer = "",
                    Rating = new Ratings
                    {
                        Rating = 0
                    }
                };
                _db.Questions.Add(enQuestion);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<Questions>> FetchAllQuestions()
        {
            try
            {
                List<Questions> alleKunder = await _db.Questions.Select(k => new Questions
                {
                    Id = k.Id,
                    Question = k.Question,
                    Answer = k.Answer,
                    Category = k.Category,
                    Rating = k.Rating
                    // Det er her sorteringen basert på vurdering skjer.
                }).OrderByDescending(k => k.Rating.Rating).ToListAsync();
                return alleKunder;
            }
            catch
            {
                return null;
            }
        }


        public async Task<Ratings> FetchOneRating(int id)
        {
            try
            {
                Ratings rating = await _db.Ratings.FindAsync(id);
                return rating;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> UpdateRating(Ratings rating)
        {
            try
            {
                Ratings endreRating = await _db.Ratings.FindAsync(rating.Id);
                endreRating.Rating = rating.Rating;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}