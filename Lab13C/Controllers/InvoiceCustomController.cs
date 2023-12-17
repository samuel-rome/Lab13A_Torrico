
using Lab13A.Models;
using Lab13A.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab13A.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceCustomController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public InvoiceCustomController(InvoiceContext context)
        {
            _context = context;
        }

        //Insertar un Invoice con un Modelo Request(IdCustomer, Date,InvoiceNumber,Total)
        [HttpPost]
        public void Insertar(InvoiceRequestV1 request) 
        { 
            Invoice inv =  new Invoice();
            inv.CustomerID = request.CustomerID;
            inv.Date = request.Date;
            inv.InvoiceNumber = request.InvoiceNumber;
            inv.Total = request.Total;
            inv.Active = true;
            
            _context.Invoices.Add(inv);
            _context.SaveChanges();
        }

        //Insertar una lista de Factura 
        [HttpPost]
        public void InsertRange(List<InvoiceRequestV1> request)
        {
            //Convertir lista de Request a Lista modelo

            var modelos = request.Select(x => new Invoice
            {
                CustomerID = x.CustomerID,
                Date = x.Date,
                InvoiceNumber = x.InvoiceNumber,    
                Total = x.Total,
                Active = true
            }).ToList();

            _context.Invoices.AddRange(modelos);
            _context.SaveChanges();
        }
    }
}
