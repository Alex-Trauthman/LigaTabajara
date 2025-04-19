using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaTabajara.Data;
using SistemaTabajara.Models;

namespace SistemaTabajara.Controllers
{
    public class ParticipacoesController : Controller
    {
        private readonly SistemaTabajaraContext _context;

        public ParticipacoesController()
        {
            _context = new SistemaTabajaraContext();
        }

        public ActionResult Index()
        {
            var participacoes = _context.Participacoes
                .Include(p => p.Liga)
                .Include(p => p.Time)
                .ToList();
            return View(participacoes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var participacao = _context.Participacoes
                .Include(p => p.Liga)
                .Include(p => p.Time)
                .FirstOrDefault(p => p.Id == id);
            if (participacao == null) return HttpNotFound();
            return View(participacao);
        }

        public ActionResult Create()
        {
            ViewBag.LigaId = new SelectList(_context.Ligas, "Id", "Nome");
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeId,LigaId,Pontos,Jogos,Vitorias,Empates,Derrotas,GolsPro,GolsContra")] Participacao participacao)
        {
            if (_context.Participacoes.Any(p => p.LigaId == participacao.LigaId && p.TimeId == participacao.TimeId))
            {
                ModelState.AddModelError("TimeId", "Este time já está participando desta liga.");
            }

            if (ModelState.IsValid)
            {
                _context.Participacoes.Add(participacao);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LigaId = new SelectList(_context.Ligas, "Id", "Nome", participacao.LigaId);
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", participacao.TimeId);
            return View(participacao);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var participacao = _context.Participacoes.Find(id);
            if (participacao == null) return HttpNotFound();
            ViewBag.LigaId = new SelectList(_context.Ligas, "Id", "Nome", participacao.LigaId);
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", participacao.TimeId);
            return View(participacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeId,LigaId,Pontos,Jogos,Vitorias,Empates,Derrotas,GolsPro,GolsContra")] Participacao participacao)
        {
            if (_context.Participacoes.Any(p => p.LigaId == participacao.LigaId && p.TimeId == participacao.TimeId && p.Id != participacao.Id))
            {
                ModelState.AddModelError("TimeId", "Este time já está participando desta liga.");
            }

            if (ModelState.IsValid)
            {
                _context.Entry(participacao).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LigaId = new SelectList(_context.Ligas, "Id", "Nome", participacao.LigaId);
            ViewBag.TimeId = new SelectList(_context.Times, "Id", "Nome", participacao.TimeId);
            return View(participacao);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var participacao = _context.Participacoes.Find(id);
            if (participacao == null) return HttpNotFound();
            return View(participacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var participacao = _context.Participacoes.Find(id);
            _context.Participacoes.Remove(participacao);
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