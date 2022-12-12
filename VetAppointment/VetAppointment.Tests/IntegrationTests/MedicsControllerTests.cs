
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using VetAppointment.API.DTOs.Create;


namespace VetAppointment.Tests.IntegrationTests
{
    public class MedicsControllerTests: BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "api/medics";

       
        [Fact]
        public async void When_CreatedMedic_Then_ShouldReturnMedicInTheGetRequest()
        {
            CreateMedicDto medicDto = new CreateMedicDto (
                "Iordache Alexandru",
                "0761452890",
                "iordache.alexandru@gmail.com"
            );
            //Act
            var createMedicResponse = await HttpClient.PostAsJsonAsync(ApiURL, medicDto);
            var getMedicResult = await HttpClient.GetAsync(ApiURL);
            //Assert
            createMedicResponse.EnsureSuccessStatusCode();
            createMedicResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        }

        private static CreateMedicDto CreateSUT()
        {
            return new CreateMedicDto
            (
                "Iordache Alexandru",
                "0761452890",
                "iordache.alexandru@gmail.com"
            );
        }

        public void Dispose()
        {
            CleanDatabases();
        }

    }
}
