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
        public Renting(string rentingNotes)
        {
            RentingDate = DateTime.Now;
            ScheduledReturnDate = DateTime.Now.AddDays(7);
            IsActive = true;
            RentingNotes = rentingNotes;
            ReturnNotes = null;
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
