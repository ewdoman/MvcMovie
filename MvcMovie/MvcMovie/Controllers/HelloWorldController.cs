using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebApplication.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld

        //Calls View (Controller methods aka action methods)
        //Routing currently has Index set to the default

        public ActionResult Index()
        {
            return View();
        }

        /* Passing data from controller to view using View.Bag      
        * http://localhost:xxxx/HelloWorld/Welcome?name=Eric&numTimes=7
        */

        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello, " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }

        /* 
        * 
        *  Url parameter test functions
        * 
        */

        // GET: /HelloWorld/textDisplay/

        //Passes a string to be displayed on web page (Technically there is NO seperate view)
        public string textDisplay()
        {
            return "This is my <b>default</b> action...";
        }

        // GET: /HelloWorld/PassParameterByQuery/

        /* Allows name and numTimes parameters to be set within URL
        * "Pass by query strings"
        * "http://localhost:xxxx/HelloWorld/ParameterExample?name=Scott&numtimes=4"
        */

        public string PassParameterByQuery(string name, int numTimes = 1)
        {
            return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);
        }

        // GET: /HelloWorld/PassParameterByRoute/

        /*Another Example of parameter passing with ID
        * "Pass by route data" 
        * specifically ID from our routing doc (url: "{controller}/{action}/{id}";)
        * http://localhost:xxx/HelloWorld/ParameterExampleID/3?name=Rick
        */

        public string PassParameterByRoute(string name, int ID = 1)
        {
            return HttpUtility.HtmlEncode("Hello " + name + ", ID: " + ID);
        }

    }
}