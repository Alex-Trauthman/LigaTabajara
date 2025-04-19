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
    public class CartoesController : Controller
    {
        private readonly SistemaTabajaraContext _context;

        public CartoesController(SistemaTabajaraContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var Cartoes = _context.Cartoes
                .Include(c => c.Jogador)
                .Include(c => c.Partida)
                .ToList();
            return View(Cartoes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cartao = _context.Cartoes
                .Include(c => c.Jogador)
                .Include(c => c.Partida)
                .FirstOrDefault(c => c.Id == id);
            if (cartao == null) return HttpNotFound();
            return View(cartao);
        }

        public ActionResult Create()
        {
            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome");
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PartidaId,JogadorId,Minuto,Tipo")] Cartao cartao)
        {
            if (cartao.Minuto < 1 || cartao.Minuto > 120)
            {
                ModelState.AddModelError("Minuto", "O minuto deve estar entre 1 e 120.");
            }

            if (ModelState.IsValid)
            {
                _context.Cartoes.Add(cartao);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", cartao.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", cartao.PartidaId);
            return View(cartao);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cartao = _context.Cartoes.Find(id);
            if (cartao == null) return HttpNotFound();
            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", cartao.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", cartao.PartidaId);
            return View(cartao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PartidaId,JogadorId,Minuto,Tipo")] Cartao cartao)
        {
            if (cartao.Minuto < 1 || cartao.Minuto > 120)
            {
                ModelState.AddModelError("Minuto", "O minuto deve estar entre 1 e 120.");
            }

            if (ModelState.IsValid)
            {
                _context.Entry(cartao).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorId = new SelectList(_context.Jogadores, "Id", "Nome", cartao.JogadorId);
            ViewBag.PartidaId = new SelectList(_context.Partidas, "Id", "Estadio", cartao.PartidaId);
            return View(cartao);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cartao = _context.Cartoes.Find(id);
            if (cartao == null) return HttpNotFound();
            return View(cartao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cartao = _context.Cartoes.Find(id);
            _context.Cartoes.Remove(cartao);
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
