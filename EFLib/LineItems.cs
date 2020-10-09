using System;
using System.Collections.Generic;

namespace EFProject
{
    public partial class LineItems
    {
        public int Id { get; set; }
        public int RequestsId { get; set; }
        public int ProductsId { get; set; }
        public int Quantity { get; set; }

        public virtual Products Products { get; set; }
        public virtual Requests Requests { get; set; }
    }
}
