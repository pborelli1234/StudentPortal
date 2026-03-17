using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            // Log the error
            // return custom response
            //return Problem(
            //    detail: exception?.Message,
            //    statusCode: 500,
            //    title: "An unexpected error occurred."
            //);

            return View("Index");
        }
    }
}
