using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Oblig1.Model
{

    public class Busser
    {
        [Key]
        public int BussId { get; set; }
        public string BussNavn { get; set; }


    }

    public class Buss_Rute
    {
        [Key]
        public int Buss_RuteId { get; set; }
        public TimeSpan TidFra { get; set; }
        public TimeSpan TidTil { get; set; }
        public virtual Busser Buss { get; set; }
        public virtual Ruter Rute { get; set; }

    }


    public class Ruter
    {
        [Key]
        public int RuteId { get; set; }
        public int Pris { get; set; }
        public virtual List<Avganger> Avganger { get; set; }

    }
    public class Stasjoner
    {
        [Key]
        public int StasjonId { get; set; }
        public string StasjonNavn { get; set; }
    }

    public class Avganger
    {
        [Key]
        public int StoppId { get; set; }
        public virtual Stasjoner Stopp { get; set; }
        public virtual List<Avgangstider> Tider { get; set; }
        public virtual Ruter Rute { get; set; }

    }

    public class Avgangstider
    {
        [Key]
        public int TidId { get; set; }
        public TimeSpan Tid { get; set; }  
    }

    public class Brukere
    {
        [Key]
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }
    public class Billetter
    {
        [Key]
        public int id { get; set; }
        public string Avgang { get; set; }
        public string Destinasjon { get; set; }
        public string Tid { get; set; }
        public string Pris { get; set; }
    }




    public class BillettContext :DbContext
    {

        public BillettContext(DbContextOptions<BillettContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Busser> Busser { get; set; }
        public DbSet<Stasjoner> Stasjoner { get; set; }
        public DbSet<Buss_Rute> Buss_rute { get; set; }
        public DbSet<Avganger> Avganger { get; set; }
        public DbSet<Ruter> Ruter { get; set; }
        public DbSet<Avgangstider> Avgangstider { get; set; }
        public DbSet<Brukere> Brukere { get; set; }
        public DbSet<Billetter> Billetter { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}

