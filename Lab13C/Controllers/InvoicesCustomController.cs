using Lab13A.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab13A.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicesCustomController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public InvoicesCustomController(InvoiceContext context)
        {
            _context = context;
        }

        [HttpGet]
       public List<Invoice> GetInvoices() {
            var response = _context.Invoices.ToList();

            return response;
        }

        
    }
}
