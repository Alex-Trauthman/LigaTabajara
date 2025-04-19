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
    public class EstatisticaPartidasController : Controller
    {
        private readonly SistemaTabajaraContext _context;

        public EstatisticaPartidasController(SistemaTabajaraContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var estatisticas = _context.EstatisticaPartidas
                .Include(e => e.Jogador)
                .Include(e => e.Partida)
                .ToList();
            return View(estatisticas);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var estatistica = _context.EstatisticaPartidas
                .Include(e => e.Jogador)
                .Include(e => e.Partida)
                .FirstOrDefault(e => e.Id == id);
            if (estatistica == null) return HttpNotFound();
            return View(estatistica);
        }

        public ActionResult Create()
        {
            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome");
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JogadorId,PartidaId,MinutosJogados,Assistencias")] EstatisticaPartida estatistica)
        {
            if (ModelState.IsValid)
            {
                _context.EstatisticaPartidas.Add(estatistica);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", estatistica.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", estatistica.PartidaId);
            return View(estatistica);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var estatistica = _context.EstatisticaPartidas.Find(id);
            if (estatistica == null) return HttpNotFound();
            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", estatistica.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", estatistica.PartidaId);
            return View(estatistica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JogadorId,PartidaId,MinutosJogados,Assistencias")] EstatisticaPartida estatistica)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(estatistica).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", estatistica.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", estatistica.PartidaId);
            return View(estatistica);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var estatistica = _context.EstatisticaPartidas.Find(id);
            if (estatistica == null) return HttpNotFound();
            return View(estatistica);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var estatistica = _context.EstatisticaPartidas.Find(id);
            _context.EstatisticaPartidas.Remove(estatistica);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }

}
