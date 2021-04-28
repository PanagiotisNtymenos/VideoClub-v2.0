using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoClub.Core.Enumerations;

namespace VideoClub.Web.Areas.Movies.Models
{
    public class MovieBindModel
    {
        [Required(ErrorMessage = "Συπληρώστε τον Τίτλο.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Συπληρώστε την Περιγραφή.")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Συπληρώστε τα Είδη.")]
        public List<Genres> Genres { get; set; }

        [Required(ErrorMessage = "Συπληρώστε τις Κόπιες.")]
        [Range(1, int.MaxValue, ErrorMessage = "Δημιουργήστε τουλάχιστον μια Κόπια.")]
        public int CopiesNumber { get; set; }

    }
}