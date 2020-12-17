using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Oblig1.Model;
using Oblig1.Controllers;
using Oblig1.DAL;




namespace Testing
{
    public class Testing
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IBillettRepository> mockRep = new Mock<IBillettRepository>();
        private readonly Mock<ILogger<BillettController>> mockLog = new Mock<ILogger<BillettController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

      
    }
}
