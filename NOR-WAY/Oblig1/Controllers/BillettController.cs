using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oblig1.DAL;
using Oblig1.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.Controllers
{
    [Route("[controller]/[action]")]
    public class BillettController : ControllerBase
    {

        private readonly IBillettRepository _db;

        private ILogger<BillettController> _log;

        public const string _loggetInn = "LoggetInn";

        public BillettController(IBillettRepository db, ILogger<BillettController> log)
        {
            _db = db;
            _log = log;
        }
        [ExcludeFromCodeCoverage]
        public async Task<Billetter> hentBillett(int id)
        {
            return await _db.hentBillett(id);
        }
        [ExcludeFromCodeCoverage]
        public async Task<int> kjopBillett(Billett innBillett)
        {
            return await _db.kjopBillett(innBillett);
        }



        [ExcludeFromCodeCoverage]
        public async Task<List<Stasjoner>> HentAlleStasjonerFra()
        {
           
            return await _db.HentAlleStasjonerFra();
        }



        public async Task<int> hentPris(int id)
        {
            return await _db.hentPris(id);
        }

        [HttpPost]
        [ExcludeFromCodeCoverage]
        public async Task<Tid> sjekkTidTur(int id)
        {

            return await _db.sjekkTidTur(id);
        }

        [HttpPost]
        [ExcludeFromCodeCoverage]
        public async Task<Tid> sjekkTidRetur(int id)
        {
       

            return await _db.sjekkTidTur(id);
        }

        [HttpPost]
        [ExcludeFromCodeCoverage]
        public async Task<List<Stasjoner>> HentAlleStasjonerTil(int id)
        {
           return await _db.HentAlleStasjonerTil(id);
       }





        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                  NY KODE FOR                    -----------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                  GRUPPEINNLEVERING 2            -----------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//



        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 STASJON ADMINISTRASJON             --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<ActionResult> EndreStasjon(Stasjoner endreStasjon)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            bool returnOk = await _db.EndreStasjon(endreStasjon);
            if (! returnOk)
            {
                _log.LogInformation("Stasjon ble ikke endret!");
                return BadRequest("Stasjon ble ikke endret!");
            }
            return Ok("Stasjon ble endret");
        }




        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 BUSS ADMINISTRASJON                --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<ActionResult> EndreBuss(Busser endreBuss)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            bool returnOk = await _db.EndreBuss(endreBuss);
            if (!returnOk)
            {
                _log.LogInformation("Bussen ble ikke endret!");
                return BadRequest("Bussen ble ikke endret!");
            }
            return Ok("Bussen ble endret!");
        }


 
        public async Task<ActionResult> HentEnRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            Buss_Rute returnOk = await _db.HentEnRute(id);
            if (returnOk == null)
            {
                _log.LogInformation("Rute ble ikke hentet!");
                return BadRequest("Rute ble ikke hentet!");
            }
            return Ok(_db.HentEnRute(id));

        }


        public async Task<ActionResult> HentAlleRuter()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            List<Buss_Rute> returnOk = await _db.HentAlleRuter();
            if (returnOk == null)
            {
                _log.LogInformation("Rutene ble ikke hentet!");
                return BadRequest("Rutene ble ikke hentet!");
            }

            return Ok(_db.HentAlleRuter());
        }



        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 Rute ADMINISTRASJON                --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//



        public async Task<ActionResult> OppdaterRetur(Reise reise)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            bool returnOk = await _db.OppdaterRetur(reise);
            if (!returnOk)
            {
                _log.LogInformation("Reisen ble ikke oppdatert!");
                return BadRequest("Reisen ble ikke oppdatert!");
            }
            return Ok("Reisen ble oppdatert!");
        }
        [HttpPost]

        public async Task<ActionResult> LeggTilRute(Rute rute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            bool returnOk = await _db.LeggTilRute(rute);
            if (!returnOk)
            {
                _log.LogInformation("Rute ble ikke lagret!");
                return BadRequest("Rute ble ikke lagret!");
            }
            return Ok("Rute ble lagret!");
        }

        public async Task<ActionResult> SlettRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            bool returnOk = await _db.SlettRute(id);
            if (!returnOk)
            {
                _log.LogInformation("Rute ble ikke slettet!");
                return BadRequest("Rute ble ikke slettet!");
            }
            return  Ok(true);
        }


        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 LOGG INN METODER API-KALL          --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public async Task<ActionResult> LeggTilTid(Tid tid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            bool returnOk = await _db.LeggTilTid(tid);
            if (!returnOk)
            {
                _log.LogInformation("Tiden ble ikke lagret!");
                return BadRequest("Tiden ble ikke lagret!");
            }
            return Ok("Tiden ble lagret!");
        }

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker" + bruker.Brukernavn);
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true);
            }
            
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
        public async Task<ActionResult> SjekkLoggetIn( )
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            return Ok(true);
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }

    }

}