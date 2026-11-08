using System.Collections.Generic;

namespace Test.Models
{
    public class ProductListViewModel
    {
        public List<Product> Items { get; set; } = new List<Product>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
