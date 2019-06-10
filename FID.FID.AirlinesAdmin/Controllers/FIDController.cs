using FID.AirlinesAdmin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace FID.AirlinesAdmin.Controllers
{
    public class FIDController : Controller
    {
        // GET: Flights  
        public ActionResult GetAllFlights()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/v1/fid/FlightDetail");
                response.EnsureSuccessStatusCode();
                List<Models.FlightDetail> flightDetail = response.Content.ReadAsAsync<List<Models.FlightDetail>>().Result;
                ViewBag.Title = "All Flight Detail";
                return View(flightDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet]  
        public ActionResult EditFlightDetail(string flightNumber)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/v1/fid/FlightDetail/" + flightNumber);
            response.EnsureSuccessStatusCode();
            Models.FlightDetail flightDetail = response.Content.ReadAsAsync<Models.FlightDetail>().Result;
            ViewBag.Title = "Edit Flight";
            return View(flightDetail);
        }

        //[HttpPost]  
        public ActionResult Update(Models.FlightDetail flightDetail)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/v1/fid/FlightDetail/" + flightDetail.FlightNumber.ToString(),flightDetail);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllFlights");
        }
       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.FlightDetail flightDetail)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PostResponse("api/v1/FlightDetail/", flightDetail);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllFlights");
        }
        public ActionResult Delete(string flightNumber)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("api/v1/FlightDetail/" + flightNumber);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllFlights");
        }
    }
}