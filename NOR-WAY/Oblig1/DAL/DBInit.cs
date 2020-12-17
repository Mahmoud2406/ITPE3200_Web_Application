using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Oblig1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Oblig1.DAL
{
    public static class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BillettContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                    //Bussliste for antall busser selskapet har
                     var BussListe = new List<Busser>
                 {
                     new Busser { BussNavn = "Oslo_Kragerø"}, new Busser { BussNavn = "Oslo_Gjøvik" },
                     new Busser { BussNavn = "Oslo_Eidsvoll" },new Busser { BussNavn = "Oslo_Trondheim" },
                     new Busser { BussNavn = "Oslo_Bergen" },new Busser { BussNavn = "Oslo_Larvik" },
                    
                 };

         
             
                // stasjoner fra Oslo_Larvik
                // Stasjonene tar ikke for seg virkeligheten
                var stasjonerOsloLarvik = new List<Stasjoner>
            {
                new Stasjoner {StasjonNavn = "Nasjonalteateret" },
                new Stasjoner {StasjonNavn = "Drammen"},
                new Stasjoner {StasjonNavn = "Sande" },
                new Stasjoner {StasjonNavn = "Tønsberg"},
                new Stasjoner {StasjonNavn = "Sandefjord"},
                new Stasjoner {StasjonNavn = "Larvik"}
            };

                // stasjoner fra Oslo_Bergen
                // Stasjonene tar ikke for seg virkeligheten
                var stasjonerOsloBergen = new List<Stasjoner>
            {
                new Stasjoner {StasjonNavn = "Fornebu" },
                new Stasjoner {StasjonNavn = "Hønefoss"},
                new Stasjoner {StasjonNavn = "Nes"},
                new Stasjoner {StasjonNavn = "Flåm"},
                new Stasjoner {StasjonNavn = "Dale" },
                new Stasjoner {StasjonNavn = "Bergen"}
            };



                // stasjoner fra Oslo_Trondheim

                // Stasjonene tar ikke for seg virkeligheten
                var stasjonerOsloTrondheim = new List<Stasjoner>
            {
                new Stasjoner {StasjonNavn = "Nittedal" },
                new Stasjoner {StasjonNavn = "Hamar"},
                new Stasjoner {StasjonNavn = "Lillehammer"},
                new Stasjoner {StasjonNavn = "Otta"},
                new Stasjoner {StasjonNavn = "Oppdal" },
                new Stasjoner {StasjonNavn = "Trondheim"}
            };


                // stasjoner fra Oslo til Gjøvik
                // Stasjonene tar ikke for seg virkeligheten
                var stasjonerOsloGjøvik = new List<Stasjoner>
            {
                new Stasjoner {StasjonNavn = "Majorstuen" },
                new Stasjoner {StasjonNavn = "Sinsen"},
                new Stasjoner {StasjonNavn = "Stovner"},
                new Stasjoner {StasjonNavn = "Lillestrøm"},
                new Stasjoner {StasjonNavn = "Jessheim" },
                new Stasjoner {StasjonNavn = "Gjøvik"}
            };

                // stasjoner fra Oslo_Eidsvoll
                // Stasjonene tar ikke for seg virkeligheten
                var stasjonerOsloEidsvoll = new List<Stasjoner>
            {
                new Stasjoner {StasjonNavn = "Jernbanetorget" },
                new Stasjoner {StasjonNavn = "Strømmen"},
                new Stasjoner {StasjonNavn = "Kløfta"},
                new Stasjoner {StasjonNavn = "Gardemoen" },
                new Stasjoner {StasjonNavn = "Eidsvoll verk"},
                new Stasjoner {StasjonNavn = "Eidsvoll"}
            };

            


                // stasjoner fra Oslo til Kragerø
                // Stasjonene tar ikke for seg virkeligheten
                var stasjonerOsloKragerø = new List<Stasjoner>
            {
                new Stasjoner {StasjonNavn = "Oslo" },
                new Stasjoner {StasjonNavn = "Gjøvik"},
                new Stasjoner {StasjonNavn = "Holmestrand"},
                new Stasjoner {StasjonNavn = "Smedstad"},
                new Stasjoner {StasjonNavn = "Ås" },
                new Stasjoner {StasjonNavn = "Kragerø"}

            };


           
         

                //avgangstider Til Buss Oslo_Larvik
                var avgangstiderOslo_Larvik = new List<Avganger>
            {
                new Avganger{StoppId = 1,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}} ,Stopp=stasjonerOsloLarvik[0]},
                new Avganger{StoppId = 2,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloLarvik[1]},
                new Avganger{StoppId = 3,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloLarvik[2]},
                new Avganger{StoppId = 4,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloLarvik[3]},
                new Avganger{StoppId = 5,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloLarvik[4]},
                new Avganger{StoppId = 6,Tider= new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloLarvik[5]},
            };

                var avgangstiderOslo_Larvik_Rute = new Ruter
                {

                    Pris = 100,
                    Avganger = avgangstiderOslo_Larvik
                };

                var BussRute1 = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListe[5],
                    Rute = avgangstiderOslo_Larvik_Rute
                };


                context.Buss_rute.Add(BussRute1);
          

                // ----------------------------------------

                var avgangstiderOslo_Bergen = new List<Avganger>
            {
                new Avganger{StoppId = 7,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloBergen[0]},
                new Avganger{StoppId = 8,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloBergen[1]},
                new Avganger{StoppId = 9,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloBergen[2]},
                new Avganger{StoppId = 10,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloBergen[3]},
                new Avganger{StoppId = 11,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloBergen[4]},
                new Avganger{StoppId = 12,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)} },Stopp=stasjonerOsloBergen[5]},
            };




                var avgangstiderOslo_Bergen_Rute = new Ruter
                {

                    Pris = 400,
                    Avganger = avgangstiderOslo_Bergen
                };

                var BussRute2 = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListe[4],
                    Rute = avgangstiderOslo_Bergen_Rute
                };


                context.Buss_rute.Add(BussRute2);




                var avgangstiderOslo_Trondheim = new List<Avganger>
            {
                new Avganger{StoppId = 13,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloTrondheim[0]},
                new Avganger{StoppId = 14,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloTrondheim[1]},
                new Avganger{StoppId = 15,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloTrondheim[2]},
                new Avganger{StoppId = 16,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloTrondheim[3]},
                new Avganger{StoppId = 17,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloTrondheim[4]},
                new Avganger{StoppId = 18,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)} },Stopp=stasjonerOsloTrondheim[5]},
            };

                var avgangstiderOslo_Trondheim_Rute = new Ruter
                {

                    Pris = 600,
                    Avganger = avgangstiderOslo_Trondheim
                };

                var BussRute3 = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListe[3],
                    Rute = avgangstiderOslo_Trondheim_Rute
                };


                context.Buss_rute.Add(BussRute3);







                var avgangstiderOslo_Eidsvoll = new List<Avganger>
            {
                new Avganger{StoppId = 19,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloEidsvoll[0] },
                new Avganger{StoppId = 20,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloEidsvoll[1] },
                new Avganger{StoppId = 21,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloEidsvoll[2] },
                new Avganger{StoppId = 22,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloEidsvoll[3] },
                new Avganger{StoppId = 23,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloEidsvoll[4] },
                new Avganger{StoppId = 24,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloEidsvoll[5] },
            };

                var avgangstiderOslo_Eidsvoll_Rute = new Ruter
                {

                    Pris = 50,
                    Avganger = avgangstiderOslo_Eidsvoll
                };

                var BussRute4 = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListe[2],
                    Rute = avgangstiderOslo_Eidsvoll_Rute
                };


                context.Buss_rute.Add(BussRute4);








                var avgangstiderOslo_Gjøvik = new List<Avganger>
            {
                new Avganger{StoppId = 25,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloGjøvik[0]},
                new Avganger{StoppId = 26,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloGjøvik[1]},
                new Avganger{StoppId = 27,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloGjøvik[2]},
                new Avganger{StoppId = 28,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloGjøvik[3]},
                new Avganger{StoppId = 29,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloGjøvik[4]},
                new Avganger{StoppId = 30,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloGjøvik[5]},
            };

                var avgangstiderOslo_Gjøvik_Rute = new Ruter
                {

                    Pris = 200,
                    Avganger = avgangstiderOslo_Gjøvik
                };

                var BussRute5 = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListe[1],
                    Rute = avgangstiderOslo_Gjøvik_Rute
                };


                context.Buss_rute.Add(BussRute5);





                //avgangstider Til Buss Oslo_Kragerø
                var avgangstiderOslo_Kragerø = new List<Avganger>
            {
                new Avganger{StoppId = 31,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloKragerø[0] },
                new Avganger{StoppId = 32,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloKragerø[1] },
                new Avganger{StoppId = 33,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloKragerø[2] },
                new Avganger{StoppId = 34,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloKragerø[3] },
                new Avganger{StoppId = 35,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloKragerø[4] },
                new Avganger{StoppId = 36,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloKragerø[5] }
            }; 

                var avgangstiderOslo_Kragerø_Rute = new Ruter
                {

                    Pris = 300,
                    Avganger = avgangstiderOslo_Kragerø
                };

                var BussRute6 = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListe[0],
                    Rute = avgangstiderOslo_Kragerø_Rute
                };


                context.Buss_rute.Add(BussRute6);





                /////////////////// Retur ////////////////////////////


                var BussListeRetur = new List<Busser>
                 {
                   new Busser { BussNavn = "Kragerø_Oslo" }, new Busser { BussNavn = "Gjøvik_Oslo" },
                     new Busser { BussNavn = "Eidsvoll_Oslo" },new Busser { BussNavn = "Trondheim_Oslo" },
                     new Busser { BussNavn = "Bergen_Oslo" },new Busser { BussNavn = "Larvik_Oslo" }
                 };


                //avgangstider Til Buss Oslo_Larvik
                var avgangstiderOslo_LarvikRetur = new List<Avganger>
            {
                new Avganger{StoppId = 37,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloLarvik[5]},
                new Avganger{StoppId = 38,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloLarvik[4]},
                new Avganger{StoppId = 39,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloLarvik[3]},
                new Avganger{StoppId = 40,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloLarvik[2]},
                new Avganger{StoppId = 41,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloLarvik[1]},
                new Avganger{StoppId = 42,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloLarvik[0]},
            };

                var avgangstiderOslo_Larvik_RuteRetur = new Ruter
                {

                    Pris = 100,
                    Avganger = avgangstiderOslo_LarvikRetur
                };

                var BussRute1Retur = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListeRetur[5],
                    Rute = avgangstiderOslo_Larvik_RuteRetur
                };


                context.Buss_rute.Add(BussRute1Retur);


                // ----------------------------------------

                var avgangstiderOslo_BergenRetur = new List<Avganger>
            {
                new Avganger{StoppId = 43,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloBergen[5]},
                new Avganger{StoppId = 44,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloBergen[4]},
                new Avganger{StoppId = 45,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloBergen[3]},
                new Avganger{StoppId = 46,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloBergen[2]},
                new Avganger{StoppId = 47,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloBergen[1]},
                new Avganger{StoppId = 48,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloBergen[0]},
            };

                var avgangstiderOslo_Bergen_RuteRetur = new Ruter
                {

                    Pris = 400,
                    Avganger = avgangstiderOslo_BergenRetur
                };

                var BussRute2Retur = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListeRetur[4],
                    Rute = avgangstiderOslo_Bergen_RuteRetur
                };


                context.Buss_rute.Add(BussRute2Retur);




                var avgangstiderOslo_TrondheimRetur = new List<Avganger>
            {
                new Avganger{StoppId = 49,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloTrondheim[5]},
                new Avganger{StoppId = 50,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloTrondheim[4]},
                new Avganger{StoppId = 51,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloTrondheim[3]},
                new Avganger{StoppId = 52,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloTrondheim[2]},
                new Avganger{StoppId = 53,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloTrondheim[1]},
                new Avganger{StoppId = 54,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloTrondheim[0]},
            };

                var avgangstiderOslo_Trondheim_RuteRetur = new Ruter
                {

                    Pris = 600,
                    Avganger = avgangstiderOslo_TrondheimRetur
                };

                var BussRute3Retur = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListeRetur[3],
                    Rute = avgangstiderOslo_Trondheim_RuteRetur
                };


                context.Buss_rute.Add(BussRute3Retur);







                var avgangstiderOslo_EidsvollRetur = new List<Avganger>
            {
                new Avganger{StoppId = 55,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloEidsvoll[5] },
                new Avganger{StoppId = 56,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloEidsvoll[4] },
                new Avganger{StoppId = 57,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloEidsvoll[3] },
                new Avganger{StoppId = 58,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloEidsvoll[2] },
                new Avganger{StoppId = 59,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloEidsvoll[1] },
                new Avganger{StoppId = 60,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloEidsvoll[0] },
            };

                var avgangstiderOslo_Eidsvoll_RuteRetur = new Ruter
                {

                    Pris = 50,
                    Avganger = avgangstiderOslo_EidsvollRetur
                };

                var BussRute4Retur = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListeRetur[2],
                    Rute = avgangstiderOslo_Eidsvoll_RuteRetur
                };


                context.Buss_rute.Add(BussRute4Retur);








                var avgangstiderOslo_GjøvikRetur = new List<Avganger>
            {
                new Avganger{StoppId = 61,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloGjøvik[5]},
                new Avganger{StoppId = 62,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloGjøvik[4]},
                new Avganger{StoppId = 63,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloGjøvik[3]},
                new Avganger{StoppId = 64,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloGjøvik[2]},
                new Avganger{StoppId = 65,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloGjøvik[1]},
                new Avganger{StoppId = 66,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)}},Stopp=stasjonerOsloGjøvik[0]},
            };

                var avgangstiderOslo_Gjøvik_RuteRetur = new Ruter
                {

                    Pris = 200,
                    Avganger = avgangstiderOslo_GjøvikRetur
                };

                var BussRute5Retur = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListeRetur[1],
                    Rute = avgangstiderOslo_Gjøvik_RuteRetur
                };


                context.Buss_rute.Add(BussRute5Retur);





                //avgangstider Til Buss Oslo_Kragerø
                var avgangstiderOslo_KragerøRetur = new List<Avganger>
            {
                new Avganger{StoppId = 67,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(11,00,00)}},Stopp=stasjonerOsloKragerø[5] },
                new Avganger{StoppId = 68, Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(12,00,00)}},Stopp=stasjonerOsloKragerø[4] },
                new Avganger{StoppId = 69,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(13,00,00)}},Stopp=stasjonerOsloKragerø[3] },
                new Avganger{StoppId = 70,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(14,00,00)}},Stopp=stasjonerOsloKragerø[2] },
                new Avganger{StoppId = 71,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(15,00,00)}},Stopp=stasjonerOsloKragerø[1] },
                new Avganger{StoppId = 72,Tider = new List<Avgangstider>{ new Avgangstider { Tid = new TimeSpan(16,00,00)} }, Stopp=stasjonerOsloKragerø[0] }
            };

                var avgangstiderOslo_Kragerø_RuteRetur = new Ruter
                {

                    Pris = 300,
                    Avganger = avgangstiderOslo_KragerøRetur
                };

                var BussRute6Retur = new Buss_Rute
                {
                    TidFra = new TimeSpan(11, 00, 00),
                    TidTil = new TimeSpan(16, 00, 00),
                    Buss = BussListeRetur[0],
                    Rute = avgangstiderOslo_Kragerø_RuteRetur
                };


                context.Buss_rute.Add(BussRute6Retur);










                // lag en påoggingsbruker
                var bruker = new Brukere();
                bruker.Brukernavn = "Admin";
                string passord = "Test12";
                byte[] salt = BillettRepository.LagSalt();
                byte[] hash = BillettRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                context.Brukere.Add(bruker);
                
                context.SaveChanges();

            }
        }
    }
}