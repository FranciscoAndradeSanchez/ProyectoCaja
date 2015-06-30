using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CajaPopular;

namespace CajaPopular.Controllers.Vistas
{
    public class tasasController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: tasas
        public async Task<ActionResult> Index()
        {
            return View(await db.tasas.ToListAsync());
        }

        // GET: tasas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tasa tasa = await db.tasas.FindAsync(id);
            if (tasa == null)
            {
                return HttpNotFound();
            }
            return View(tasa);
        }

        // GET: tasas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tasas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,tasa1,descripcioin")] tasa tasa)
        {
            if (ModelState.IsValid)
            {
                db.tasas.Add(tasa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tasa);
        }

        // GET: tasas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tasa tasa = await db.tasas.FindAsync(id);
            if (tasa == null)
            {
                return HttpNotFound();
            }
            return View(tasa);
        }

        // POST: tasas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,tasa1,descripcioin")] tasa tasa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tasa);
        }

        // GET: tasas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tasa tasa = await db.tasas.FindAsync(id);
            if (tasa == null)
            {
                return HttpNotFound();
            }
            return View(tasa);
        }

        // POST: tasas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tasa tasa = await db.tasas.FindAsync(id);
            db.tasas.Remove(tasa);
            await db.SaveChangesAsync();
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
