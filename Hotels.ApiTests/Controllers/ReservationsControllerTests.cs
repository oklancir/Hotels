using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hotels.ApiTests.Controllers
{
    [TestClass]
    public class ReservationsControllerTests
    {
        private IHotelsContext Context = new HotelsContext();
        private int existingGuestId;
        private int nonexistentGuestId;
        private int latestRoomId;
        private int latestReservationId;
        private int latestInvoiceId;
        private int latestItemId;
        private int latestServiceProductId;

        [TestInitialize]
        public void ItemsControllerTestsSetup()
        {
            var room = Context.Rooms.Add(new Room() { RoomTypeId = 1, Name = "ApiTESTROOM" });

            Context.SaveChanges();
            latestRoomId = room.Id;

            var guest = Context.Guests.Add(new Guest()
            {
                FirstName = "Gregori",
                LastName = "Grgur",
                Address = "Gregorova 3",
                Email = "greg@gregich.eu",
                PhoneNumber = "0918237433"
            });

            Context.SaveChanges();
            existingGuestId = guest.Id;
            nonexistentGuestId = -1;

            var reservation = Context.Reservations.Add(new Reservation()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(10),
                Guest = guest,
                Room = room,
                ReservationStatusId = 1
            });

            Context.SaveChanges();
            latestReservationId = reservation.Id;

            var invoice = Context.Invoices.Add(new Invoice()
            {
                Reservation = reservation,
                IsPaid = false
            });

            Context.SaveChanges();
            latestInvoiceId = invoice.Id;

            var serviceProduct = Context.ServiceProducts.Add(new ServiceProduct()
            {
                Name = "TestServiceProduct",
                Price = 20
            });

            Context.SaveChanges();
            latestServiceProductId = serviceProduct.Id;

            var item = Context.Items.Add(new Item()
            {
                InvoiceId = latestInvoiceId,
                ServiceProductId = latestServiceProductId,
                Quantity = 1
            });

            Context.SaveChanges();
            latestItemId = item.Id;
        }

        [TestMethod]
        public async Task GetReservationsTest()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/reservations");
            IEnumerable<ReservationDto> reservations = null;

            if (response.IsSuccessStatusCode)
            {
                reservations = await response.Content.ReadAsAsync<IEnumerable<ReservationDto>>();
            }

            Assert.IsInstanceOfType(reservations, typeof(IEnumerable<ReservationDto>), "Reservations list request failed.");
        }

        [TestMethod]
        public async Task GetReservation_WhenIdIsValid_ReturnsReservationDto()
        {
            var id = latestReservationId;
            var client = GetHttpClient();
            ReservationDto reservationDto = null;

            var response = await client.GetAsync("api/reservations/" + id);
            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.IsNotNull(reservationDto, "Request failed.");
            Assert.AreEqual(reservationDto.GetType(), typeof(ReservationDto), "ReservationDto not returned.");
        }
        [TestMethod]
        public async Task GetReservation_WhenIdNotValid_ReturnsReservationDto()
        {
            var id = -1;
            var client = GetHttpClient();
            ReservationDto reservationDto = null;

            var response = await client.GetAsync("api/reservations/" + id);
            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.IsNull(reservationDto, "Request success, reservation id is valid.");
        }

        [TestMethod]
        public async Task CreateReservation_WhenReservationDtoIsValid_ReturnsReservationDto()
        {
            var client = GetHttpClient();
            ReservationDto reservationDto = null;
            var reservationToCreate = new Reservation
            {
                Discount = 20,
                RoomId = latestRoomId,
                StartDate = DateTime.Today.AddDays(15),
                EndDate = DateTime.Today.AddDays(19),
                GuestId = existingGuestId,
                ReservationStatusId = 1
            };

            var response = await client.PostAsJsonAsync("api/reservations", Mapper.Map<Reservation, ReservationDto>(reservationToCreate));

            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Expected status code 200.");
            Assert.IsNotNull(reservationDto);
            Assert.IsInstanceOfType(reservationDto, typeof(ReservationDto), "Reservation not added successfully");
        }

        [TestMethod]
        public async Task CreateReservation_WhenReservationDtoIsNull_ReturnsNullGuestDto()
        {
            var client = GetHttpClient();
            ReservationDto reservationDto = null;
            var response = await client.PostAsJsonAsync("api/reservations", Mapper.Map<Reservation, ReservationDto>(null));

            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.IsNull(reservationDto, "Trying to post a Reservation that is not null.");
        }

        [TestMethod]
        public async Task EditReservation_WithVaildReservationId_ReturnsUpdatedReservationObject()
        {
            var client = GetHttpClient();
            ReservationDto reservationDto = null;
            var reservationToUpdate = Context.Reservations.Find(latestReservationId);
            reservationToUpdate.ReservationStatusId = 3;
            var response = await client.PutAsJsonAsync("api/reservations/" + reservationToUpdate.Id,
                Mapper.Map<Reservation, ReservationDto>(reservationToUpdate));

            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Expected status code 200.");
            Assert.IsNotNull(reservationDto);
            Assert.AreEqual(3, reservationDto.ReservationStatusId, "Reservation status not updated succesfully");
            Assert.IsInstanceOfType(reservationDto, typeof(ReservationDto), "Not a valid ReservationDto object.");
        }

        [TestMethod]
        public async Task DeleteReservation_WhenCalledWithValidId_ReturnsDeletedObject()
        {
            var client = GetHttpClient();
            var reservationToDelete = GetReservationToDelete().GetAwaiter().GetResult();
            var response = await client.DeleteAsync($"api/reservations/{reservationToDelete.Id}");

            var result = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Expected status code 200.");
            Assert.AreEqual($"\"Reservation {reservationToDelete.Id} successfully removed.\"", result);
        }



        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Reservation MockReservation()
        {
            return new Reservation
            {
                StartDate = DateTime.Today.AddDays(60),
                EndDate = DateTime.Today.AddDays(53),
                RoomId = latestRoomId,
                GuestId = existingGuestId,
                ReservationStatusId = 1
            };
        }

        private Reservation MockReservationToDb()
        {
            var Context = new HotelsContext();

            var testReservation = new Reservation
            {
                StartDate = DateTime.Today.AddDays(-90),
                EndDate = DateTime.Today.AddDays(-80),
                GuestId = 11,
                RoomId = 5,
                ReservationStatusId = 1
            };

            var savedReservation = Context.Reservations.Add(testReservation);
            Context.SaveChanges();

            return savedReservation;
        }

        private async Task<ReservationDto> GetReservationToDelete()
        {
            var client = GetHttpClient();
            var reservationToCreate = MockReservation();
            var response = await client.PostAsJsonAsync("api/reservations", Mapper.Map<Reservation, ReservationDto>(reservationToCreate));

            if (!response.IsSuccessStatusCode)
                return null;

            var reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            return reservationDto;
        }
    }
}
