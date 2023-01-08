using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using VetAppointment.API.Controllers;
using VetAppointment.Infrastructure;

namespace VetAppointment.Tests.IntegrationTests
{
    public class BaseIntegrationTests
    {
        private DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseSqlite("Data Source = MyTests.db").Options;
        private DatabaseContext databaseContext;
        protected HttpClient HttpClient { get; private set; }

        protected BaseIntegrationTests()
        {
            var application = new WebApplicationFactory<MedicsController>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            databaseContext = new DatabaseContext(options);
        }

        protected void CleanDatabases()
        {
            databaseContext.Medics.RemoveRange(databaseContext.Medics.ToList());
            databaseContext.SaveChanges();
            databaseContext.Database.EnsureDeleted();
        }
    }
}
