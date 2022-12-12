using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace VetAppointment.Tests.IntegrationTests
{
    [TestClass]
    public class ClientsTests:BaseIntegrationTests
    {
        [TestMethod]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await HttpClient.GetAsync("api/Clients");
            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
