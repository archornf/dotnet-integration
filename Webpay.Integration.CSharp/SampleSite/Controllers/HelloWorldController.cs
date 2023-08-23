using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace SampleSite.Controllers;

public class HelloWorldController : Controller
{
    // 
    // GET: /HelloWorld/
    public string Index()
    {
        return "HelloWorld";
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public string Welcome()
    {
        return "HelloWorld/Welcome";
    }
}
