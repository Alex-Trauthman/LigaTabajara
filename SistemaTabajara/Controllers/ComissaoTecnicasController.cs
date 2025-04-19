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
    public class ComissaoTecnicasController : Controller
    {
        private readonly SistemaTabajaraContext _context;

        public ComissaoTecnicasController(SistemaTabajaraContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var comissaoTecnicas = _context.ComissaoTecnicas.Include(c => c.Time).ToList();
            return View(comissaoTecnicas);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var comissao = _context.ComissaoTecnicas.Include(c => c.Time).FirstOrDefault(c => c.Id == id);
            if (comissao == null) return HttpNotFound();
            return View(comissao);
        }

        public ActionResult Create()
        {
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,DataNascimento,Cargo,TimeId")] ComissaoTecnica comissao)
        {
            if (_context.ComissaoTecnicas.Any(c => c.TimeId == comissao.TimeId && c.Cargo == comissao.Cargo))
            {
                ModelState.AddModelError("Cargo", "Este cargo já está ocupado neste time.");
            }

            if (ModelState.IsValid)
            {
                _context.ComissaoTecnicas.Add(comissao);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", comissao.TimeId);
            return View(comissao);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var comissao = _context.ComissaoTecnicas.Find(id);
            if (comissao == null) return HttpNotFound();
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", comissao.TimeId);
            return View(comissao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataNascimento,Cargo,TimeId")] ComissaoTecnica comissao)
        {
            if (_context.ComissaoTecnicas.Any(c => c.TimeId == comissao.TimeId && c.Cargo == comissao.Cargo && c.Id != comissao.Id))
            {
                ModelState.AddModelError("Cargo", "Este cargo já está ocupado neste time.");
            }

            if (ModelState.IsValid)
            {
                _context.Entry(comissao).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", comissao.TimeId);
            return View(comissao);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var comissao = _context.ComissaoTecnicas.Find(id);
            if (comissao == null) return HttpNotFound();
            return View(comissao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var comissao = _context.ComissaoTecnicas.Find(id);
            _context.ComissaoTecnicas.Remove(comissao);
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
