using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public TestContext db;
        public StockController(TestContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult GetStock()
        {
            try
            {
                var stock = db.Stock.Where(x => x.Deleted == false).ToList();

                if (stock.Count() != 0)
                {
                    return Ok(stock);
                }

                return Ok(new { Err = false, msg = "No hay stock para ningun producto!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en GetStock/StockController " + e.Message);
                return Ok(new { Err = true, msg = "Error al traer los datos! " + e.Message });
            }
        }
        [HttpPost]
        public IActionResult PostStock(Stock model)
        {
            try
            {
                Stock stock = new Stock();
                stock = model;
                stock.Created_At = DateTime.Now;
                stock.Deleted = false;
                db.Stock.Add(stock);
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Stock creado correctamente!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en PostStock/StockController  " + e.Message);
                return Ok(new { Err = true, msg = "Error al crear el stock! " + e.Message });
            }
        }
        [HttpPut]
        public IActionResult PutStock(Stock model)
        {
            try
            {
                var bsq = db.Stock.Find(model.Id);
                bsq.Cantidad = model.Cantidad;
                bsq.IdProducto= model.IdProducto;

                db.SaveChanges();
                return Ok(bsq);
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en PutStock/StockController " + e.Message);
                return Ok(new { Err = true, msg = "Error al editar los datos! " + e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteStock(Stock model)
        {
            try
            {
                var bsq = db.Stock.Find(model.Id);
                bsq.Deleted = true;
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Stock eliminado correctamente!" });
            }
            catch (Exception e)
            {
                Services.LogService.Log("Error en DeleteStock/StockController " + e.Message);
                return Ok(new { Err = true, msg = "Error al eliminar el dato! " + e.Message });
            }
        }

    }
}
