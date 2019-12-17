using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.MVCUI.Models;
using JLL.DVDCentral.MVCUI.ViewModels;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class CustomerController : Controller
    {
        List<Customer> customers;
        // GET: Customer
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                customers = CustomerManager.Load();
                return View(customers);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Customer/Details/
        public ActionResult Details(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Customer customer = CustomerManager.LoadById(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            CustomerOrders CO = new CustomerOrders();
           
                return View(CO);
           

        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerOrders CO)
        {
            
            try
            {

                // TODO: Add insert logic here
              
                UserManager.Insert(CO.CU_UserId, CO.CU_FirstName, CO.CU_LastName, CO.User.PassCode);
                CustomerManager.Insert(CO.CU_FirstName, CO.CU_LastName, CO.Customer.Address, CO.Customer.City, CO.Customer.State, CO.Customer.ZIP, CO.Customer.Phone, CO.CU_UserId);
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //   return View();
                throw ex;
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Customer customer = CustomerManager.LoadById(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                CustomerManager.Update(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Customer customer = CustomerManager.LoadById(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                // TODO: Add delete logic here
                CustomerManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
