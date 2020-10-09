using EFLib;
using System;
using System.Linq;

namespace EFProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var _context = new prsContext();

            var vendors = _context.Vendors.ToList();
            var IKEA = _context.Vendors.SingleOrDefault(v => v.Code == "IKEA");

            var RequCtrl = new RequestsController(_context);

            //see all the stasus' in review
            var requestInReview = RequCtrl.GetRequestsInReview();

            //check if total worked
            var updTotal = RequCtrl.RecalculateRequestTotal(1);

            //check to see if it gets approved and rejected
            var req2 = _context.Requests.Find(2);
            var worked = RequCtrl.SetToApproved(req2);
            var reject = RequCtrl.SetToRejected(req2);

            //to check and see if the request is over 50.00 gets reviewed
            var req3 = _context.Requests.Find(3);
            var req4 = _context.Requests.Find(4);
            RequCtrl.ReviewRequest(req3);
            RequCtrl.ReviewRequest(req4);

            var UserCtrl = new UsersController(_context);

            //test login function
            var pheebs = UserCtrl.Login("PBuffay", "Smellycat");
            var who = UserCtrl.Login("xx", "yy");//should be null

            var users = _context.Users.ToList();

            //find a user and change the information
            var users1 = _context.Users.Find(7);
            users1.PhoneNumber = "513-555-7777";
            _context.SaveChanges();
        }
    }
}
