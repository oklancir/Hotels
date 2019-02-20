using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Hotels.MvcApplication;

namespace Hotels.ApiTests.Controllers
{
    [TestClass]
    public class GuestsControllerTests
    {

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            AutoMapperConfiguration.Configure();
        }


        [TestMethod]
        public async Task GetGuestsTest()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/guests");
            IEnumerable<GuestDto> guests = null;

            if (response.IsSuccessStatusCode)
            {
                guests = await response.Content.ReadAsAsync<IEnumerable<GuestDto>>();
            }

            Assert.IsInstanceOfType(guests, typeof(IEnumerable<GuestDto>), "Guests list request failed.");
        }

        [TestMethod]
        public async Task GetGuest_WhenIdIsValid_ReturnsGuestDto()
        {
            var id = 12;
            var client = GetHttpClient();
            GuestDto guestDto = null;

            var response = await client.GetAsync("api/guests/" + id);
            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNotNull(guestDto, "Request success.");
            Assert.AreEqual(guestDto.GetType(), typeof(GuestDto), "GuestDto not returned.");
        }
        [TestMethod]
        public async Task GetGuest_WhenIdNotValid_ReturnsGuestDto()
        {
            var id = -1;
            var client = GetHttpClient();
            GuestDto guestDto = null;

            var response = await client.GetAsync("api/guests/" + id);
            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNull(guestDto, "Guest not null.");
        }

        [TestMethod]
        public async Task CreateGuest_WhenGuestDtoIsValid_ReturnsGuestDto()
        {
            var client = GetHttpClient();
            GuestDto guestDto = null;
            var guestToCreate = MockGuest();
            var response = await client.PostAsJsonAsync("api/guests", Mapper.Map<Guest, GuestDto>(guestToCreate));

            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNotNull(guestDto);
            Assert.IsInstanceOfType(guestDto, typeof(GuestDto), "Guest creation failed");
        }

        [TestMethod]
        public async Task CreateGuest_WhenGuestDtoIsNull_ReturnsNullGuestDto()
        {
            var client = GetHttpClient();
            GuestDto guestDto = null;
            var response = await client.PostAsJsonAsync("api/guests", Mapper.Map<Guest, GuestDto>(null));

            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNull(guestDto, "Guest should be null.");
        }

        [TestMethod]
        public async Task EditGuest_WithVaildGuestId_ReturnsUpdatedGuestObject()
        {
            var client = GetHttpClient();
            GuestDto guestDto = null;
            var guestToUpdate = MockGuestToDb();
            guestToUpdate.FirstName = "UpdatedTestName";
            var response = await client.PutAsJsonAsync("api/guests/" + guestToUpdate.Id, Mapper.Map<Guest, GuestDto>(guestToUpdate));

            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNotNull(guestDto);
            Assert.AreEqual("UpdatedTestName", guestDto.FirstName, "FirstName did not update succesfully");
            Assert.IsInstanceOfType(guestDto, typeof(GuestDto), "GuestDto object not returned");
        }

        [TestMethod]
        public async Task DeleteGuest_WhenCalledWithValidId_ReturnsDeletedObject()
        {
            var client = GetHttpClient();
            GuestDto guestDto = null;
            var guestToDelete = GetGuestToDelete().GetAwaiter().GetResult();
            var response = await client.DeleteAsync($"api/guests/{guestToDelete.Id}");

            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNotNull(guestDto);
            Assert.AreEqual(guestToDelete.Id, guestDto.Id, "Guest Id is not valid");
            Assert.IsInstanceOfType(guestDto, typeof(GuestDto), "Object not deleted successfully");
        }



        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Guest MockGuest()
        {
            return new Guest
            {
                Id = 8,
                FirstName = "TestingRusk",
                LastName = "TestRussian",
                Address = "TestAddress",
                Email = "test@test.it",
                PhoneNumber = "123456789"
            };
        }

        private Guest MockGuestToDb()
        {
            var Context = new HotelsContext();

            var testGuest = new Guest
            {
                Id = 8,
                FirstName = "TestingRuskDb",
                LastName = "TestRussianDb",
                Address = "TestAddressDb",
                Email = "test@test.it",
                PhoneNumber = "123456789"
            };

            if (Context.Guests.Find(testGuest.Id) == null)
            {
                Context.Guests.Add(testGuest);
                Context.SaveChanges();
            }

            return testGuest;
        }

        private async Task<GuestDto> GetGuestToDelete()
        {
            var client = GetHttpClient();
            var guestToCreate = MockGuest();
            var response = await client.PostAsJsonAsync("api/guests", Mapper.Map<Guest, GuestDto>(guestToCreate));

            if (!response.IsSuccessStatusCode)
                return null;

            var guestDto = await response.Content.ReadAsAsync<GuestDto>();
            return guestDto;
        }
    }
}
