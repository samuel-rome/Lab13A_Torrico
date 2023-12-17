using Lab13A.Models;
using Lab13A.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13A.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerCustomController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public CustomerCustomController(InvoiceContext context)
        {
            _context = context;
        }

        //Insertar un Customer con un Modelo Request(FirstName, LastName, DocumentNumber)
        [HttpPost]
        public void Insert(CustomerRequestV1 request)
        {
            Customer cust = new Customer();
            cust.FirstName = request.FirstName;
            cust.LastName = request.LastName;
            cust.DocumentNumber = request.DocumentNumber;
            cust.Active = true;

            _context.Customers.Add(cust);
            _context.SaveChanges();
        }

        //Eliminar un Customer ocn un Modelo Request(id)
        [HttpPost]
        public void Delete(CustomerRequestV2 request)
        {
            Customer cus = _context.Customers.FirstOrDefault(c => c.CustomerID == request.CustomerID);
            if (cus != null)
            {
                //Mostramos el Customer como Inactivo
                cus.Active = false;
                _context.SaveChanges();
            }
        }

        //Actualizar el numeroDocumento al cliente
        [HttpPost]
        public void UpdateNumberDocument(CustomerRequestV3 request)
        {
            //Buscando el Id para actualizar
            var model = _context.Customers.Find(request.CustomerID);
            //AListando para modificar
            _context.Entry(model).State = EntityState.Modified;
            //Actualizar el documentNumber
            model.DocumentNumber = request.DocumentNumber;

            _context.SaveChanges();
        }
    }
}
