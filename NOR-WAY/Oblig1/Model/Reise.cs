using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.Model
{
    public class Reise
    {
        public int BussRuteId { get; set; }
        public int RuteId { get; set; }
        public int StasjonId { get; set; }
        public int Pris { get; set; }
        public string BussNavn { get; set; }
       
    }
}
