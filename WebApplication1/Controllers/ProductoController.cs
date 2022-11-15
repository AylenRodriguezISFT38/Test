using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public TestContext db;
        public ProductoController(TestContext _db) { 
            db= _db;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = db.Producto.Where(x=>x.Deleted == false).ToList();
                
                if (products.Count() != 0)
                {
                    return Ok(products);
                }

                return Ok(new { Err = false, msg = "No hay productos!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en GetProducts/ProductoController " + e.Message);
                return Ok(new {Err = true, msg = "Error al traer los datos! "+e.Message });
            }
        }
        [HttpPost]
        public IActionResult GetProductsFilter(Producto model)
        {
            try
            {
                var bsq = db.Producto.Where(x => x.IdTipoProducto == model.IdTipoProducto).ToList();

                if (bsq.Count() != 0)
                {
                    return Ok(bsq);
                }
                else
                {
                    return Ok(new {Err = false, msg = "No se encuentra ningun producto de ese tipo!"});
                }
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en GetProductsFilter/ProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al traer los datos! " + e.Message });
            }
        }
        [HttpPost]
        public IActionResult PostProducts(Producto model)
        {
            try
            {
                Producto producto = new Producto();
                producto = model;
                producto.Created_At= DateTime.Now;
                producto.Deleted= false;
                db.Producto.Add(producto);
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Producto creado correctamente!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en PostProducts/ProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al crear el producto! " + e.Message });
            }
        }
        [HttpPut]
        public IActionResult PutProducts(Producto model)
        {
            try
            {
                var bsq = db.Producto.Find(model.Id);
                bsq.Nombre = model.Nombre;
                bsq.Precio = model.Precio;
                bsq.IdTipoProducto = model.IdTipoProducto;

                db.SaveChanges();
                return Ok(new {Err=false, msg = "Producto editado correctamente!"});
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en PutProducts/ProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al editar los datos! " + e.Message });
            }
        }
        [HttpDelete] 
        public IActionResult DeleteProducts(Producto model)
        {
            try
            {
                var bsq = db.Producto.Find(model.Id);
                bsq.Deleted = true;
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Producto eliminado correctamente!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en DeleteProducts/ProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al eliminar el dato! " + e.Message });
            }
        }
    }
}
