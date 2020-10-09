using System;
using System.Collections.Generic;

namespace EFProject
{
    public partial class Users
    {
        public Users()
        {
            Requests = new HashSet<Requests>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }
    }
}
