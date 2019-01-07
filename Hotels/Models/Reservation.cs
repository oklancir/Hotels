﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotels.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-YYYY}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-YYYY}")]
        public DateTime EndDate { get; set; }

        [ForeignKey("Guest")]
        [Display(Name = "Guest ")]
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }

        [ForeignKey("Room")]
        [Display(Name = "Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
    }
}