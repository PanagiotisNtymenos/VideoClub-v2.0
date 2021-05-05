using System;
using System.ComponentModel.DataAnnotations;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Rentings.Models
{
    public class RentingViewModel
    {
        public int Id { get; set; }

        public string Poster { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime ScheduledReturnDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; }

        public virtual Copy Copy { get; set; }

        public string RentingNotes { get; set; }

        public string ReturnNotes { get; set; }

    }
}