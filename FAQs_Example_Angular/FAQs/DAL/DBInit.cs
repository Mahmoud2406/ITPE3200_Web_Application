using mappe3.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mappe3.DAL
{
    public static class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
            
            var db = serviceScope.ServiceProvider.GetService<FaqContext>();

            // må slette og opprette databasen hver gang når den skal initieres (seed`es)
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var rating = new Ratings
            {
                Rating = 943,
            };


            var question = new Questions
            {
                Question = "Hvordan ser jeg alle tilgjengeligereiser fra en stasjon?",
                Answer = "Hvis du velger en stasjon og endrer på tiden ifra denne stasjonen, vil du få nærmeste " +
                "tilgjengelige avgang basert på hva du har satt som tid",
                Category = "billett",
                Rating= rating
            };

        


            db.Questions.Add(question);
            db.Ratings.Add(rating);

            var rating2 = new Ratings
            {
                Rating = 654,
            };

            var question2 = new Questions
            {
                Question = "Hvordan betaler jeg med vipps?",
                Answer = "Vi har dessverre ikke en betalingsløsning for vipps",
                Category = "kjøpsvilkår",
                Rating = rating2
            };

           


            db.Questions.Add(question2);
            db.Ratings.Add(rating2);




            var rating3 = new Ratings
            {
                Rating = 655,
            };


      
            var question3 = new Questions
            {
                Question = "Hvordan er det jeg kan se avgangstider for 1 måned frem i tid?",
                Answer = "Vi har en kalender under (fra / til) dato som du kan bruke for å sette bestemt dato og se tilgjengelige avganger gitt den bestemte datoen",
                Category = "avganger",
                Rating = rating3
            };




            db.Questions.Add(question3);
            db.Ratings.Add(rating3);




            var rating4 = new Ratings
            {
                Rating = 25,
            };

            var question4 = new Questions
            {
                Question = "Hvor finner jeg billetten min etter å ha bestilt den?",
                Answer = "Billetten din er i form av en QR kode som blir tilsendt til deg via epost.",
                Category = "billett",
                Rating = rating4
            };




            db.Questions.Add(question4);
            db.Ratings.Add(rating4);



            var rating5 = new Ratings
            {
                Rating = 26,
            };

            var question5 = new Questions
            {
                Question = "Hva er cvc feltet for?",
                Answer = "CVC er et tresifret tall som må være med sammen med kortnummer ved et kjøp",
                Category = "kjøpsvilkår",
                Rating = rating5
            };




            db.Questions.Add(question5);
            db.Ratings.Add(rating5);




            var rating6 = new Ratings
            {
                Rating = 29,
            };

            var question6 = new Questions
            {
                Question = "Betaler jeg like mye om jeg reiser med barnebillett vs voksen?",
                Answer = "Hver rute har en spesifikk pris. Billettypene har også spesifikke priser uavhengig av ruter. Om du bestiller en rute som koster 100kr med voksenbillett, vil det tilsvare 200kr",
                Category = "avganger",
                Rating = rating6
            };




            db.Questions.Add(question6);
            db.Ratings.Add(rating6);




            var rating7 = new Ratings
               {
                          Rating = 0,
                      };

            var question7 = new Questions
            {
                Question = "Hvordan angrer jeg et kjøp?",
                Answer = "",
                Category = "avganger",
                Rating = rating7
            };




            db.Questions.Add(question7);
            db.Ratings.Add(rating7);

            db.SaveChanges();

        }
    }  
}
