using Microsoft.AspNetCore.Mvc;

namespace SampleSite.Controllers
{
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        // GET: /Home/
        public string Index()
        {
            return "This is my default action...";
        }

        // GET: /Home/Welcome/ 
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
