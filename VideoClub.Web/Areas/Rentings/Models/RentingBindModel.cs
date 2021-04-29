using System;
using System.ComponentModel.DataAnnotations;

namespace VideoClub.Web.Areas.Rentings.Models
{
    public class RentingBindModel
    {

        [Required(ErrorMessage ="Συμπληρώστε τον Τίτλο.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Συμπληρώστε το ID της ταινίας.")]
        public int? MovieId { get; set; }

        [Required(ErrorMessage = "Συμπληρώστε τον Πελάτη.")]
        public string Username { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime RentingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/MM/yyyy}")]
        public DateTime ScheduledReturnDate { get; set; }

        public string RentNotes { get; set; }

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