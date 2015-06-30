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
    public class personas1Controller : Controller
    {
        private caja_popular2Entities db = new caja_popular2Entities();

        // GET: personas1
        public async Task<ActionResult> Index()
        {
            return View(await db.personas.ToListAsync());
        }

        // GET: personas1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona persona = await db.personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: personas1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: personas1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uid,nombre,paterno,materno,fecha_nacimiento,domicilio,colonia,cruce1,cruce2,sector,cp,ciudad,estado,municipio,edo_civil,reg_matrimonial,telefono_casa,telefono_celular,tipo_vivienda,tiempo_residiendo,conyuge,ocupacion_conyuge")] persona persona)
        {
            if (ModelState.IsValid)
            {
                db.personas.Add(persona);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        // GET: personas1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona persona = await db.personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: personas1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uid,nombre,paterno,materno,fecha_nacimiento,domicilio,colonia,cruce1,cruce2,sector,cp,ciudad,estado,municipio,edo_civil,reg_matrimonial,telefono_casa,telefono_celular,tipo_vivienda,tiempo_residiendo,conyuge,ocupacion_conyuge")] persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // GET: personas1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona persona = await db.personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: personas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            persona persona = await db.personas.FindAsync(id);
            db.personas.Remove(persona);
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
