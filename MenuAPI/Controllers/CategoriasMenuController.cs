using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MenuAPI;

namespace MenuAPI.Controllers
{
    public class CategoriasMenuController : Controller
    {
        private MenuXallyContainer db = new MenuXallyContainer();

        // GET: CategoriasMenu
        public ActionResult Index()
        {
            return View(db.TiposDePlatillo.ToList());
        }

        // GET: CategoriasMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMenu categoriaMenu = db.TiposDePlatillo.Find(id);
            if (categoriaMenu == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMenu);
        }

        // GET: CategoriasMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriasMenu/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodigoTipoPlatillo,DescripcionTipoPlatillo,EstadoTipoPlatillo")] CategoriaMenu categoriaMenu)
        {
            if (ModelState.IsValid)
            {
                db.TiposDePlatillo.Add(categoriaMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaMenu);
        }

        // GET: CategoriasMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMenu categoriaMenu = db.TiposDePlatillo.Find(id);
            if (categoriaMenu == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMenu);
        }

        // POST: CategoriasMenu/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodigoTipoPlatillo,DescripcionTipoPlatillo,EstadoTipoPlatillo")] CategoriaMenu categoriaMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaMenu);
        }

        // GET: CategoriasMenu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMenu categoriaMenu = db.TiposDePlatillo.Find(id);
            if (categoriaMenu == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMenu);
        }

        // POST: CategoriasMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaMenu categoriaMenu = db.TiposDePlatillo.Find(id);
            db.TiposDePlatillo.Remove(categoriaMenu);
            db.SaveChanges();
            return RedirectToAction("Index");
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
