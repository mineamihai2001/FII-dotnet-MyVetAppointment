using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;

namespace VetAppointment.Tests
{
    [TestClass] 
    public class IntegrationTest
    {
        private System.Net.Http.HttpClient httpClient;

        public IntegrationTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async void DefaultRoutes()
        {
            var respone = await httpClient.GetAsync("");
            var stringResult = await respone.Content.ReadAsStringAsync();

            Assert.AreEqual("Working", stringResult);
        }

        [TestMethod]
        public async void ClientsRoutes()
        {
            var response = await httpClient.GetAsync("/Clients");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }


        [TestMethod]
        public async void MedicsRoutes()
        {
            var response = await httpClient.GetAsync("/Medics");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }

        [TestMethod]
        public async void MedicineRoutes()
        {
            var response = await httpClient.GetAsync("/Medicine");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }

        [TestMethod]
        public async void AppointmentRoutes()
        {
            var response = await httpClient.GetAsync("/Appoinments");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }

        [TestMethod]
        public async void BillsRoutes()
        {
            var response = await httpClient.GetAsync("/Bills");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }

        [TestMethod]
        public async void NursesRoutes()
        {
            var response = await httpClient.GetAsync("/Nurses");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }

        [TestMethod]
        public async void PatientsRoutes()
        {
            var response = await httpClient.GetAsync("/Patients");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }

        [TestMethod]
        public async void RoomsRoutes()
        {
            var response = await httpClient.GetAsync("/Rooms");
            var stringResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
        }
    }
}