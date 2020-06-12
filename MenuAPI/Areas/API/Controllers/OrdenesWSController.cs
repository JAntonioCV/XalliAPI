using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuAPI.Areas.API.Models;

namespace MenuAPI.Areas.API.Controllers
{
    public class OrdenesWSController : Controller
    {
        private MenuXallyContainer db = new MenuXallyContainer();


        // ordenes atendidas !! ordenes no atendidas
        [HttpGet]
        public JsonResult Ordenes(bool estado)
        {
            var ordenes = (from r in db.Ordenes
                           where r.EstadoOrden == estado
                           select new OrdenWS
                           {
                               id = r.Id,
                               fechaorden = r.FechaOrden,
                               tiempoorden = r.TiempoOrden,
                               estado = r.EstadoOrden

                           }).ToList();

            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UltimoCodigo()
        {
            int code = int.Parse(db.Ordenes.Max(x => x.CodigoOrden));

            string codigo;

            if (code < 10)
                codigo = "00" + (code + 1);
            else
                if (code >= 10 || code < 100)
                codigo = "0" + (code + 1);
            else
                codigo = (code + 1).ToString();

           
            return Json(codigo, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AddOrden(OrdenWS ordenWS)
        {
            Orden orden = db.Ordenes.DefaultIfEmpty(null).FirstOrDefault(x => x.CodigoOrden == ordenWS.codigo);
            ResultadoWS resultadoWS = new ResultadoWS();

            if (orden == null)
            {
                orden = new Orden();
                orden.CodigoOrden = ordenWS.codigo;
                orden.FechaOrden = ordenWS.fechaorden;
                orden.TiempoOrden = ordenWS.tiempoorden;
                orden.EstadoOrden = orden.EstadoOrden;

                db.Ordenes.Add(orden);

                int safe = db.SaveChanges();

                if (safe > 0)
                {
                    resultadoWS.Mensaje = orden.Id.ToString();
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




    }
}