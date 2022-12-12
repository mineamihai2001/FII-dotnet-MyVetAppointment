using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using VetAppointment.API.Controllers;
using VetAppointment.Infrastructure;

namespace VetAppointment.Tests.IntegrationTests
{
    public class BaseClientsTests
    {
        private DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseSqlite("Data Source = MyTests.db").Options;
        private DatabaseContext databaseContext;
        protected HttpClient HttpClient { get; private set; }

        //private DatabaseContext databaseContext;

        protected BaseClientsTests()
        {
            var application = new WebApplicationFactory<ClientsController>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            databaseContext = new DatabaseContext();
            //CleanDatabases();
        }
    }
}
