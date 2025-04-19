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
    // GolsController
    public class GolsController : Controller
    {
        private readonly SistemaTabajaraContext _context;

        public GolsController(SistemaTabajaraContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var gols = _context.Gols
                .Include(g => g.Jogador)
                .Include(g => g.Partida)
                .ToList();
            return View(gols);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var gol = _context.Gols
                .Include(g => g.Jogador)
                .Include(g => g.Partida)
                .FirstOrDefault(g => g.Id == id);
            if (gol == null) return HttpNotFound();
            return View(gol);
        }

        public ActionResult Create()
        {
            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome");
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PartidaId,JogadorId,Minuto,TipoGol")] Gol gol)
        {
            if (gol.Minuto < 1 || gol.Minuto > 120)
            {
                ModelState.AddModelError("Minuto", "O minuto deve estar entre 1 e 120.");
            }

            if (ModelState.IsValid)
            {
                _context.Gols.Add(gol);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", gol.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", gol.PartidaId);
            return View(gol);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var gol = _context.Gols.Find(id);
            if (gol == null) return HttpNotFound();
            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", gol.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", gol.PartidaId);
            return View(gol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PartidaId,JogadorId,Minuto,TipoGol")] Gol gol)
        {
            if (gol.Minuto < 1 || gol.Minuto > 120)
            {
                ModelState.AddModelError("Minuto", "O minuto deve estar entre 1 e 120.");
            }

            if (ModelState.IsValid)
            {
                _context.Entry(gol).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", gol.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", gol.PartidaId);
            return View(gol);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var gol = _context.Gols.Find(id);
            if (gol == null) return HttpNotFound();
            return View(gol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var gol = _context.Gols.Find(id);
            _context.Gols.Remove(gol);
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
