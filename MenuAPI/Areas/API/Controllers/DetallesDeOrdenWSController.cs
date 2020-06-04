using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuAPI.Areas.API.Models;

namespace MenuAPI.Areas.API.Controllers
{
    public class DetallesDeOrdenWSController : Controller
    {

        private MenuXallyContainer db = new MenuXallyContainer();


        // ordenes atendidas !! ordenes no atendidas
        [HttpPost]
        public JsonResult OrdenesDetalle(List<DetallesDeOrdenWS> detallesDeOrdenWS)
        {
            int contador=0;
            ResultadoWS resultadoWS = new ResultadoWS();
            foreach (var DetalleActual in detallesDeOrdenWS)
            {
                DetalleDeOrden detallesDeOrden = new DetalleDeOrden();
                detallesDeOrden.CantidadOrden = DetalleActual.cantidadorden;
                detallesDeOrden.NotaDetalleOrden = DetalleActual.notaorden;
                detallesDeOrden.EstadoDetalleOrden = DetalleActual.estado;
                detallesDeOrden.MenuId = DetalleActual.menuid;
                detallesDeOrden.OrdenId = DetalleActual.ordenid;

                db.DetallesDeOrden.Add(detallesDeOrden);

                int safe = db.SaveChanges();

                if (safe > 0)
                {
                    contador++;

                    if (detallesDeOrdenWS.Count() == contador)
                    {
                        resultadoWS.Mensaje = contador.ToString();
                        resultadoWS.Resultado = true;
                        return Json(resultadoWS);
                    }
                }
                else
                {
                    resultadoWS.Mensaje = "Error al guardar";
                    resultadoWS.Resultado = false;
                    return Json(resultadoWS);
                }


            }

            resultadoWS.Mensaje = "Ni entro al foreach pendejo XD";
            resultadoWS.Resultado = false;

            return Json(resultadoWS);
        }

    }
}