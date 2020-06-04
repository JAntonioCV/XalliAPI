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
    public class DetalleDeOrdensController : Controller
    {
        private MenuXallyContainer db = new MenuXallyContainer();

        // GET: DetalleDeOrdens
        public ActionResult Index()
        {
            var detallesDeOrden = db.DetallesDeOrden.Include(d => d.Menu).Include(d => d.Orden);
            return View(detallesDeOrden.ToList());
        }

        // GET: DetalleDeOrdens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleDeOrden detalleDeOrden = db.DetallesDeOrden.Find(id);
            if (detalleDeOrden == null)
            {
                return HttpNotFound();
            }
            return View(detalleDeOrden);
        }

        // GET: DetalleDeOrdens/Create
        public ActionResult Create()
        {
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu");
            ViewBag.OrdenId = new SelectList(db.Ordenes, "Id", "CodigoOrden");
            return View();
        }

        // POST: DetalleDeOrdens/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CantidadOrden,NotaDetalleOrden,EstadoDetalleOrden,MenuId,OrdenId")] DetalleDeOrden detalleDeOrden)
        {
            if (ModelState.IsValid)
            {
                db.DetallesDeOrden.Add(detalleDeOrden);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu", detalleDeOrden.MenuId);
            ViewBag.OrdenId = new SelectList(db.Ordenes, "Id", "CodigoOrden", detalleDeOrden.OrdenId);
            return View(detalleDeOrden);
        }

        // GET: DetalleDeOrdens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleDeOrden detalleDeOrden = db.DetallesDeOrden.Find(id);
            if (detalleDeOrden == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu", detalleDeOrden.MenuId);
            ViewBag.OrdenId = new SelectList(db.Ordenes, "Id", "CodigoOrden", detalleDeOrden.OrdenId);
            return View(detalleDeOrden);
        }

        // POST: DetalleDeOrdens/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CantidadOrden,NotaDetalleOrden,EstadoDetalleOrden,MenuId,OrdenId")] DetalleDeOrden detalleDeOrden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleDeOrden).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu", detalleDeOrden.MenuId);
            ViewBag.OrdenId = new SelectList(db.Ordenes, "Id", "CodigoOrden", detalleDeOrden.OrdenId);
            return View(detalleDeOrden);
        }

        // GET: DetalleDeOrdens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleDeOrden detalleDeOrden = db.DetallesDeOrden.Find(id);
            if (detalleDeOrden == null)
            {
                return HttpNotFound();
            }
            return View(detalleDeOrden);
        }

        // POST: DetalleDeOrdens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleDeOrden detalleDeOrden = db.DetallesDeOrden.Find(id);
            db.DetallesDeOrden.Remove(detalleDeOrden);
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
