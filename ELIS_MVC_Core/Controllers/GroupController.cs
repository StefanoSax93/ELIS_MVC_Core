using ELIS_MVC_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace ELIS_MVC_Core.Controllers
{
    public class GroupController : Controller
    {
        private readonly CorsiContext _context;

        public GroupController(CorsiContext context)
        {
            _context = context;
        }
        public IActionResult ConteggioEdizioni()
        {
            ViewBag.Elenco = (from c in _context.Corsis
                             join e in _context.Edizionis
                             on c.Idcorso equals e.Idcorso
                             group e by c into g
                             select new
                             {
                                 TitoloCorso = g.Key.Titolo,
                                 N_edizioni = g.Count()
                             }).ToList();

            return View();
        }

        public IActionResult ChiamaSP()
        {
            var query = _context.GetProcedures().usp_PartecipantiEdizioniAsync().Result;
            return View(query);
        }
    }
}
