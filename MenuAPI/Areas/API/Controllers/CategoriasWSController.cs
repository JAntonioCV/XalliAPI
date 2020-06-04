using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MenuAPI.Areas.API.Models; 

namespace MenuAPI.Areas.API.Controllers
{
    public class CategoriasWSController : Controller
    {
        private MenuXallyContainer db = new MenuXallyContainer();

        [HttpGet]
        public JsonResult Categorias()
        {
            var categorias = (from c in db.TiposDePlatillo
                              select new CategoriaWS
                              {
                                  id = c.Id,
                                  codigo = c.CodigoTipoPlatillo,
                                  descripcion=c.DescripcionTipoPlatillo,
                                  estado=c.EstadoTipoPlatillo

                              }).ToList();

            return Json(categorias, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCategoria(CategoriaWS categoriaWS)
        {
            CategoriaMenu categoria = db.TiposDePlatillo.DefaultIfEmpty(null).FirstOrDefault(x => x.CodigoTipoPlatillo == categoriaWS.codigo);
            ResultadoWS resultadoWS=new ResultadoWS();

            if (categoria == null)
            {
                categoria = new CategoriaMenu();
                categoria.CodigoTipoPlatillo = categoriaWS.codigo;
                categoria.DescripcionTipoPlatillo = categoriaWS.descripcion;
                categoria.EstadoTipoPlatillo = categoriaWS.estado;

                db.TiposDePlatillo.Add(categoria);

                int safe = db.SaveChanges();

                if (safe > 0)
                {
                    resultadoWS.Mensaje = "Agregado correctamente";
                    resultadoWS.Resultado = true;
                    return Json(resultadoWS);
                }
                else
                {
                    resultadoWS.Mensaje = "Error al guardar";
                    resultadoWS.Resultado = false;

                    return Json(resultadoWS);
                }
            }

            resultadoWS.Mensaje = "Ya existe ese Codigo";
            resultadoWS.Resultado = false;

            return Json(resultadoWS);
           
        }



        [HttpGet]
        public JsonResult Categoria(int id)
        {
            var categoria = (from c in db.TiposDePlatillo
                              where c.Id == id
                              select new CategoriaWS
                              {
                                  id = c.Id,
                                  codigo = c.CodigoTipoPlatillo,
                                  descripcion = c.DescripcionTipoPlatillo,
                                  estado = c.EstadoTipoPlatillo

                              }).ToList();

            return Json(categoria, JsonRequestBehavior.AllowGet);
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
