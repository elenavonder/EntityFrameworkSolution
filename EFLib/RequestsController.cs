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
       //readonly means you can only set the value of the property by a constructor and no other mehtods can change it
        private readonly prsContext _context;
        //constructor
        public RequestsController(prsContext context)
        {
            _context = context;
        }

        public List<Requests> GetRequestsInReview()
        {
            //return prsContext Request table data in _context where Status equals review and put it in a generic collectin list
            //tolist() give generic list of REVIEW
            return _context.Requests.Where(r => r.Status == "REVIEW").ToList();
        }

        public bool RecalculateRequestTotal(int Id)
        {
            //_context.Reuests means _context table.Request from the _context table
            //when we are passing a class instance we are passing it as a reference type
            //hover over Find
            var request = _context.Requests.Find(Id);
            //need to calculate a total from two tables LineItem and Product, so join the tables first using query syntax
            //ToList is a DBSEt in EF which turns DBSEt into a generic list ToList or we could have said ToArray
            var reqTotal = (from li in _context.LineItems.ToList()
                            join pr in _context.Products.ToList()
                            on li.ProductsId equals pr.Id
                            where li.RequestsId == Id
                            //select new because we are making a new column LineTotal
                            select new
                            {//LineTotal is a new column under select new
                                LineTotal = li.Quantity * pr.Price
                                //have to put lambda functin in here
                            }).Sum(t => t.LineTotal);
            //need to update the request column with the new Total
            request.Total = reqTotal;
            _context.SaveChanges();
            return true;//need return to let the user know it worked
        }
        //would probably make a private method for settorejected and settoapproved since they have duplicated code
        //method to allow automatic approved or review of request. if approved returns approved. if review the other two methods
        //are called that allows a reviewer to approve or reject
        /// <summary>
        /// Reviews the request for the owner(user)
        /// status is set to APPROVED if Total <= 50
        /// else status is set to REVIEW
        /// </summary>
        /// <param name="request">A request</param>
        /// <returns>true is successful; else false</returns>
        public bool ReviewRequest(Requests request)
        {//passing in an instance of request which was different properties, one of which is Status, or Id, or Desc
            //request only recieves a copy of whatever was passed into it, request itself didn't change
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
            _context.SaveChanges();//puts data in database
            return true;
        }  
    }
}
