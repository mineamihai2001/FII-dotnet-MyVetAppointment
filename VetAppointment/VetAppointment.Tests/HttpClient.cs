using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using VetAppointment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VetAppointment.API.DTOs.Create;
using System;
using System.Net.Http;
using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace VetAppointment.Tests
{
    [TestClass]
    public class HttpClient<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DatabaseContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<DatabaseContext>(options =>
                {
                    //options.UseInMemoryDatabase("InMemoryEmployeeTest");
                    options.UseSqlite("Data Source = Tests.db");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                    }
                    catch (Exception e)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }
    }
}
