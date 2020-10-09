using System;
using System.Collections.Generic;

namespace EFProject
{
    public partial class Requests
    {
        public Requests()
        {
            LineItems = new HashSet<LineItems>();
        }

        public int Id { get; set; }
        public int UsersId { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public DateTime DateNeeded { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string ReasonForRejection { get; set; }

        public virtual Users Users { get; set; }
        public virtual ICollection<LineItems> LineItems { get; set; }
    }
}
