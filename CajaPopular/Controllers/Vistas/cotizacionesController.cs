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
using CajaPopular.Models;

namespace CajaPopular.Controllers.Vistas
{
    public class cotizacionesController : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: cotizaciones
        public async Task<ActionResult> Index()
        {
            var cotizacions = db.cotizacions.Include(c => c.solicitante);
            return View(await cotizacions.ToListAsync());
        }








        // GET: cotizaciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var per = db.personas.Include(d=>d.solicitantes);
            var sol = db.solicitantes;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cotizacion cotizacion = await db.cotizacions.FindAsync(id);
            if (cotizacion == null)
            {
                return HttpNotFound();
            }
            var lista = new List<CotizacionResult>();
            int x = 0;
            CotizacionResult cr = new CotizacionResult();
            ClassCreateViemModel ccvm = new ClassCreateViemModel();
            cr.SaldoInicial = (double)cotizacion.monto_solicitado;
            cr.InteresTotal = 0;

            var cm = (double)cotizacion.monto_solicitado;
            var cp = (double)cotizacion.plazo;
            var ct = (double)cotizacion.tasa;
            var interes = (cm * ((cp) * (ct)/100)) * (1.16) / cp;
            var abono = (cm/cp)+  ((cm * ((cp) * (ct)/100)) * (1.16) / cp);
            var comodin = cr.SaldoInicial;
            for(x=0;x<cotizacion.plazo;x++)
            {

                cr.NumAbono = x;
                cr.Intereses = interes;
                cr.InteresTotal += interes;
                cr.Abono = abono;
                cr.Saldorestante = ((comodin + cr.Intereses) - cr.Abono);
                comodin = cr.Saldorestante;
                cr.SaldoTotal = cr.SaldoInicial + (interes/cp);
                lista.Add(cr);

                cr = new CotizacionResult {SaldoInicial = comodin};
            }
            var sols = from s in sol
                where s.num_solicitante == cotizacion.num_solicitante.ToString()
                select s;
            solicitante unico=null;
            foreach (var solic in sols)
            {
                unico = solic;
                break;
            }
            var pers = from p in per
                where p.uid == cotizacion.num_solicitante
                select p;
            cotizacion.persona = pers.SingleOrDefault();     
            cotizacion.CotizacionResultssList = lista;
            return View(cotizacion);
        }























        // GET: cotizaciones/Create
        public ActionResult Create()
        {
            ViewBag.num_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante");
            return View();
        }

        // POST: cotizaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,num_solicitante,monto_solicitado,plazo,fecha_captura,fecha_entrega,monto_asignado,tasa")] cotizacion cotizacion)
        {
           
            if (ModelState.IsValid)
            {
                db.cotizacions.Add(cotizacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.num_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", cotizacion.num_solicitante);
            return View(cotizacion);
        }

        // GET: cotizaciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cotizacion cotizacion = await db.cotizacions.FindAsync(id);
            if (cotizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.num_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", cotizacion.num_solicitante);
            return View(cotizacion);
        }

        // POST: cotizaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,num_solicitante,monto_solicitado,plazo,fecha_captura,fecha_entrega,monto_asignado,tasa")] cotizacion cotizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotizacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.num_solicitante = new SelectList(db.solicitantes, "uid", "num_solicitante", cotizacion.num_solicitante);
            return View(cotizacion);
        }

        // GET: cotizaciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cotizacion cotizacion = await db.cotizacions.FindAsync(id);
            if (cotizacion == null)
            {
                return HttpNotFound();
            }
            return View(cotizacion);
        }

        // POST: cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            cotizacion cotizacion = await db.cotizacions.FindAsync(id);
            db.cotizacions.Remove(cotizacion);
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
