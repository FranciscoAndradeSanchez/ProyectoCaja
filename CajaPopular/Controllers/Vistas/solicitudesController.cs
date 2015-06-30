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
    public class solicitudesController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: solicitudes
        public async Task<ActionResult> Index()
        {
            var solicituds = db.solicituds.Include(s => s.solicitante);
            return View(await solicituds.ToListAsync());
        }

        // GET: solicitudes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitud solicitud = await db.solicituds.FindAsync(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // GET: solicitudes/Create
        public ActionResult Create()
        {
            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante");
            return View();
        }

        // POST: solicitudes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,id_solicitante,fecha,cantidad_solicitada,plazo_solicitado,cantidad_maxima,finalidad,entrega_prestamo,clave_interbancaria,tipo_prestamo,estatus,monto_autorizado,plazo_autorizado,fecha_resolucion,responsable_autorizacion")] solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.solicituds.Add(solicitud);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", solicitud.id_solicitante);
            return View(solicitud);
        }

        // GET: solicitudes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitud solicitud = await db.solicituds.FindAsync(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", solicitud.id_solicitante);
            return View(solicitud);
        }

        // POST: solicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,id_solicitante,fecha,cantidad_solicitada,plazo_solicitado,cantidad_maxima,finalidad,entrega_prestamo,clave_interbancaria,tipo_prestamo,estatus,monto_autorizado,plazo_autorizado,fecha_resolucion,responsable_autorizacion")] solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", solicitud.id_solicitante);
            return View(solicitud);
        }

        // GET: solicitudes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitud solicitud = await db.solicituds.FindAsync(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // POST: solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            solicitud solicitud = await db.solicituds.FindAsync(id);
            db.solicituds.Remove(solicitud);
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
