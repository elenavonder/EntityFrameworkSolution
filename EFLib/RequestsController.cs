using EFProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EFLib
{
    public class RequestsController
    {
        private readonly prsContext _context;//readonly means you can only set the value in a contrsuctor

        public RequestsController(prsContext context)
        {
            _context = context;
        }

        public List<Requests> GetRequestsInReview()
        {
            return _context.Requests.Where(r => r.Status == "REVIEW").ToList();//tolist() give generic list of REVIEW
        }

        public bool RecalculateRequestTotal (int Id)
        {
            var request = _context.Requests.Find(Id); //read the data
            var reqtotal = (from l in _context.LineItems.ToList()//getting LineItems
                           join p in _context.Products.ToList()//Getting Products
                           on l.ProductsId equals p.Id//joining the two tables
                           where l.RequestsId == Id //collecting lineitem from requestid for request
                           select new
                           {
                               LineTotal = l.Quantity * p.Price//get quantity and price and multiply together
                           }).Sum(t => t.LineTotal);//get sum of all lineitems for grand total
            request.Total = reqtotal;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Reviews the request for the owner(user)
        /// status is set to APPROVED if Total <= 50
        /// else status is set to REVIEW
        /// </summary>
        /// <param name="request">A request</param>
        /// <returns>true is successful; else false</returns>
        public bool ReviewRequest(Requests request)
        {
            //Ternary operator doing same thing as if and else
            //request.Status = (request.Total <= 50) ? "APPROVED" : "REVIEWED";
           if (request.Total <= 50)
            {
                request.Status = "APPROVED";
            }
            else
            {
                request.Status = "REVIEWED";
            }
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Sets the status of request to APPROVED
        /// </summary>
        /// <param name="request">a single request</param>
        /// <returns>true if successful; else false</returns>
        public bool SetToApproved(Requests request)
        {
            request.Status = "APPROVED";//changes the status in C#
            _context.SaveChanges();//its just floating in here until this statement then its saved into the database
            return true;//return is for us to see it on our end to see if it worked
        }
        /// <summary>
        /// Sets the status of request to REJECTED
        /// </summary>
        /// <param name="request">a single request</param>
        /// <returns>true if successful; else false</returns>
        public bool SetToRejected (Requests request)
        {
            request.Status = "REJECTED";
            _context.SaveChanges();
            return true;
        }  
    }
}
