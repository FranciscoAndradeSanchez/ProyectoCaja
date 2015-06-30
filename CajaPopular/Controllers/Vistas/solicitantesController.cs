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
    public class solicitantesController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: solicitantes
        public async Task<ActionResult> Index()
        {
            var solicitantes = db.solicitantes.Include(s => s.persona);
            return View(await solicitantes.ToListAsync());
        }

        // GET: solicitantes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitante solicitante = await db.solicitantes.FindAsync(id);
            if (solicitante == null)
            {
                return HttpNotFound();
            }
            return View(solicitante);
        }

        // GET: solicitantes/Create
        public ActionResult Create()
        {
            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre");
            return View();
        }

        // POST: solicitantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,num_solicitante,id_persona,numero_socio,dependientes,sueldo,sueldo_diario,departamento")] solicitante solicitante)
        {
            if (ModelState.IsValid)
            {
                db.solicitantes.Add(solicitante);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre", solicitante.id_persona);
            return View(solicitante);
        }

        // GET: solicitantes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitante solicitante = await db.solicitantes.FindAsync(id);
            if (solicitante == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre", solicitante.id_persona);
            return View(solicitante);
        }

        // POST: solicitantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,num_solicitante,id_persona,numero_socio,dependientes,sueldo,sueldo_diario,departamento")] solicitante solicitante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitante).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_persona = new SelectList(db.personas, "uid", "nombre", solicitante.id_persona);
            return View(solicitante);
        }

        // GET: solicitantes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitante solicitante = await db.solicitantes.FindAsync(id);
            if (solicitante == null)
            {
                return HttpNotFound();
            }
            return View(solicitante);
        }

        // POST: solicitantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            solicitante solicitante = await db.solicitantes.FindAsync(id);
            db.solicitantes.Remove(solicitante);
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
