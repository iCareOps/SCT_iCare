using SCT_iCare.Filters;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Contabilidad
{
    public class ContabilidadController : Controller
    {
        // GET: Contabilidad
        //[AuthorizeUser(idOperacion:4)]
        public ActionResult Index()
        {
            
            Execute().Wait();
            return View();
        }

        static async Task Execute()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey); 

            

            var from = new EmailAddress("no-reply@grupogamx.mx", "Grupo GA");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("amunoz@devware.mx", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<h1><strong>and easy to do anywhere, even with C#</strong></h1>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
            //Console.(response.StatusCode);
            //Console.ReadLine();
        }
    }
}