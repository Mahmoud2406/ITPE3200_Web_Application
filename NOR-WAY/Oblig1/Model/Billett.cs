using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.Model
{
    public class Billett
    {
        public int id { get; set; }
        public string Avgang { get; set;}
        public string Destinasjon { get; set; }
        public string Tid { get; set; }
        public string Pris { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Du må skrive ned Telefonummer")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[0-9]{8}")]
        public string Nummer { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email må oppgis")]
        [EmailAddress]
        public string Epost { get; set; }

        [Display(Name = "Kortnummer")]
        [Required(ErrorMessage = "Kortnummer må oppgis (16 Tall)")]
        [RegularExpression(@"[0-9]{16}")]
        public string Kortnummer { get; set; }

        [Display(Name = "CVC")]
        [Required(ErrorMessage = "CVC må oppgis (3 Tall)")]
        [RegularExpression(@"[0-9]{3}")]
        public int Cvc { get; set; }

    }
}
