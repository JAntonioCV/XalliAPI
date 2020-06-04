using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuAPI.Areas.API.Models;


namespace MenuAPI.Areas.API.Controllers
{
    public class RecetasWSController : Controller
    {
        private MenuXallyContainer db = new MenuXallyContainer();

        [HttpGet]
        public JsonResult Recetas()
        {
            var recetas = (from r in db.Recetas
                              select new RecetaWS  
                              {
                                  id = r.Id,
                                  descripcion = r.DescripcionReceta,
                                  tiempoEstimado = r.TiempoEstimadoReceta,
                                  ingrediente = r.IngredientesPrincipales,
                                  estado = r.EstadoReceta
                               
                              }).ToList();

            return Json(recetas , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Receta(int id)
        {
            var receta = (from r in db.Recetas
                           where r.Id == id
                           select new RecetaWS
                           {
                               id = r.Id,
                               descripcion = r.DescripcionReceta,
                               tiempoEstimado = r.TiempoEstimadoReceta,
                               ingrediente = r.IngredientesPrincipales,
                               estado = r.EstadoReceta

                           }).ToList();

            return Json(receta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RecetaPorPlatillo(int id)
        {
            var receta = (from r in db.Recetas
                          where r.MenuId == id
                          select new RecetaWS
                          {
                              id = r.Id,
                              descripcion = r.DescripcionReceta,
                              tiempoEstimado = r.TiempoEstimadoReceta,
                              ingrediente = r.IngredientesPrincipales,
                              estado = r.EstadoReceta

                          }).ToList();

            return Json(receta, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}