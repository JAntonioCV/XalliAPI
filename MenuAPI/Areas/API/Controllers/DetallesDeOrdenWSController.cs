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
        public JsonResult OrdenesDetalle(OrdenWS ordenWS, List<DetallesDeOrdenWS> detallesWS)
        {
            ResultadoWS resultadoWS = new ResultadoWS();

            Orden orden = new Orden();

            Orden ord = db.Ordenes.DefaultIfEmpty(null).FirstOrDefault(o => o.CodigoOrden.Trim() == ordenWS.codigo.Trim());

            if (ord == null)
            {
                orden.CodigoOrden = ordenWS.codigo;
                orden.FechaOrden = ordenWS.fechaorden;
                orden.TiempoOrden = ordenWS.tiempoorden;
                orden.EstadoOrden = ordenWS.estado;

                db.Ordenes.Add(orden);

                int saveorder = db.SaveChanges();

                if (saveorder > 0)
                {
                    foreach (var DetalleActual in detallesWS)
                    {
                        DetalleDeOrden detallesDeOrden = new DetalleDeOrden();
                        detallesDeOrden.CantidadOrden = DetalleActual.cantidadorden;
                        detallesDeOrden.NotaDetalleOrden = DetalleActual.notaorden;
                        detallesDeOrden.EstadoDetalleOrden = DetalleActual.estado;
                        detallesDeOrden.MenuId = DetalleActual.menuid;
                        detallesDeOrden.OrdenId = orden.Id;

                        db.DetallesDeOrden.Add(detallesDeOrden);
                    }

                    int saveDetails = db.SaveChanges();

                    if (saveDetails > 0)
                    {
                        resultadoWS.Mensaje = "Almecenado con exito";
                        resultadoWS.Resultado = true;
                    }
                    else
                    {
                        resultadoWS.Mensaje = "Error al guardar el detalle";
                        resultadoWS.Resultado = false;
                    }
                }
                else
                {
                    resultadoWS.Mensaje = "Error al guardar la orden";
                    resultadoWS.Resultado = false;
                }
            }
            else
            {
                resultadoWS.Mensaje = "El codigo ya existe";
                resultadoWS.Resultado = false;
            }

            return Json(resultadoWS);
        }

    }
}