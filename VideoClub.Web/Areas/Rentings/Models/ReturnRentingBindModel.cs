using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Rentings.Models
{
    public class ReturnRentingBindModel
    {
        public ReturnRentingBindModel() { }

        [Required]
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Copy Copy { get; set; }

        public DateTime RentingDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string RentingNotes { get; set; }

        public string ReturnNotes { get; set; }
    }
}