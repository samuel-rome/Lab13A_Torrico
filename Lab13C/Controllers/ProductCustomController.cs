using Lab13A.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab13A.Models.Request;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Lab13A.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductCustomController : ControllerBase
    {
        private readonly InvoiceContext _context;
        public ProductCustomController(InvoiceContext context)
        {
            _context = context;
        }

        //Insertar un Producto con un Modelo Request(Name, Price)
        [HttpPost]
        public void Insert(ProductRequestV1 request)
        {
            Product product = new Product();
            product.Price = request.Price;
            product.Name = request.Name;
            product.Active = true;

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        //Eliminar de manera Logica con un Modelo Request(ProductID)
        [HttpPost]
        public void Delete(ProductRequestV2 request)
        {
            try
            {
                //Buscar Produto por ID
                Product product = _context.Products.FirstOrDefault(p => p.ProductID == request.ProductID);

                if(product != null)
                {
                    //Mostramos el Producto como inactivo 
                    product.Active = false;

                    _context.SaveChanges();
                }
                
            }catch (Exception ex)
            {

            }
        }
        //Actualizar el precio del Producto
        [HttpPost]
        public void UpdatePrice(ProductRequestV3 request)
        {
            //Busca el Objeto
            var model = _context.Products.Find(request.ProductID);
            //Lo preparapara modificar
            _context.Entry(model).State = EntityState.Modified;
           //Asigna valoer nuevo
            model.Price=request.Price;
            //Confirmacion 
            _context.SaveChanges();
        }

        //Insertar una lista de Productos
        [HttpPost]
        public void InsertRange(List< ProductRequestV1 > request)
        {
            //Convertir lista de Request a Lista modelo

            var modelos = request.Select(x => new Product {
                Price = x.Price,
                Name = x.Name,
                Active = true
            }).ToList();


            _context.Products.AddRange(modelos);
            _context.SaveChanges();
        }
    }
}
