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
    public class JogadoresController : Controller
    {
        private readonly SistemaTabajaraContext _context;

        public JogadoresController(SistemaTabajaraContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var jogadores = _context.Jogadores.Include(j => j.Time).ToList();
            return View(jogadores);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var jogador = _context.Jogadores.Include(j => j.Time).FirstOrDefault(j => j.Id == id);
            if (jogador == null) return HttpNotFound();
            return View(jogador);
        }

        public ActionResult Create()
        {
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,DataNascimento,Nacionalidade,Posicao,Camisa,Altura,Peso,PeDominante,TimeId")] Jogador jogador)
        {
            if (_context.Jogadores.Any(j => j.TimeId == jogador.TimeId && j.Camisa == jogador.Camisa))
            {
                ModelState.AddModelError("Camisa", "Número da camisa já está em uso neste time.");
            }

            if (ModelState.IsValid)
            {
                _context.Jogadores.Add(jogador);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", jogador.TimeId);
            return View(jogador);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var jogador = _context.Jogadores.Find(id);
            if (jogador == null) return HttpNotFound();
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", jogador.TimeId);
            return View(jogador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataNascimento,Nacionalidade,Posicao,Camisa,Altura,Peso,PeDominante,TimeId")] Jogador jogador)
        {
            if (_context.Jogadores.Any(j => j.TimeId == jogador.TimeId && j.Camisa == jogador.Camisa && j.Id != jogador.Id))
            {
                ModelState.AddModelError("Camisa", "Número da camisa já está em uso neste time.");
            }

            if (ModelState.IsValid)
            {
                _context.Entry(jogador).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", jogador.TimeId);
            return View(jogador);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var jogador = _context.Jogadores.Find(id);
            if (jogador == null) return HttpNotFound();
            return View(jogador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var jogador = _context.Jogadores.Find(id);
            _context.Jogadores.Remove(jogador);
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
