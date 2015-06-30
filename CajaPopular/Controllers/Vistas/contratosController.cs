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
    public class contratosController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: contratos
        public async Task<ActionResult> Index()
        {
            var contratoes = db.contratoes.Include(c => c.rel_solicitante_aval);
            return View(await contratoes.ToListAsync());
        }

        // GET: contratos/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contrato contrato = await db.contratoes.FindAsync(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // GET: contratos/Create
        public ActionResult Create()
        {
            ViewBag.id_solicitante = new SelectList(db.rel_solicitante_aval, "id_solicitante", "id_solicitante");
            return View();
        }

        // POST: contratos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,id_solicitud,id_solicitante,id_aval")] contrato contrato)
        {
            if (ModelState.IsValid)
            {
                db.contratoes.Add(contrato);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_solicitante = new SelectList(db.rel_solicitante_aval, "id_solicitante", "id_solicitante", contrato.id_solicitante);
            return View(contrato);
        }

        // GET: contratos/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contrato contrato = await db.contratoes.FindAsync(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_solicitante = new SelectList(db.rel_solicitante_aval, "id_solicitante", "id_solicitante", contrato.id_solicitante);
            return View(contrato);
        }

        // POST: contratos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,id_solicitud,id_solicitante,id_aval")] contrato contrato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contrato).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_solicitante = new SelectList(db.rel_solicitante_aval, "id_solicitante", "id_solicitante", contrato.id_solicitante);
            return View(contrato);
        }

        // GET: contratos/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contrato contrato = await db.contratoes.FindAsync(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // POST: contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            contrato contrato = await db.contratoes.FindAsync(id);
            db.contratoes.Remove(contrato);
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
