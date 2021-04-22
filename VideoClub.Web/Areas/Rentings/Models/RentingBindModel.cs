using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Rentings.Models
{
    public class RentingBindModel
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public int? MovieId { get; set; }

        [Required]
        public string Username { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime RentingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime ScheduledReturnDate { get; set; }

        public string RentingNotes { get; set; }

        public string ReturnNotes { get; set; }

        public RentingBindModel(string username,string title, int? movieId)
        {
            this.Username = username;
            this.Title = title;
            this.MovieId = movieId;
        }

        public RentingBindModel() { }
    }
}