using System;
using System.Collections.Generic;

namespace EFProject
{
    public partial class Products //partial is so you can make another partial class and when you regenerate it, the files will combine
    {
        public Products()
        {
            LineItems = new HashSet<LineItems>();
        }

        public int Id { get; set; }
        public int VendorsId { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }

        public virtual Vendors Vendors { get; set; }
        public virtual ICollection<LineItems> LineItems { get; set; }
    }
}
