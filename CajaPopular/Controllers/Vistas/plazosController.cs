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
    public class plazosController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: plazos
        public async Task<ActionResult> Index()
        {
            return View(await db.plazoes.ToListAsync());
        }

        // GET: plazos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plazo plazo = await db.plazoes.FindAsync(id);
            if (plazo == null)
            {
                return HttpNotFound();
            }
            return View(plazo);
        }

        // GET: plazos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: plazos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,plazo1,descripcion")] plazo plazo)
        {
            if (ModelState.IsValid)
            {
                db.plazoes.Add(plazo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(plazo);
        }

        // GET: plazos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plazo plazo = await db.plazoes.FindAsync(id);
            if (plazo == null)
            {
                return HttpNotFound();
            }
            return View(plazo);
        }

        // POST: plazos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,plazo1,descripcion")] plazo plazo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plazo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(plazo);
        }

        // GET: plazos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plazo plazo = await db.plazoes.FindAsync(id);
            if (plazo == null)
            {
                return HttpNotFound();
            }
            return View(plazo);
        }

        // POST: plazos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            plazo plazo = await db.plazoes.FindAsync(id);
            db.plazoes.Remove(plazo);
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
