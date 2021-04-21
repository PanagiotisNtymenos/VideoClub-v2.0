using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Rentings.Models
{
    public class RentingViewModel
    {

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime RentingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime ScheduledReturnDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; }

        public virtual Copy Copy { get; set; }

        public string RentingNotes { get; set; }

        public string ReturnNotes { get; set; }
    }
}