
using FluentAssertions;
using System.Net.Http.Json;
using VetAppointment.API.DTOs.Create;
using VetAppointment.API.DTOs.Update;
using VetAppointment.Domain.Models;

namespace VetAppointment.Tests.IntegrationTests
{
    public class MedicsControllerTests: BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "api/medics";

       
        [Fact]
        public async void When_CreatedMedic_Then_ShouldReturnMedicInTheGetRequest()
        {
            //Arrange
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

        [Fact]
        public async void When_RegisterClientsToMedic_Then_ShouldSaveClientsInMedic()
        {
            //Arrange
            CreateMedicDto medicDto = new CreateMedicDto(
                "Iordache Alexandru",
                "0761452890",
                "iordache.alexandru@gmail.com"
            );
            var createMedicResponsePOST = await HttpClient.PostAsJsonAsync(ApiURL, medicDto);
            var clients = new List<CreateClientDto>
            { 
                new CreateClientDto(
                "Dragomir Ionut", 
                "0765321908", 
                "dragomir.ionut@gmail.com", 
                "Iasi")
            };
            var createMedicResponseGET = await HttpClient.GetAsync(ApiURL);
            var medicResponse = await createMedicResponseGET.Content.ReadFromJsonAsync<List<Medic>>();
            medicResponse.Should().NotBeNull();
            medicResponse.Count.Should().BeGreaterThan(0);
            
            //Act
            var resultResponse = await HttpClient.PostAsJsonAsync($"{ApiURL}/{medicResponse[0].Id}/clients", clients);
            
            //Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        }

        [Fact]
        public async void When_UpdateMedic_Then_ShouldReturnMedicInTheGetRequest()
        {
            //Arrange
            CreateMedicDto createMedicDto = new CreateMedicDto(
               "Iordache Alexandru",
               "0761452890",
               "iordache.alexandru@gmail.com"
            );
            var createMedicResponsePOST = await HttpClient.PostAsJsonAsync(ApiURL, createMedicDto);
            var createMedicResponseGET = await HttpClient.GetAsync(ApiURL);
            var medicResponse = await createMedicResponseGET.Content.ReadFromJsonAsync<List<Medic>>();
            medicResponse.Should().NotBeNull();

            UpdateMedicDto updateMedicDto = new UpdateMedicDto(
                medicResponse[0].Id,
                medicResponse[0].Name,
                "0761453333",
                medicResponse[0].EmailAddress);

            //Act
            var createMedicResponsePUT = await HttpClient.PutAsJsonAsync(ApiURL, updateMedicDto);

            //Assert
            createMedicResponsePUT.EnsureSuccessStatusCode();
            createMedicResponsePUT.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        }

        [Fact]
        public async void When_DeleteMedic_Then_ShouldReturnOk()
        {
            //Arrange
            CreateMedicDto medicDto = new CreateMedicDto(
                "Iordache Alexandru",
                "0761452890",
                "iordache.alexandru@gmail.com"
            );
            var createMedicResponsePOST = await HttpClient.PostAsJsonAsync(ApiURL, medicDto);
            var createMedicResponseGET = await HttpClient.GetAsync(ApiURL);
            var medicResponse = await createMedicResponseGET.Content.ReadFromJsonAsync<List<Medic>>();
            medicResponse.Should().NotBeNull();

            //Act
            var resultResponse = await HttpClient.DeleteAsync($"{ApiURL}/{medicResponse[0].Id}");

            //Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        public void Dispose()
        {
            CleanDatabases();
        }

    }
}
