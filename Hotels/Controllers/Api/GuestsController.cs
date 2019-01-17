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
    public class GuestsController : ApiController
    {
        private readonly HotelsContext Context;

        public GuestsController()
        {
            Context = new HotelsContext();
        }

        [HttpGet]
        public IEnumerable<GuestDto> GetGuests()
        {
            return Context.Guests.ToList().Select(Mapper.Map<Guest, GuestDto>);
        }

        [HttpGet]
        public IHttpActionResult GetGuest(int id)
        {
            var guest = Context.Guests.SingleOrDefault(g => g.Id == id);

            if (guest == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Guest, GuestDto>(guest));
        }

        [HttpPost]
        public IHttpActionResult CreateGuest(GuestDto guestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var guest = Mapper.Map<GuestDto, Guest>(guestDto);
            Context.Guests.Add(guest);
            Context.SaveChanges();

            guestDto.Id = guest.Id;

            return Created(new Uri(Request.RequestUri + "/" + guest.Id), guestDto);
        }

        [HttpPut]
        public void UpdateGuest(int id, GuestDto guestDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var guestInDb = Context.Guests.SingleOrDefault(g => g.Id == id);

            if (guestInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(guestDto, guestInDb);

            Context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteGuest(int id)
        {
            var guestInDb = Context.Guests.SingleOrDefault(g => g.Id == id);

            if (guestInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Guests.Remove(guestInDb);
            Context.SaveChanges();
        }
    }
}
