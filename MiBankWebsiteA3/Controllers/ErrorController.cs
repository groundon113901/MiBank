using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MiBankWebsiteA3.Controllers
{
    public class ErrorController : Controller
    {
        //This was done using the following website as a guide
        //https://andrewlock.net/re-execute-the-middleware-pipeline-with-the-statuscodepages-middleware-to-create-custom-error-pages/
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404 || statusCode == 500)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
