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
            var id = 12;
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
            var roomToCreate = new Room { Id = 14, Name = "TestingRoom", IsAvailable = true, RoomTypeId = 4 };
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
            var roomToDelete = GetRoomToDelete().GetAwaiter().GetResult();
            var response = await client.DeleteAsync($"api/rooms/{roomToDelete.Id}");

            Assert.IsNotNull(response.IsSuccessStatusCode, "Room not deleted successfully");
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
                Id = 14,
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
                Id = 14,
                Name = "TestRoom",
                RoomTypeId = 4,
                IsAvailable = true
            };

            if (Context.Rooms.Find(testRoom.Id) == null)
            {
                Context.Rooms.Add(testRoom);
                Context.SaveChanges();
            }

            return testRoom;
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
