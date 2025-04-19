using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaTabajara.Data;
using SistemaTabajara.Models;

namespace SistemaTabajara.Controllers
{
    public class PartidasController : Controller
    {
        private SistemaTabajaraContext db = new SistemaTabajaraContext();

        // GET: Partidas
        public ActionResult Index()
        {
            var partidas = db.Partidas.Include(p => p.Liga).Include(p => p.Mandante).Include(p => p.Visitante);
            return View(partidas.ToList());
        }

        // GET: Partidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partida partida = db.Partidas.Find(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            return View(partida);
        }

        // GET: Partidas/Create
        public ActionResult Create()
        {
            ViewBag.LigaId = new SelectList(db.Ligas, "Id", "Nome");
            ViewBag.MandanteId = new SelectList(db.Times, "Id", "Nome");
            ViewBag.VisitanteId = new SelectList(db.Times, "Id", "Nome");
            return View();
        }

        // POST: Partidas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DataHora,Rodada,Estadio,MandanteId,VisitanteId,LigaId")] Partida partida)
        {
            if (ModelState.IsValid)
            {
                db.Partidas.Add(partida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LigaId = new SelectList(db.Ligas, "Id", "Nome", partida.LigaId);
            ViewBag.MandanteId = new SelectList(db.Times, "Id", "Nome", partida.MandanteId);
            ViewBag.VisitanteId = new SelectList(db.Times, "Id", "Nome", partida.VisitanteId);
            return View(partida);
        }

        // GET: Partidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partida partida = db.Partidas.Find(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            ViewBag.LigaId = new SelectList(db.Ligas, "Id", "Nome", partida.LigaId);
            ViewBag.MandanteId = new SelectList(db.Times, "Id", "Nome", partida.MandanteId);
            ViewBag.VisitanteId = new SelectList(db.Times, "Id", "Nome", partida.VisitanteId);
            return View(partida);
        }

        // POST: Partidas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DataHora,Rodada,Estadio,MandanteId,VisitanteId,LigaId")] Partida partida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LigaId = new SelectList(db.Ligas, "Id", "Nome", partida.LigaId);
            ViewBag.MandanteId = new SelectList(db.Times, "Id", "Nome", partida.MandanteId);
            ViewBag.VisitanteId = new SelectList(db.Times, "Id", "Nome", partida.VisitanteId);
            return View(partida);
        }

        // GET: Partidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partida partida = db.Partidas.Find(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            return View(partida);
        }

        // POST: Partidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partida partida = db.Partidas.Find(id);
            db.Partidas.Remove(partida);
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
