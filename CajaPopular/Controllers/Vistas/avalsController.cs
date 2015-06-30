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
    public class avalsController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: avals
        public async Task<ActionResult> Index()
        {
            var avals = db.avals.Include(a => a.persona);
            return View(await avals.ToListAsync());
        }

        // GET: avals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aval aval = await db.avals.FindAsync(id);
            if (aval == null)
            {
                return HttpNotFound();
            }
            return View(aval);
        }

        // GET: avals/Create
        public ActionResult Create()
        {
            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre");
            return View();
        }

        // POST: avals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,id_persona,num_socio")] aval aval)
        {
            if (ModelState.IsValid)
            {
                db.avals.Add(aval);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre", aval.id_persona);
            return View(aval);
        }

        // GET: avals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aval aval = await db.avals.FindAsync(id);
            if (aval == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre", aval.id_persona);
            return View(aval);
        }

        // POST: avals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,id_persona,num_socio")] aval aval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aval).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre", aval.id_persona);
            return View(aval);
        }

        // GET: avals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aval aval = await db.avals.FindAsync(id);
            if (aval == null)
            {
                return HttpNotFound();
            }
            return View(aval);
        }

        // POST: avals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            aval aval = await db.avals.FindAsync(id);
            db.avals.Remove(aval);
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
