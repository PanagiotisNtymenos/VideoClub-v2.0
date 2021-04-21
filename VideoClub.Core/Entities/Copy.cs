using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Entities
{
    public class Copy
    {
        public Copy(Movie movie)
        {
            Movie = movie;
            IsAvailable = true;
        }

        public Copy() { }

        public int Id { get; set; }

        public virtual Movie Movie { get; set; }

        public bool IsAvailable { get; set; }

        public virtual List<Renting> Rentings { get; set; }

    }
}
