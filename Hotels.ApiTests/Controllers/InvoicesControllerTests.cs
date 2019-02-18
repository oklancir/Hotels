using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hotels.ApiTests.Controllers
{
    [TestClass]
    public class InvoicesControllerTests
    {
        IHotelsContext context  = new HotelsContext();
        private int existingGuestId;
        private int nonexistentGuestId;

        private int latestReservationId;
        private int reservationNoInvoiceId;
        private int latestInvoiceId;

        [TestInitialize]
        public void InvoicesControllerTestsSetup()
        {
            var room = context.Rooms.Add(new Room() { RoomTypeId = 1, Name = "ApiTESTROOM"});

            var guest = context.Guests.Add(new Guest()
            {
                FirstName = "Gregori",
                LastName = "Grgur",
                Address = "Gregorova 3",
                Email = "greg@gregich.eu",
                PhoneNumber = "0918237433"
            });
            context.SaveChanges();
            existingGuestId = guest.Id;
            nonexistentGuestId = -1;
            
            var reservation = context.Reservations.Add(new Reservation()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(10),
                Guest = guest,
                Room = room,
                ReservationStatusId = 1
            });

            var reservationNoInvoice = context.Reservations.Add(new Reservation()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(10),
                Guest = guest,
                Room = room,
                ReservationStatusId = 1
            });

            context.SaveChanges();
            reservationNoInvoiceId = reservation.Id;

            var invoice = context.Invoices.Add(new Invoice()
            {
                Reservation = reservation,
                IsPaid = false
            });

            context.SaveChanges();
            latestInvoiceId = invoice.Id;
        }


        [TestCleanup]
        public void InvoicesControllerTestsCleanup()
        {
            

        }

        [TestMethod]
        public async Task GetInvoicesTest_WhenCalled_ReturnsListOfInvoiceObjects()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/invoices");
            IEnumerable<InvoiceDto> invoices = null;

            if (response.IsSuccessStatusCode)
            {
                invoices = await response.Content.ReadAsAsync<IEnumerable<InvoiceDto>>();
            }

            Assert.IsInstanceOfType(invoices, typeof(IEnumerable<InvoiceDto>), "Invoices list request failed.");
        }

        [TestMethod]
        public async Task GetInvoice_WhenIdIsValid_ReturnsInvoiceDto()
        {
            var invoiceId = latestInvoiceId;
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;

            var response = await client.GetAsync("api/invoices/" + invoiceId);
            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNotNull(invoiceDto, "Request failed.");
            Assert.AreEqual(invoiceDto.GetType(), typeof(InvoiceDto), "Invoice not returned successfully.");
        }

        [TestMethod]
        public async Task GetInvoice_WhenIdNotValid_ReturnsFailedRequest()
        {
            var id = -1;
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;

            var response = await client.GetAsync("api/invoices/" + id);
            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNull(invoiceDto, "Request success.");
        }

        [TestMethod]
        public async Task CreateInvoice_WhenInvoiceIsValid_ReturnsInvoiceDto()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var invoiceToCreate = MockInvoice();
            var response = await client.PostAsJsonAsync("api/invoices", Mapper.Map<Invoice, InvoiceDto>(invoiceToCreate));

            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNotNull(invoiceDto);
            Assert.IsInstanceOfType(invoiceDto, typeof(InvoiceDto), "Invoice not successfully added");
        }

        [TestMethod]
        public async Task CreateInvoice_WhenInvoiceIsNull_ReturnsNullInvoiceDto()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var response = await client.PostAsJsonAsync("api/invoices", Mapper.Map<Invoice, InvoiceDto>(null));

            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNull(invoiceDto, "Trying to post an Invoice that is not null");
        }

        [TestMethod]
        public async Task EditInvoice_WithValidInvoiceId_ReturnsUpdatedInvoiceObject()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var invoiceToUpdate = context.Invoices.Find(latestInvoiceId);
            invoiceToUpdate.IsPaid = true;
            var response = await client.PutAsJsonAsync("api/invoices/" + invoiceToUpdate.Id, Mapper.Map<Invoice, InvoiceDto>(invoiceToUpdate));

            var updatedInvoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();

            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.IsNotNull(updatedInvoiceDto);
            Assert.AreEqual(true, updatedInvoiceDto.IsPaid, "IsPaid not updated successfully");
            Assert.IsInstanceOfType(updatedInvoiceDto, typeof(InvoiceDto), "InvoiceDto object not returned");
        }

        [TestMethod]
        public async Task DeleteInvoice_WhenCalledWithValidId_ReturnsDeletedObject()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var invoiceToDelete = GetInvoiceToDelete().GetAwaiter().GetResult();
            var response = await client.DeleteAsync($"api/invoices/{invoiceToDelete.Id}");

            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNotNull(invoiceDto);
            Assert.AreEqual(invoiceToDelete.Id, invoiceDto.Id, "Invoice Id is not valid");
            Assert.IsInstanceOfType(invoiceDto, typeof(InvoiceDto), "Object not deleted successfully");
        }



        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Invoice MockInvoice()
        {
            return new Invoice
            {
                ReservationId = reservationNoInvoiceId,
                TotalAmount = 10000,
                IsPaid = false
            };
        }

        private Invoice SaveInvoiceToDb()
        {
            var Context = new HotelsContext();

            var testInvoice = new Invoice
            {
                ReservationId = 145,
                ItemId = 1,
                TotalAmount = 10000,
                IsPaid = false
            };

            //if (Context.Invoices.Find(testInvoice.Id) == null)
            //{
                var savedInvoice = Context.Invoices.Add(testInvoice);
                Context.SaveChanges();
            //}

            return savedInvoice;
        }

        private async Task<InvoiceDto> GetInvoiceToDelete()
        {
            var client = GetHttpClient();
            var invoiceToCreate = MockInvoice();
            var response = await client.PostAsJsonAsync("api/invoices", Mapper.Map<Invoice, InvoiceDto>(invoiceToCreate));

            if (!response.IsSuccessStatusCode)
                return null;

            var invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            return invoiceDto;
        }
    }
}
