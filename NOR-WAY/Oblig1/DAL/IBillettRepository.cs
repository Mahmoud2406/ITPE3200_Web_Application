using Oblig1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.DAL
{
    public interface IBillettRepository
    {
        Task<int> kjopBillett(Billett innBillett);
        Task<List<Stasjoner>> HentAlleStasjonerFra();
        Task<Tid> sjekkTidTur(int id);
        Task<Billetter> hentBillett(int id);
        Task<Tid> sjekkTidRetur(int id);
        Task<int> hentPris(int id);
        Task<List<Stasjoner>> HentAlleStasjonerTil(int id);

        // ny admin


        
        // Stasjon
        Task<bool> EndreStasjon(Stasjoner stasjon); //
        // Buss
        Task<bool> EndreBuss(Busser enBuss); //
        Task<bool> LoggInn(Bruker bruker); //
        Task<List<Buss_Rute>> HentAlleRuter();//
        //Rute
        Task<bool> OppdaterRetur(Reise reise); //
        Task<bool> LeggTilRute(Rute rute); //
        Task<bool> SlettRute(int id);  //
        Task<Buss_Rute> HentEnRute(int id); //

        //Tid
        Task<bool> LeggTilTid(Tid tid); //
        
    }
}
