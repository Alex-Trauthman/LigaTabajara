using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaTabajara.Data;
using SistemaTabajara.Models;

namespace SistemaTabajara.Controllers
{
    public class LigasController : Controller
    {
        private readonly SistemaTabajaraContext _context;

        public LigasController()
        {
            _context = new SistemaTabajaraContext();
        }

        public ActionResult Index()
        {
            var ligas = _context.Ligas.ToList();
            return View(ligas);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var liga = _context.Ligas
                .Include(l => l.Participacoes.Select(p => p.Time))
                .FirstOrDefault(l => l.Id == id);
            if (liga == null) return HttpNotFound();
            ViewBag.IsApta = IsLigaApta(liga);
            return View(liga);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,DataInicio,DataFim")] Liga liga)
        {
            if (ModelState.IsValid)
            {
                _context.Ligas.Add(liga);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liga);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var liga = _context.Ligas.Find(id);
            if (liga == null) return HttpNotFound();
            return View(liga);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataInicio,DataFim")] Liga liga)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(liga).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liga);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var liga = _context.Ligas.Find(id);
            if (liga == null) return HttpNotFound();
            return View(liga);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var liga = _context.Ligas.Find(id);
            _context.Ligas.Remove(liga);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool IsLigaApta(Liga liga)
        {
            if (liga == null) return false;

            var times = _context.Times
                .Include(t => t.Jogadores)
                .Include(t => t.ComissaoTecnica)
                .Where(t => liga.Participacoes.Any(p => p.TimeId == t.Id))
                .ToList();

            bool hasExactly20Times = liga.Participacoes.Count == 20;
            bool allTimesApto = times.All(t => IsTimeApto(t));

            return hasExactly20Times && allTimesApto;
        }

        private bool IsTimeApto(Time time)
        {
            if (time == null) return false;

            bool hasEnoughPlayers = time.Jogadores.Count >= 30;
            bool hasEnoughCommission = time.ComissaoTecnica.Count >= 5;
            bool hasUniqueCargos = time.ComissaoTecnica
                .GroupBy(c => c.Cargo)
                .All(g => g.Count() == 1);
            bool hasRequiredFields = !string.IsNullOrEmpty(time.Nome) &&
                                     !string.IsNullOrEmpty(time.Estadio) &&
                                     !string.IsNullOrEmpty(time.Cidade) &&
                                     time.CapacidadeEstadio > 0;

            return hasEnoughPlayers && hasEnoughCommission && hasUniqueCargos && hasRequiredFields;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}