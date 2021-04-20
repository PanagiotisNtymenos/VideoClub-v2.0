﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Entities
{
    public class MovieGenre
    {
        [Key]
        [Column(Order = 0)]
        public int MovieId { get; set; }

        [Key]
        [Column(Order = 1)]
        public virtual int Genre { get; set; }

        public virtual Movie Movie { get; set; }

    }
}
