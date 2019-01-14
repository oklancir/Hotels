﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }

        [Required, DefaultValue("Pending")]
        public string Name { get; set; }
    }
}