﻿using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiClientSample.Controllers
{
    public class HomeController : Controller
    {
        public class UserIdDto
        {
            public string NtId { get; set; }
        }
        public ActionResult Index()
        {
            #region GetOnlineCustomersCount example
            //var client = new RestClient("http://localhost:8085");
            //var request = new RestRequest("api/services/app/queueSystemService/GetOnlineCustomersCount", Method.POST);


            //// execute the request
            //IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            //var jsonObject = JObject.Parse(content);
            //var success = Convert.ToBoolean(jsonObject["success"].ToString());
            //var onlineCustomersCount = Convert.ToInt32(jsonObject["result"].ToString());

            //var allowedMaxOnlineCustomerCount = Convert.ToInt32(ConfigurationManager.AppSettings["AllowedMaxOnlineCustomerCount"]);
            //if (success)
            //{
            //    if (onlineCustomersCount > allowedMaxOnlineCustomerCount)
            //    {
            //        Response.Redirect(string.Format(@"http://localhost:8085/queue?eid={0}","admin"));
            //    }
            //} 
            #endregion

            #region CanAccessSystem example
            var client = new RestClient("http://localhost:8085");
            var request = new RestRequest("api/services/app/queueSystemService/CanAccessSystem", Method.POST);

            request.AddJsonBody(new UserIdDto { NtId = "Admin" });
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            var jsonObject = JObject.Parse(content);
            var canAccessSystem = Convert.ToBoolean(jsonObject["result"].ToString());

            if (!canAccessSystem)
            {
                Response.Redirect(string.Format(@"http://localhost:8085/queue?eid={0}", "admin"));
            } 
            #endregion

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}