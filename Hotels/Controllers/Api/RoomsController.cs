using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Hotels.Controllers.Api
{
    public class RoomsController : ApiController
    {
        private readonly HotelsContext Context;

        public RoomsController()
        {
            Context = new HotelsContext();
        }

        [HttpGet]
        public IEnumerable<RoomDto> GetRooms()
        {
            return Context.Rooms.ToList().Select(Mapper.Map<Room, RoomDto>);
        }

        [HttpGet]
        public IHttpActionResult GetRoom(int id)
        {
            var room = Context.Rooms.SingleOrDefault(r => r.Id == id);

            if (room == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Room, RoomDto>(room));
        }

        [HttpPost]
        public IHttpActionResult CreateRoom(RoomDto roomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var room = Mapper.Map<RoomDto, Room>(roomDto);
            Context.Rooms.Add(room);
            Context.SaveChanges();

            roomDto.Id = room.Id;

            return Created(new Uri(Request.RequestUri + "/" + room.Id), roomDto);
        }

        [HttpPut]
        public void EditRoom(int id, RoomDto roomDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var roomInDb = Context.Rooms.SingleOrDefault(r => r.Id == id);

            if (roomInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(roomDto, roomInDb);

            Context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteRoom(int id)
        {
            var roomInDb = Context.Rooms.SingleOrDefault(r => r.Id == id);

            if (roomInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Rooms.Remove(roomInDb);
            Context.SaveChanges();
        }
    }
}
