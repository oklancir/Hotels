using Hotels.Models;
using NLog;
using System.Linq;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly HotelsContext Context = new HotelsContext();
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        // GET: Invoice
        public ActionResult InvoiceList()
        {
            var invoices = Context.Invoices.ToList();
            return View(invoices);
        }
    }
}