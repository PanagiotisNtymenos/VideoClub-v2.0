using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Entities
{
    public class Renting
    {
        public Renting(DateTime rentingDate, DateTime scheduledReturnDate, DateTime returnDate, User user, Copy copy, bool isActive, string rentingNotes, string returnNotes)
        {
            RentingDate = rentingDate;
            ScheduledReturnDate = scheduledReturnDate;
            ReturnDate = returnDate;
            User = user;
            Copy = copy;
            IsActive = isActive;
            RentingNotes = rentingNotes;
            ReturnNotes = returnNotes;
        }

        public Renting(User user, Copy copy)
        {
            User = user;
            Copy = copy;
        }

        public Renting() { }

        public int Id { get; set; }

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
