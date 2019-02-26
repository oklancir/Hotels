using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Hotels.Controllers.Api
{
    public class RoomsController : ApiController
    {
        private readonly IHotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public RoomsController()
        {
            Context = new HotelsContext();
        }

        public RoomsController(IHotelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IEnumerable<RoomDto> GetRooms()
        {
            return Context.Rooms.ToList().Select(Mapper.Map<Room, RoomDto>);
        }

        [HttpGet]
        public IEnumerable<RoomDto> GetRooms(DateTime startDate, DateTime endDate)
        {
            var helper = new ReservationHelper(Context);
            return helper.GetAvailableRooms(startDate, endDate).ToList().Select(Mapper.Map<Room, RoomDto>);
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

            try
            {
                Context.SaveChanges();
                roomDto.Id = room.Id;
                return Created(new Uri(Request.RequestUri + "/" + room.Id), roomDto);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpPut]
        public IHttpActionResult EditRoom(int id, [FromBody] RoomDto roomDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var roomInDb = Context.Rooms.SingleOrDefault(r => r.Id == id);

            if (roomInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(roomDto, roomInDb);

            try
            {
                Context.SaveChanges();
                return Ok(roomInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteRoom(int id)
        {
            var roomInDb = Context.Rooms.SingleOrDefault(r => r.Id == id);

            if (roomInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Rooms.Remove(roomInDb);

            try
            {
                Context.SaveChanges();
                return Ok(roomInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }
    }
}
