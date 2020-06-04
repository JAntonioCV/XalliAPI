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
    public class RecetasController : Controller
    {
        private MenuXallyContainer db = new MenuXallyContainer();

        // GET: Recetas
        public ActionResult Index()
        {
            var recetas = db.Recetas.Include(r => r.Menu);
            return View(recetas.ToList());
        }

        // GET: Recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // GET: Recetas/Create
        public ActionResult Create()
        {
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu");
            return View();
        }

        // POST: Recetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DescripcionReceta,TiempoEstimadoReceta,EstadoReceta,IngredientesPrincipales,MenuId")] Receta receta)
        {
            if (ModelState.IsValid)
            {
                db.Recetas.Add(receta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu", receta.MenuId);
            return View(receta);
        }

        // GET: Recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu", receta.MenuId);
            return View(receta);
        }

        // POST: Recetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DescripcionReceta,TiempoEstimadoReceta,EstadoReceta,IngredientesPrincipales,MenuId")] Receta receta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "CodigoMenu", receta.MenuId);
            return View(receta);
        }

        // GET: Recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // POST: Recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receta receta = db.Recetas.Find(id);
            db.Recetas.Remove(receta);
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
