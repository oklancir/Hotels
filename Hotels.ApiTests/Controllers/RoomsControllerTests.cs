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

namespace Hotels.ApiTests.Controllers
{
    [TestClass]
    public class RoomsControllerTests
    {
        private IHotelsContext Context = new HotelsContext();
        private int latestRoomId;

        [TestInitialize]
        public void ItemsControllerTestsSetup()
        {
            var room = Context.Rooms.Add(new Room() { RoomTypeId = 1, Name = "ApiTESTROOM" });

            Context.SaveChanges();
            latestRoomId = room.Id;
        }

        [TestMethod]
        public async Task GetRooms_WhenCalled_ReturnsRoomsList()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/rooms");
            IEnumerable<RoomDto> rooms = null;

            if (response.IsSuccessStatusCode)
            {
                rooms = await response.Content.ReadAsAsync<IEnumerable<RoomDto>>();
            }

            Assert.IsInstanceOfType(rooms, typeof(IEnumerable<RoomDto>), "Rooms list request failed.");
        }

        [TestMethod]
        public async Task GetRoom_WhenIdIsValid_ReturnsRoomDto()
        {
            var id = latestRoomId;
            var client = GetHttpClient();
            RoomDto roomDto = null;

            var response = await client.GetAsync("api/rooms/" + id);
            if (response.IsSuccessStatusCode)
            {
                roomDto = await response.Content.ReadAsAsync<RoomDto>();
            }

            Assert.IsNotNull(roomDto, "Request success.");
            Assert.AreEqual(roomDto.GetType(), typeof(RoomDto), "RoomDto returned.");
        }
        [TestMethod]
        public async Task GetRoom_WhenIdNotValid_ReturnsFailedRequest()
        {
            var id = -1;
            var client = GetHttpClient();
            RoomDto roomDto = null;

            var response = await client.GetAsync("api/rooms/" + id);
            if (response.IsSuccessStatusCode)
            {
                roomDto = await response.Content.ReadAsAsync<RoomDto>();
            }

            Assert.IsNull(roomDto, "Request success, should fail.");
        }

        [TestMethod]
        public async Task CreateRoom_WhenRoomDtoIsValid_ReturnsRoomDto()
        {
            var client = GetHttpClient();
            RoomDto roomDto = null;
            var roomToCreate = Context.Rooms.Find(latestRoomId);
            var response = await client.PostAsJsonAsync("api/rooms", Mapper.Map<Room, RoomDto>(roomToCreate));

            if (response.IsSuccessStatusCode)
            {
                roomDto = await response.Content.ReadAsAsync<RoomDto>();
            }

            Assert.IsNotNull(roomDto);
            Assert.IsInstanceOfType(roomDto, typeof(RoomDto), "Room not added successfully");
        }

        [TestMethod]
        public async Task CreateRoom_WhenRoomDtoIsNull_ReturnsNullRoomObject()
        {
            var client = GetHttpClient();
            RoomDto roomDto = null;
            var response = await client.PostAsJsonAsync("api/rooms", Mapper.Map<Room, RoomDto>(null));

            if (response.IsSuccessStatusCode)
            {
                roomDto = await response.Content.ReadAsAsync<RoomDto>();
            }

            Assert.IsNull(roomDto, "Trying to post empty Item");
        }

        [TestMethod]
        public async Task EditRoom_WithVaildRoomId_ReturnsUpdatedRoomObject()
        {
            var client = GetHttpClient();
            RoomDto roomDto = null;
            var roomToUpdate = MockRoomToDb();
            roomToUpdate.Name = "UpdatedRoomName";
            var response = await client.PutAsJsonAsync("api/rooms/" + roomToUpdate.Id, Mapper.Map<Room, RoomDto>(roomToUpdate));

            if (response.IsSuccessStatusCode)
            {
                roomDto = await response.Content.ReadAsAsync<RoomDto>();
            }

            Assert.IsNotNull(roomDto);
            Assert.AreEqual("UpdatedRoomName", roomDto.Name, "Room Name not updated");
            Assert.IsInstanceOfType(roomDto, typeof(RoomDto), "Failed to return RoomDto object");
        }

        [TestMethod]
        public async Task DeleteRoom_WhenCalledWithValidId_ReturnsDeletedObject()
        {
            var client = GetHttpClient();
            RoomDto roomDto = null;
            var roomToDelete = GetRoomToDelete().GetAwaiter().GetResult();
            var response = await client.DeleteAsync($"api/rooms/{roomToDelete.Id}");

            if (response.IsSuccessStatusCode)
            {
                roomDto = await response.Content.ReadAsAsync<RoomDto>();
            }

            Assert.IsNotNull(roomDto);
            Assert.AreEqual(roomToDelete.Id, roomDto.Id, "Room Id is valid");
            Assert.IsInstanceOfType(roomDto, typeof(RoomDto), "Object Deleted successfully");
        }

        [TestMethod]
        public async Task GetAvailableRooms_WhenCalled_ReturnsListOfAvailableRooms()
        {
            var client = GetHttpClient();
            var startDate = DateTime.Today.AddDays(3);
            var endDate = DateTime.Today.AddDays(10);
            var response = await client.GetAsync($"api/rooms?{startDate}&{endDate}");
            IEnumerable<RoomDto> rooms = null;

            if (response.IsSuccessStatusCode)
            {
                rooms = await response.Content.ReadAsAsync<IEnumerable<RoomDto>>();
            }

            Assert.IsInstanceOfType(rooms, typeof(IEnumerable<RoomDto>), "Rooms list request failed.");
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Room MockRoom()
        {
            return new Room
            {
                Name = "TestRoom",
                RoomTypeId = 4,
                IsAvailable = true
            };
        }

        private Room MockRoomToDb()
        {
            var Context = new HotelsContext();

            var testRoom = new Room
            {
                Name = "TestRoom",
                RoomTypeId = 4,
                IsAvailable = true
            };

            var savedRoom = Context.Rooms.Add(testRoom);
            Context.SaveChanges();


            return savedRoom;
        }

        private async Task<RoomDto> GetRoomToDelete()
        {
            var client = GetHttpClient();
            var roomToCreate = MockRoom();
            var response = await client.PostAsJsonAsync("api/rooms", Mapper.Map<Room, RoomDto>(roomToCreate));

            if (!response.IsSuccessStatusCode)
                return null;

            var roomDto = await response.Content.ReadAsAsync<RoomDto>();
            return roomDto;
        }
    }
}
