using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mappe3.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KundeApp2.Controllers
{
    [ApiController]
    // dekoratøren over må være med; dersom ikke må post og put bruke [FromBody] som deoratør inne i prameterlisten
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private IFaqRepository _db;

        private ILogger<FaqController> _log;

        public FaqController(IFaqRepository db, ILogger<FaqController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost]
        public async Task<ActionResult> SaveQuestion(Questions question)
        {
            bool lagre = await _db.SaveQuestion(question);
            if(!lagre)
            {
                return BadRequest("Spørsmålet ble ikke lagret");

            }
            return Ok(lagre);
        }


        [HttpGet]
        public async Task<ActionResult> FetchAllQuestions()
        {
            List<Questions> questions = await _db.FetchAllQuestions();
            if(questions == null)
            {
                return BadRequest("Spørsmålene kunne ikke hentes");
            }
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FetchOneRating(int id)
        {

            Ratings rating = await _db.FetchOneRating(id);
            if(rating == null)
            {
                return BadRequest("Vurderingen kunne ikke hentes");

            }
            return Ok(rating);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateRating(Ratings rating)
        {
            bool UpdateRating = await _db.UpdateRating(rating);
            if (!UpdateRating)
            {
                return BadRequest("Kunne ikke oppdatere vurderingen!");
            }
            return Ok(UpdateRating);
        }


    }
}
