using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using Itenso.TimePeriod;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Hotels.Controllers.Api
{
    public class ReservationsController : ApiController
    {
        private readonly HotelsContext Context;

        public ReservationsController()
        {
            Context = new HotelsContext();
        }

        [HttpGet]
        public IEnumerable<ReservationDto> GetReservations()
        {
            return Context.Reservations.ToList().Select(Mapper.Map<Reservation, ReservationDto>);
        }

        [HttpGet]
        public IHttpActionResult GetReservation(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(g => g.Id == id);

            if (reservation == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Reservation, ReservationDto>(reservation));
        }

        [HttpPost]
        public IHttpActionResult CreateReservation(ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The data you have entered is not valid.");
            }

            var rangeFromSelect = new TimeRange(reservationDto.StartDate, reservationDto.EndDate);

            var reservations = Context.Reservations.ToList();

            var unavailableRoomsId = reservations
                .Where(r => rangeFromSelect.IntersectsWith(new TimeRange(r.StartDate, r.EndDate, true)))
                .Select(r => r.RoomId).ToList();

            if (Context.Rooms.Any(ar => !unavailableRoomsId.Contains(ar.Id)))
            {
                Context.Reservations.Add(Mapper.Map<ReservationDto, Reservation>(reservationDto));
                Context.SaveChanges();
                return Ok("Reservation added successfully.");
            }
            return BadRequest("The room is already reserved for that period");
        }

        [HttpDelete]
        public void DeleteReservation(int id)
        {
            var reservationInDb = Context.Reservations.SingleOrDefault(g => g.Id == id);

            if (reservationInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Reservations.Remove(reservationInDb);
            Context.SaveChanges();
        }
    }
}
