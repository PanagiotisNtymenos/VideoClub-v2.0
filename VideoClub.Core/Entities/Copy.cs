using System.Collections.Generic;


namespace VideoClub.Core.Entities
{
    public class Copy : BaseEntity<int>
    {
        public Copy(Movie movie)
        {
            Movie = movie;
            IsAvailable = true;
        }

        public Copy() { }

        public virtual Movie Movie { get; set; }

        public bool IsAvailable { get; set; }

        public virtual List<Renting> Rentings { get; set; }

    }
}
