using EFLib;
using System;
using System.Linq;
//THIS IS DATABASE FIRST
namespace EFProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //create new instance of prsContext class and store data in the instance _context
            var _context = new prsContext();

            var vendors = _context.Vendors.ToList();
            var IKEA = _context.Vendors.SingleOrDefault(v => v.Code == "IKEA");
            //pass the instance of _context into method RequestsController and store in instance ReqCtrl
            var RequCtrl = new RequestsController(_context);

            //check review status and return anything with review. brings up two rows in Request
            var requestInReview = RequCtrl.GetRequestsInReview();

            //instance ReqCtrl.method - calls Re method and passes in the parameter 1 which is the Id of 1 and store it in instance updTotal
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

            //retrieve all rows and columns from the Users database which is a collection of data and store it in users
            //the following line connects to the database, opens the database, reads data, retrieves data,
            //closes the reader and closes the database
            //var users = _context.Users.ToList();
            ////retrieve the row with a primary key of 1 using the Find()
            //var user1 = _context.Users.Find(1);
            ////update a Phone number is user1 record(row). need to save changes when finished. open SQl Users table to see if phone number changed
            //user1.PhoneNumber = "513-555-1212";
            //_context.SaveChanges();
            ////example for Vendor
            //var vendors = _context.Vendor.ToList();
            //var bbuy = _context.Vendor.SingleOrDefault(v => v.Code == "BSTBY");

            var users = _context.Users.ToList();

            //find a user and change the information
            var users1 = _context.Users.Find(7);
            users1.PhoneNumber = "513-555-7777";
            _context.SaveChanges();
        }
    }
}
