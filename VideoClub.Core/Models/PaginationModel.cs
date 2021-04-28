using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Models
{
    public class PaginationModel<T>
    {
        public List<T> Items { get; set; }

        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PaginationModel()
        {
            Items = new List<T>();
        }

        public PaginationModel(List<T> items)
        {
            Items = items;
        }

        public PaginationModel(List<T> items, int pageNum, int pageSize, int totalItems)
        {
            PageNum = pageNum;
            PageSize = pageSize;
            TotalItems = totalItems;
            Items = items;
            TotalPages = (int)Math.Ceiling((double)TotalItems / (double)pageSize);
        }
    }
}
