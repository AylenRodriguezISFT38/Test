using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        public TestContext db;
        public TipoProductoController(TestContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult GetTypes()
        {
            try
            {
                var types = db.TipoProducto.Where(x => x.Deleted == false).ToList();

                if (types.Count() != 0)
                {
                    return Ok(types);
                }

                return Ok(new { Err = false, msg = "No hay tipos de productos registrados!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en GetTypes/TipoProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al traer los datos! " + e.Message });
            }
        }
        [HttpPost]
        public IActionResult PostTypes(TipoProducto model)
        {
            try
            {
                TipoProducto tipoProducto = new TipoProducto();
                tipoProducto = model;
                tipoProducto.Created_At = DateTime.Now;
                tipoProducto.Deleted = false;
                db.TipoProducto.Add(tipoProducto);
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Tipo de producto creado correctamente!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en PostTypes/TipoProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al crear el tipo de producto! " + e.Message });
            }
        }
        [HttpPut]
        public IActionResult PutTypes(TipoProducto model)
        {
            try
            {
                var bsq = db.TipoProducto.Find(model.Id);
                bsq.Descripcion = model.Descripcion;
                db.SaveChanges();
                return Ok(bsq);
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en PutTypes/TipoProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al editar los datos! " + e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteTypes(TipoProducto model)
        {
            try
            {
                var bsq = db.TipoProducto.Find(model.Id);
                bsq.Deleted = true;
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Tipo de producto eliminado correctamente!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en DeleteTypes/TipoProductoController " + e.Message);
                return Ok(new { Err = true, msg = "Error al eliminar el dato! " + e.Message });
            }
        }
    }
}
