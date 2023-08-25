using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace SampleSite.Controllers;

public class HelloWorldController : Controller
{
    // 
    // GET: /HelloWorld/
    // Calls the controller's View method.
    // Uses a view template to generate an HTML response.
    // Use with: 
    // The Index method in the HelloWorldController ran the statement return
    // View();, which specified that the method should use a view template file
    // to render a response to the browser.
    // A view template file name wasn't specified, so MVC defaulted to using
    // the default view file. When the view file name isn't specified, the
    // default view is returned. The default view has the same name as the
    // action method, Index in this example. The view template
    // /Views/HelloWorld/Index.cshtml is used.
    public IActionResult Index()
    {
        return View();
    }

    // GET: /HelloWorld/Welcome/ 
    // Requires using System.Text.Encodings.Web;
    //public string Welcome(string name, int numTimes = 1)
    //{
    //    // String interpolation using $
    //    /**
    //    The $ special character identifies a string literal as an interpolated
    //    string. An interpolated string is a string literal that might contain
    //    interpolation expressions. When an interpolated string is resolved to a result
    //    string, items with interpolation expressions are replaced by the string
    //    representations of the expression results.

    //    Use with: https://localhost:7104/helloworld/welcome?name=Rick&numtimes=4
    //    The MVC model binding system automatically maps the named parameters
    //    from the query string to parameters in the method.
    //     */
    //    return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
    //}
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        // The ViewData dictionary object contains data that will be passed to the view.
        // Use with: https://localhost:{PORT}/HelloWorld/Welcome?name=Rick&numtimes=4
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }

    // GET: /HelloWorld/WelcomeId/ 
    // Use with: ttps://localhost:{PORT}/HelloWorld/Welcome/3?name=Rick
    public string WelcomeId(string name, int ID = 1)
    {
        return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
    }
}
