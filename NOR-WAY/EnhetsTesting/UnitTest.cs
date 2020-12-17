using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Oblig1.Controllers;
using Oblig1.DAL;
using Oblig1.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EnhetsTesting
{
    public class UnitTest
    {



        private const string _loggetInn = "LoggetInn";
        private const string _ikkeLoggetInn = "";



        private readonly Mock<IBillettRepository> mockRep = new Mock<IBillettRepository>();
        private readonly Mock<ILogger<BillettController>> mockLog = new Mock<ILogger<BillettController>>();



        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();




        [Fact]
        public async Task HentAlleRuterOK()
        {



            var Avgangstider = new Avgangstider { TidId = 1, Tid = new TimeSpan(12, 00, 00) };



            var Avgangstider1 = new Avgangstider { TidId = 2, Tid = new TimeSpan(14, 00, 00) };



            var listAvgang = new List<Avgangstider>();



            listAvgang.Add(Avgangstider);
            listAvgang.Add(Avgangstider1);



            var Stasjoner = new Stasjoner { StasjonId = 1, StasjonNavn = "Stovner" };



            var Buss = new Busser { BussId = 1, BussNavn = "Oslo" };



            var Rute = new Ruter { RuteId = 1, Pris = 10 };



            var Avgang = new Avganger { StoppId = 1, Stopp = Stasjoner, Tider = listAvgang, Rute = Rute };



            var Avgang1 = new Avganger { StoppId = 2, Stopp = Stasjoner, Tider = listAvgang, Rute = Rute };



            var listAvganger = new List<Avganger>();



            listAvganger.Add(Avgang);
            listAvganger.Add(Avgang1);



            var bussrute1 = new Buss_Rute { Buss_RuteId = 1, TidFra = new TimeSpan(10, 00, 00), TidTil = new TimeSpan(16, 00, 00), Buss = Buss, Rute = Rute };

            var ruteliste = new List<Buss_Rute>();


            mockRep.Setup(k => k.HentAlleRuter()).ReturnsAsync(ruteliste);



            var billettController = new BillettController(mockRep.Object, mockLog.Object);



            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            billettController.ControllerContext.HttpContext = mockHttpContext.Object;



            // Act
            var resultat = await billettController.HentAlleRuter() as OkObjectResult;



            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Buss_Rute>>((List<Buss_Rute>)resultat.Value, ruteliste);




        }



        [Fact]
        public async Task HentAlleRuterIkkeOK()
        {



            mockRep.Setup(k => k.HentAlleRuter()).ReturnsAsync(It.IsAny<List<Buss_Rute>>());



            var billettController = new BillettController(mockRep.Object, mockLog.Object);



            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            billettController.ControllerContext.HttpContext = mockHttpContext.Object;



            // Act
            var resultat = await billettController.HentAlleRuter() as UnauthorizedObjectResult;



            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }



        [Fact]
        public async Task SjekkRute()
        {



            mockRep.Setup(k => k.LeggTilRute(It.IsAny<Rute>())).ReturnsAsync(true);



            var billettController = new BillettController(mockRep.Object, mockLog.Object);



            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            billettController.ControllerContext.HttpContext = mockHttpContext.Object;



            // Act
            var resultat = await billettController.LeggTilRute(It.IsAny<Rute>()) as OkObjectResult;



            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rute lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnIkkeOK()
        {
            // Arrange



            mockRep.Setup(k => k.LeggTilRute(It.IsAny<Rute>())).ReturnsAsync(false);



            var billettController = new BillettController(mockRep.Object, mockLog.Object);



            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            billettController.ControllerContext.HttpContext = mockHttpContext.Object;



            // Act
            var resultat = await billettController.LeggTilRute(It.IsAny<Rute>()) as BadRequestObjectResult;



            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Rute kunne ikke lagres", resultat.Value);
        }




        [Fact]
        public async Task SJekkLeggTilRute()
        {
            mockRep.Setup(k => k.LeggTilRute(It.IsAny<Rute>())).ReturnsAsync(true);

            var billettController = new BillettController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            billettController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await billettController.LeggTilRute(It.IsAny<Rute>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LoggInnOK()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var billettController = new BillettController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            billettController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await billettController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }



        [Fact]
        public void LoggUtOk()
        {
            var billettController = new BillettController(mockRep.Object, mockLog.Object);



            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            billettController.ControllerContext.HttpContext = mockHttpContext.Object;



            // Act
            billettController.LoggUt();



            // Assert
            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }


    }
}
