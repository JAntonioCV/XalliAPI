using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuAPI.Areas.API.Models;

namespace MenuAPI.Areas.API.Controllers
{
    public class MenusWSController : Controller
    {
        private MenuXallyContainer db = new MenuXallyContainer();

        [HttpGet]
        public JsonResult Menus()
        {
            var menus = (from m in db.Menus
                         select new MenuWS
                         {
                             id = m.Id,
                             codigo = m.CodigoMenu,
                             descripcion = m.DescripcionPlatillo,
                             precio = m.PrecioPlatillo,
                             estado = m.EstadoPlatillo,
                             idcategoria = m.TipoDePlatilloId
                            
                              }).ToList();

            return Json(menus , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Menu(int id)
        {
            var menu = (from m in db.Menus
                              where m.Id == id
                              select new MenuWS
                              {
                                  id = m.Id,
                                  codigo = m.CodigoMenu,
                                  descripcion = m.DescripcionPlatillo,
                                  precio = m.PrecioPlatillo,
                                  estado = m.EstadoPlatillo

                              }).ToList();

            return Json(menu , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MenusCategoria(int id)
        {
            var menu = (from m in db.Menus
                        where m.TipoDePlatilloId == id
                        select new MenuWS
                        {
                            id = m.Id,
                            codigo = m.CodigoMenu,
                            descripcion = m.DescripcionPlatillo,
                            precio = m.PrecioPlatillo,
                            estado = m.EstadoPlatillo,
                            idcategoria = m.TipoDePlatilloId
                          
                        }).ToList();

            return Json(menu, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProducto(MenuWS menuWS, int idcategoria)
        {
            Menu menu = db.Menus.DefaultIfEmpty(null).FirstOrDefault(x => x.CodigoMenu == menuWS.codigo);
            ResultadoWS resultadoWS = new ResultadoWS();

            if (menu == null)
            {
                menu = new Menu();
                menu.CodigoMenu = menuWS.codigo;
                menu.DescripcionPlatillo = menuWS.descripcion;
                menu.PrecioPlatillo = menuWS.precio;
                menu.EstadoPlatillo = menu.EstadoPlatillo;
                menu.TipoDePlatilloId = idcategoria;


                db.Menus.Add(menu);

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