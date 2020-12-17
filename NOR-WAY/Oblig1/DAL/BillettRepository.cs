using Castle.Core.Logging;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oblig1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Oblig1.DAL
{
    public class BillettRepository : IBillettRepository
    {
        private BillettContext _db;

        private ILogger<BillettRepository> _log;

        public BillettRepository(BillettContext db, ILogger<BillettRepository> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost] // Lagrer billett i databasen. returnerer id til billet for så å bruke den i hentBillett
        public async Task<int> kjopBillett(Billett enBillett)
        {
            try
            {
                //lagrer billettt i databasen
                var dbBilletter = new Billetter();
                dbBilletter.Avgang = enBillett.Avgang;
                dbBilletter.Destinasjon = enBillett.Destinasjon;
                dbBilletter.Tid = enBillett.Tid;
                dbBilletter.Pris = enBillett.Pris;
                _db.Billetter.Add(dbBilletter);

                await _db.SaveChangesAsync();
                //returnerer dbBilletter.id
                return dbBilletter.id;
            }
            catch
            {

                return -1;
            }
        }


        //Henter ut kjøpt billett gitt en id
        [HttpPost]
        public async Task<Billetter> hentBillett(int id)
        {
            try
            {

                Billetter billett = await _db.Billetter.FindAsync(id);
                await _db.SaveChangesAsync();
                return billett;
            }
            catch
            {

                return null;
            }
        }



      

        
//Henter ut alle stasjoner inn i første select (stasjoner fra)
public async Task<List<Stasjoner>> HentAlleStasjonerFra()
{
   try
   {
       // gjøre om fra biletter til billett 
       List<Stasjoner> alleStasjoner = await _db.Stasjoner.ToListAsync();

       return alleStasjoner;
   }
   catch
   {
       return null;
   }
}

   [HttpPost]   //Henter ut alle tilgjengelige stasjonertil med samme rute for stasjonerfra
   public async Task<List<Stasjoner>> HentAlleStasjonerTil(int id)
   {
       try
       {
           Avganger stasjonAvgang = await _db.Avganger.FirstOrDefaultAsync(b => b.Stopp.StasjonId == id);

           Ruter rute = await _db.Ruter.FirstOrDefaultAsync(rute => rute.RuteId == stasjonAvgang.Rute.RuteId);

           List<Stasjoner> stasjonerRute = new List<Stasjoner>();

       //finner riktig rute for stasjonerfra
       rute.Avganger.ForEach(Avganger => {
               if (!(Avganger.Stopp.StasjonId == id)) {
                   stasjonerRute.Add(Avganger.Stopp);
                   };
               }
           );
           return stasjonerRute;
       }
       catch
       {
           return null;
       } 
   }




        //Sjekker avgangstider for stasjoner fra (tur)
        public async Task<Tid> sjekkTidTur(int id)
        {

            //Tur ligger på indeks 0
            List<Avganger> stasjonerTur = await _db.Avganger.Where(avganger => (avganger.Stopp.StasjonId == id)).ToListAsync();

            Tid tidTur = new Tid();

            tidTur.Hours = stasjonerTur[0].Tider[0].Tid.Hours;
            tidTur.Minutes = stasjonerTur[0].Tider[0].Tid.Minutes;
            tidTur.Seconds = stasjonerTur[0].Tider[0].Tid.Seconds;

             return tidTur;
        }

//Sjekker avgangstider for stasjoner til (retur)
public async Task<Tid> sjekkTidRetur(int id)
{
   //Tur ligger på indeks 1
   List<Avganger> stasjonerTur = await _db.Avganger.Where(avganger => (avganger.Stopp.StasjonId == id)).ToListAsync();
   Tid tidTur = new Tid();
   tidTur.Hours = stasjonerTur[1].Tider[0].Tid.Hours;
   tidTur.Minutes = stasjonerTur[1].Tider[0].Tid.Minutes;
   tidTur.Seconds = stasjonerTur[1].Tider[0].Tid.Seconds;
   return tidTur;

}





        public async Task<int> hentPris(int id)
        {

            try
            {
                Avganger avgang = _db.Avganger.Where(a => a.Stopp.StasjonId == id).FirstOrDefault();
                return avgang.Rute.Pris;
            }
            catch
            {
                return -1;
            }

        }






        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 Ny kode                            --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//






        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 Stasjon ADMINISTRASJON                --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public async Task<bool> EndreStasjon(Stasjoner stasjon)
        {
            try
            {
                var endreObjekt = await _db.Stasjoner.FindAsync(stasjon.StasjonId);

                endreObjekt.StasjonNavn = stasjon.StasjonNavn;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
            return true;
        }


        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 BUSS ADMINISTRASJON                --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
       [HttpPost]
        public async Task<bool> EndreBuss(Busser enBuss)
        {
            try
            {
                var endreObjekt = await _db.Busser.FindAsync(enBuss.BussId);

                endreObjekt.BussNavn = enBuss.BussNavn;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
     
        }




 

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                Avgangstider ADMINISTRASJON             --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public async Task<bool> LeggTilTid(Tid tid)
        {
            try
            {
            var Avganger = await _db.Avganger.FindAsync(tid.Id);
            var avgangstid = new Avgangstider();
            avgangstid.Tid = new TimeSpan(tid.Hours, tid.Minutes, tid.Seconds);
            Avganger.Tider.Add(avgangstid);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }




        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 Rute ADMINISTRASJON                --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        public async Task<bool> OppdaterRetur(Reise reise)
        {
            try
            {
                List<Avganger> retur = await _db.Avganger.Where(a => a.Stopp.StasjonId == reise.StasjonId).ToListAsync();
                // indeks 1 er alltid retur.
                retur[1].Rute.Pris = reise.Pris;
                retur[0].Rute.Pris = reise.Pris;

                Buss_Rute bussNavnTur =
                await _db.Buss_rute.Where((b) => b.Rute.RuteId == retur[0].Rute.RuteId)
                .FirstOrDefaultAsync();

                Buss_Rute bussNavnReTur =
              await _db.Buss_rute.Where((b) => b.Rute.RuteId == retur[1].Rute.RuteId)
              .FirstOrDefaultAsync();
                bussNavnTur.Buss.BussNavn = reise.BussNavn;
                bussNavnReTur.Buss.BussNavn = reise.BussNavn;
                if (bussNavnReTur.Buss_RuteId == reise.BussRuteId)
                {
                    bussNavnTur.Buss.BussNavn += "_Motsatt";
                }

               if(bussNavnTur.Buss_RuteId == reise.BussRuteId)
                {
                    bussNavnReTur.Buss.BussNavn += "_Motsatt";
                }
              
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }



        public async Task<bool> SlettRute(int id)
        {
            try
            {
                Buss_Rute buss_Rute = await _db.Buss_rute.FindAsync(id);
                Ruter rute = await _db.Ruter.FindAsync(buss_Rute.Rute.RuteId);
                List<Avganger> avganger = await _db.Avganger.Where((a) => a.Rute.RuteId == rute.RuteId).ToListAsync();


                List<Avgangstider> avgangstider = new List<Avgangstider>();
                avganger.ForEach((a) => {
                    a.Tider.ForEach(at => avgangstider.Add(at));
                });

                Busser buss = await _db.Busser.FindAsync(buss_Rute.Buss.BussId);

                _db.Avgangstider.RemoveRange(avgangstider);
                _db.Avganger.RemoveRange(avganger);

                _db.Busser.Remove(buss);
                _db.Ruter.Remove(rute);

                _db.Buss_rute.Remove(buss_Rute);

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public async Task<Buss_Rute> HentEnRute(int id)
        {
            try
            {

                Buss_Rute rute = await _db.Buss_rute.FindAsync(id);
                await _db.SaveChangesAsync();
                return rute;
            }
            catch
            {

                return null;
            }
        }



        [HttpPost]
        public async Task<List<Buss_Rute>> HentAlleRuter()
        {
            try
            {
                List<Buss_Rute> ruter = await _db.Buss_rute.OrderBy((r) => r.Rute.RuteId).ToListAsync();
                await _db.SaveChangesAsync();
                return ruter;
            }
            catch
            {

                return null;
            }
        }


        public async Task<bool> LeggTilRute(Rute rute)
        {
            try
            {
                var Buss = new Busser();
                Buss.BussNavn = rute.BussNavn;
                var BussRetur = new Busser();
                BussRetur.BussNavn = rute.BussNavn + "_Retur";

               string[] avganger = rute.Avganger.Split(",".ToCharArray());
               string[] avgangstider = rute.Tider.Split(",".ToCharArray());


                // stasjoner fra Oslo_Larvik
                // Stasjonene tar ikke for seg virkeligheten
                var stasjoner = new List<Stasjoner>
            {
                new Stasjoner {StasjonNavn = avganger[0]},
                new Stasjoner {StasjonNavn =  avganger[1]},
                new Stasjoner {StasjonNavn = avganger[2]},
                new Stasjoner {StasjonNavn = avganger[3]},
                new Stasjoner {StasjonNavn =  avganger[4]},
                new Stasjoner {StasjonNavn =  avganger[5]}
            };

                var Last_Inserted_Id = _db.Avganger.OrderByDescending(u => u.StoppId).FirstOrDefault();
                int id = Last_Inserted_Id.StoppId;


                //avgangstider Til Buss Oslo_Larvik
                var avgang = new List<Avganger>
            {
                new Avganger{StoppId = id+1,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[0].Split(":")[0]), int.Parse(avgangstider[0].Split(":")[1]), 00)}} ,Stopp=stasjoner[0]},
                new Avganger{StoppId = id+2,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[1].Split(":")[0]), int.Parse(avgangstider[1].Split(":")[1]), 00)}},Stopp=stasjoner[1]},
                new Avganger{StoppId = id+3,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[2].Split(":")[0]), int.Parse(avgangstider[2].Split(":")[1]),00)}},Stopp=stasjoner[2]},
                new Avganger{StoppId = id+4,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[3].Split(":")[0]), int.Parse(avgangstider[3].Split(":")[1]),00)}},Stopp=stasjoner[3]},
                new Avganger{StoppId = id+5,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[4].Split(":")[0]), int.Parse(avgangstider[4].Split(":")[1]),00)}},Stopp=stasjoner[4]},
                new Avganger{StoppId = id+6,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[5].Split(":")[0]), int.Parse(avgangstider[5].Split(":")[1]),00)}},Stopp=stasjoner[5]},
            };

                var Rute = new Ruter
                {

                    Pris = 100,
                    Avganger = avgang
                };

                var BussRute1 = new Buss_Rute
                {
                    TidFra = new TimeSpan(int.Parse(avgangstider[0].Split(":")[0]), int.Parse(avgangstider[0].Split(":")[1]), 00),
                    TidTil = new TimeSpan(int.Parse(avgangstider[5].Split(":")[0]), int.Parse(avgangstider[0].Split(":")[1]), 00),
                    Buss = Buss,
                    Rute = Rute
                };

                _db.Buss_rute.Add(BussRute1);


                var avgangRetur = new List<Avganger>
            {
                new Avganger{StoppId = id+7,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[5].Split(":")[0]), int.Parse(avgangstider[5].Split(":")[1]), 00)}} ,Stopp=stasjoner[5]},
                new Avganger{StoppId = id+8,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[4].Split(":")[0]), int.Parse(avgangstider[4].Split(":")[1]), 00)}},Stopp=stasjoner[4]},
                new Avganger{StoppId = id+9,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[3].Split(":")[0]), int.Parse(avgangstider[3].Split(":")[1]),00)}},Stopp=stasjoner[3]},
                new Avganger{StoppId = id+10,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[2].Split(":")[0]), int.Parse(avgangstider[2].Split(":")[1]),00)}},Stopp=stasjoner[2]},
                new Avganger{StoppId = id+11,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[1].Split(":")[0]), int.Parse(avgangstider[1].Split(":")[1]),00)}},Stopp=stasjoner[1]},
                new Avganger{StoppId = id+12,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(int.Parse(avgangstider[0].Split(":")[0]), int.Parse(avgangstider[0].Split(":")[1]),00)}},Stopp=stasjoner[0]},
            };

                var RuteRetur = new Ruter
                {

                    Pris = 100,
                    Avganger = avgangRetur
                };

                var BussRute1Retur = new Buss_Rute
                {
                    TidFra = new TimeSpan(int.Parse(avgangstider[5].Split(":")[0]), int.Parse(avgangstider[0].Split(":")[1]), 00),
                    TidTil = new TimeSpan(int.Parse(avgangstider[0].Split(":")[0]), int.Parse(avgangstider[0].Split(":")[1]), 00),
                    Buss = BussRetur,
                    Rute = RuteRetur
                };


                _db.Buss_rute.Add(BussRute1Retur);

                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }


        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------                 Innlogging                         --------------------------------------------------------------------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                // sjekk passordet
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }


    }
}
       


