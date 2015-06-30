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
    public class rel_solicitante_avalController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: rel_solicitante_aval
        public async Task<ActionResult> Index()
        {
            var rel_solicitante_aval = db.rel_solicitante_aval.Include(r => r.aval).Include(r => r.solicitante);
            return View(await rel_solicitante_aval.ToListAsync());
        }

        // GET: rel_solicitante_aval/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rel_solicitante_aval rel_solicitante_aval = await db.rel_solicitante_aval.FindAsync(id);
            if (rel_solicitante_aval == null)
            {
                return HttpNotFound();
            }
            return View(rel_solicitante_aval);
        }

        // GET: rel_solicitante_aval/Create
        public ActionResult Create()
        {
            ViewBag.id_aval = new SelectList(db.avals, "uid", "num_socio");
            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante");
            return View();
        }

        // POST: rel_solicitante_aval/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_solicitante,id_aval,cantidad,ingresos_mensuales,ingresos_extra,gastos_mensuales,otras_deudas")] rel_solicitante_aval rel_solicitante_aval)
        {
            if (ModelState.IsValid)
            {
                db.rel_solicitante_aval.Add(rel_solicitante_aval);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_aval = new SelectList(db.avals, "uid", "num_socio", rel_solicitante_aval.id_aval);
            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", rel_solicitante_aval.id_solicitante);
            return View(rel_solicitante_aval);
        }

        // GET: rel_solicitante_aval/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rel_solicitante_aval rel_solicitante_aval = await db.rel_solicitante_aval.FindAsync(id);
            if (rel_solicitante_aval == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_aval = new SelectList(db.avals, "uid", "num_socio", rel_solicitante_aval.id_aval);
            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", rel_solicitante_aval.id_solicitante);
            return View(rel_solicitante_aval);
        }

        // POST: rel_solicitante_aval/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_solicitante,id_aval,cantidad,ingresos_mensuales,ingresos_extra,gastos_mensuales,otras_deudas")] rel_solicitante_aval rel_solicitante_aval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rel_solicitante_aval).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_aval = new SelectList(db.avals, "uid", "num_socio", rel_solicitante_aval.id_aval);
            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", rel_solicitante_aval.id_solicitante);
            return View(rel_solicitante_aval);
        }

        // GET: rel_solicitante_aval/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rel_solicitante_aval rel_solicitante_aval = await db.rel_solicitante_aval.FindAsync(id);
            if (rel_solicitante_aval == null)
            {
                return HttpNotFound();
            }
            return View(rel_solicitante_aval);
        }

        // POST: rel_solicitante_aval/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            rel_solicitante_aval rel_solicitante_aval = await db.rel_solicitante_aval.FindAsync(id);
            db.rel_solicitante_aval.Remove(rel_solicitante_aval);
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
