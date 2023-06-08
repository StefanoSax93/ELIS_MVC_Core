using ELIS_MVC_Core.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ELIS_MVC_Core.Controllers
{
	public class GestioneController : Controller
	{
		private readonly CorsiContext _context;

		public GestioneController(CorsiContext context)
		{
			_context = context;
		}

        #region CRUD CORSI
        public IActionResult Index()
        {
            // LINQ - Query Expression
            //var query = from c in _context.Corsis
            //			where c.Idcorso == 2
            //			select c;
            var query = from c in _context.Corsis
                        select c;

            // LINQ - Method Expression(Utilizza espressioni lambda)
            //var query1 = _context.Corsis.Where(c=>c.Idcorso == 2).ToList();
            var query1 = _context.Corsis.ToList(); // ToList() mi permette di recuperare tutti i corsi

            //return View(query.ToList());  se scrivo con la query expression devo aggiungere il ToList() alla fine
            return View(query1);
        }

        public IActionResult NuovoCorso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuovoCorso(int id, string titolo, int durata, string scaletta, decimal costo)
        {
            var nuovoCorso = new Corsi();
            nuovoCorso.Idcorso = id;
            nuovoCorso.Titolo = titolo;
            nuovoCorso.Durata = durata;
            nuovoCorso.Scaletta = scaletta;
            nuovoCorso.Costo = costo;

            _context.Corsis.Add(nuovoCorso);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditCorso(int? id)
        {

            var corso = _context.Corsis.Find(id);

            return View(corso);
        }

        [HttpPost]
        public IActionResult EditCorso(int id, string titolo, int durata, string scaletta, int costo)
        {

            var updatedRecord = new Corsi();
            updatedRecord.Idcorso = id;
            updatedRecord.Titolo = titolo;
            updatedRecord.Durata = durata;
            updatedRecord.Scaletta = scaletta;
            updatedRecord.Costo = costo;

            _context.Corsis.Update(updatedRecord);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region CRUD PARTECIPANTI
        public IActionResult Partecipanti()
        {

            var query = from c in _context.Partecipantis
                        select c;

            return View(query.ToList());
        }

        // stiamo facendo il create
        [HttpGet]
        public IActionResult NuovoPartecipante()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuovoPartecipante(int id, string nome, string cognome, DateTime data, string residenza, string studio)
        {
            var nuovoRecord = new Partecipanti();
            nuovoRecord.Idpartecipante = id;
            nuovoRecord.Nome = nome;
            nuovoRecord.Cognome = cognome;
            nuovoRecord.DataNascita = data;
            nuovoRecord.Residenza = residenza;
            nuovoRecord.TitoloStudio = studio;

            _context.Partecipantis.Add(nuovoRecord);
            _context.SaveChanges();

            return RedirectToAction("Partecipanti");
        }

        public IActionResult Edit(int? id)
        {

            var partecipante = _context.Partecipantis.Find(id);

            return View(partecipante);
        }

        [HttpPost]
        public IActionResult Edit(int id, string nome, string cognome, DateTime data, string residenza, string studio)
        {


            var updatedRecord = new Partecipanti();
            updatedRecord.Idpartecipante = id;
            updatedRecord.Nome = nome;
            updatedRecord.Cognome = cognome;
            updatedRecord.DataNascita = data;
            updatedRecord.Residenza = residenza;
            updatedRecord.TitoloStudio = studio;

            _context.Partecipantis.Update(updatedRecord);
            _context.SaveChanges();

            return RedirectToAction("Partecipanti");
        }
        #endregion

        #region CRUD EDIZIONI
        //EDIZIONI

        public IActionResult ElencoEdizioni()
        {
            var query = from c in _context.Corsis
                        join e in _context.Edizionis
                        on c.Idcorso equals e.Idcorso
                        orderby e.Idedizione
                        select new
                        {
                            e.Idedizione,
                            e.Idcorso,
                            c.Titolo,
                            c.Durata,
                            c.Costo,
                            e.DataInizio,
                            e.Luogo
                        };

            /*var query1 = _context.Corsis
				.Join(_context.Edizionis,c=>c.Idcorso,e=>e.Idcorso, (c,e) => new {c,e})
				.Select(x => new
				{
					x.e.Idedizione,
					x.e.Idcorso,
					x.c.Titolo,
					x.c.Durata,
					x.e.DataInizio,
					x.e.Luogo
				});*/

            ViewBag.Elenco = query.ToList();


            return View();
        }

        public IActionResult NuovaEdizione()
        {
            ViewBag.NuovoId = _context.Edizionis.Max(c => c.Idedizione) + 1;
            ViewData["Idcorso"] = new SelectList(_context.Corsis, "Idcorso", "Titolo");
            return View();
        }

        [HttpPost]
        public IActionResult NuovaEdizione(Edizioni edizione)
        {
            _context.Add(edizione);
            _context.SaveChangesAsync();
            return RedirectToAction("ElencoEdizioni");
        }

        public IActionResult EditEdizione(int? id)
        {

            var edizione = _context.Edizionis.Find(id);
            ViewData["Idcorso"] = new SelectList(_context.Corsis, "Idcorso", "Titolo", edizione?.Idcorso);
            return View(edizione);
        }


        [HttpPost]

        public IActionResult EditEdizione(int id, int Idcorso, DateTime DataInizio, string Luogo)
        {

            var updatedRecord = new Edizioni();
            updatedRecord.Idedizione = id;
            updatedRecord.Idcorso = Idcorso;
            updatedRecord.DataInizio = DataInizio;
            updatedRecord.Luogo = Luogo;

            _context.Edizionis.Update(updatedRecord);
            _context.SaveChanges();

            return RedirectToAction("ElencoEdizioni");

        }
        #endregion

        #region CRUD PARTECIPAZIONI
        public IActionResult ElencoPartecipazioni()
        {
            // query mista in cui uso entrambe le sintassi
            var query = (from c in _context.Corsis
                         join e in _context.Edizionis
                         on c.Idcorso equals e.Idcorso
                         join p in _context.Partecipazionis
                         on e.Idedizione equals p.Idedizione
                         join pa in _context.Partecipantis
                         on p.Idpartecipante equals pa.Idpartecipante
                         select new
                         {
                             e.Idedizione,
                             e.DataInizio,
                             c.Titolo,
                             e.Luogo,
                             c.Durata,
                             pa.Cognome,
                             pa.Residenza,
                             p.Giudizio,
                             p.IdPartecipazione
                         }).OrderBy(p => p.Idedizione); // query combinata

            ViewBag.Elenco = query.ToList();
            return View();
        }

        //public IActionResult NuovaPartecipazione()
        //{
        //    ViewBag.NuovoId = _context.Partecipazionis.Max(c => c.IdPartecipazione) + 1;
        //    ViewData["Idcorso"] = new SelectList(_context.Corsis, "Idcorso", "Titolo");
        //    return View();
        //}

        public IActionResult EditPartecipazione(int? id)
        {


            var partecipazione = _context.Partecipazionis.Find(id);
            return View(partecipazione);
        }


        [HttpPost]

        public IActionResult EditPartecipazione(int id, int idedizione, int idpartecipante, string giudizio)
        {

            var updatedRecord = new Partecipazioni();
            updatedRecord.IdPartecipazione = id;
            updatedRecord.Idedizione = idedizione;
            updatedRecord.Idpartecipante = idpartecipante;
            updatedRecord.Giudizio = giudizio;

            _context.Partecipazionis.Update(updatedRecord);
            _context.SaveChanges();

            return RedirectToAction("ElencoPartecipazioni");

        }

        public IActionResult Delete(int? id)
        {
            /* METODO ALTERNATIVO
			var record = _context.Partecipazionis.Find(id);
            var edizione= _context.Ediziones.Find(record.IdEdizione);
            var corso = _context.Corsis.Find(y.IdCorso);

            ViewBag.titolo = corso.Titolo;
			 */

            //METODO CON MODEL PERSONALIZZATO
            var query = from c in _context.Corsis
                        join e in _context.Edizionis
                        on c.Idcorso equals e.Idcorso
                        join p in _context.Partecipazionis
                        on e.Idedizione equals p.Idedizione
                        join pa in _context.Partecipantis
                        on p.Idpartecipante equals pa.Idpartecipante
                        where p.IdPartecipazione == id
                        select new DeletePartecipation
                        {
                            Id = p.IdPartecipazione,
                            IdEdizione = p.Idedizione,
                            NomePartecipante = pa.Nome,
                            CognomePartecipante = pa.Cognome,
                            NomeCorso = c.Titolo,
                            Luogo = e.Luogo,
                            Durata = c.Durata,
                        };


            var viewModel = query.FirstOrDefault();

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePartecipazione(int id)
        {
            var partecipazione = _context.Partecipazionis.Find(id);

            _context.Partecipazionis.Remove(partecipazione);


            _context.SaveChangesAsync();
            return RedirectToAction("ElencoPartecipazioni");
        }
        #endregion

        #region ISCRIVITI
        public IActionResult Iscriviti(int? id)
        {
            var nomeCorso = (from c in _context.Corsis
                             join e in _context.Edizionis
                             on c.Idcorso equals e.Idcorso
                             where e.Idedizione == id
                             select c.Titolo).FirstOrDefault();

            ViewData["Idpartecipazione"] = _context.Partecipazionis.Max(c => c.IdPartecipazione) + 1;
            ViewData["Idedizione"] = id;
            ViewData["NomeCorso"] = nomeCorso;
            ViewData["Idpartecipante"] = new SelectList(_context.Partecipantis, "Idpartecipante", "Cognome");
            return View();
        }

        [HttpPost]

        public IActionResult Iscriviti(int idpartecipazione, int idedizione, int idpartecipante)
        {
            var newRecord = new Partecipazioni();
            newRecord.IdPartecipazione = idpartecipazione;
            newRecord.Idedizione = idedizione;
            newRecord.Idpartecipante = idpartecipante;

            _context.Partecipazionis.Add(newRecord);
            _context.SaveChanges();

            return RedirectToAction("ElencoPartecipazioni");
        }
        #endregion

        #region ELENCHI FILTRATI
        public IActionResult ElencoFiltratoSemplice()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ElencoFiltratoSemplice(string citta)
        {
            ViewBag.Citta = citta;
            return View(_context.Partecipantis.Where(c => c.Residenza == citta).ToList());
        }

        public IActionResult ElencoFiltratoCorsi()
        {
            var query = from c in _context.Corsis
                        join e in _context.Edizionis
                        on c.Idcorso equals e.Idcorso
                        join p in _context.Partecipazionis
                        on e.Idedizione equals p.Idedizione
                        join pa in _context.Partecipantis
                        on p.Idpartecipante equals pa.Idpartecipante
                        select new
                        {
                            e.Idedizione,
                            e.DataInizio,
                            e.Luogo,
                            c.Titolo,
                            c.Durata,
                            pa.Cognome,
                            pa.Residenza,
                            p.Giudizio,
                            p.IdPartecipazione
                        };
            ViewBag.Elenco = query.ToList();
            //ViewBag.ElencoFiltrato = Enumerable.Empty<string>()  se avessi voluto lasciare la view vuota al caricamento, e non avessi voluto scrivere
            // if(ViewBag.ElencoFiltrato != null) nella view, avrei dovuto passargli l'elenco vuoto in questo modo
            return View();
        }
        [HttpPost]
        public IActionResult ElencoFiltratoCorsi(string corso)
        {
            var query = from c in _context.Corsis
                        join e in _context.Edizionis
                        on c.Idcorso equals e.Idcorso
                        join p in _context.Partecipazionis
                        on e.Idedizione equals p.Idedizione
                        join pa in _context.Partecipantis
                        on p.Idpartecipante equals pa.Idpartecipante
                        where c.Titolo.Contains(corso)
                        select new
                        {
                            e.Idedizione,
                            e.DataInizio,
                            e.Luogo,
                            c.Titolo,
                            c.Durata,
                            pa.Cognome,
                            pa.Residenza,
                            p.Giudizio,
                            p.IdPartecipazione
                        };
            ViewBag.Corso = corso;
            ViewBag.Elenco = query.ToList();
            return View();
        }

        public IActionResult ElencoPartecipazioniCombo(string corso)
        {
            ViewBag.Titoli = new SelectList(_context.Corsis.Select(t => t.Titolo).ToList(), corso);

            var query = from c in _context.Corsis
                        join e in _context.Edizionis
                        on c.Idcorso equals e.Idcorso
                        join p in _context.Partecipazionis
                        on e.Idedizione equals p.Idedizione
                        join pa in _context.Partecipantis
                        on p.Idpartecipante equals pa.Idpartecipante
                        where c.Titolo == corso
                        select new
                        {
                            e.Idedizione,
                            e.DataInizio,
                            e.Luogo,
                            c.Titolo,
                            c.Durata,
                            pa.Cognome,
                            pa.Residenza,
                            p.Giudizio,
                            p.IdPartecipazione
                        };

            ViewBag.Elenco = query.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult ElencoPartecipazioniComboExtra()
        {
            ViewBag.Titoli = _context.Corsis.Select(t => new SelectListItem
            {
                Value = t.Titolo,
                Text = t.Titolo
            }).ToList();

            string titoliJson = JsonConvert.SerializeObject(ViewBag.Titoli);
            HttpContext.Session.SetString("Titoli", titoliJson);
            ViewBag.Elenco = Enumerable.Empty<string>();

            return View();
        }

        [HttpPost]
        public IActionResult ElencoPartecipazioniComboExtra(string corso)
        {
            var titoliJson = HttpContext.Session.GetString("Titoli");

            if (titoliJson != null)
            {
                var titoli = JsonConvert.DeserializeObject<List<SelectListItem>>(titoliJson);
                var selectedTitolo = corso;
                foreach (var titolo in titoli)
                {
                    if (titolo.Value == selectedTitolo)
                    {
                        titolo.Selected = true;
                        break;
                    }
                }

                ViewBag.Titoli = titoli;
            }
            else
            {
                // ricaricare la lista dal Database
                //ViewBag.Titoli = new SelectList(_context.Corsis.Select(t => t.Titolo).ToList(), corso);
            }

            ViewBag.Elenco = (from c in _context.Corsis
                              join e in _context.Edizionis on c.Idcorso equals e.Idcorso
                              join p in _context.Partecipazionis on e.Idedizione equals p.Idedizione
                              join pa in _context.Partecipantis on p.Idpartecipante equals pa.Idpartecipante
                              select new
                              {
                                  e.Idedizione,
                                  e.DataInizio,
                                  e.Luogo,
                                  c.Titolo,
                                  c.Durata,
                                  pa.Cognome,
                                  pa.Residenza,
                                  p.Giudizio,
                                  p.IdPartecipazione
                              }).Where(c => c.Titolo == corso).ToList();

            return View();
        }

        // Usando questo metodo(che è il più efficente), la pagina non viene riaggiornata
        // e la query per filtrare i corsi viene eseguita solo al caricamento della pagina in get
        // come quando usiamo la session
        public IActionResult PartecipazioniAjax()
        {
            ViewBag.Titoli = new SelectList(_context.Corsis.Select(t => t.Titolo).ToList());
            return View();
        }
        [HttpPost]
        public IActionResult PartecipazioniAjax(string corso)
        {
            ViewBag.Elenco = (from c in _context.Corsis
                              join e in _context.Edizionis on c.Idcorso equals e.Idcorso
                              join p in _context.Partecipazionis on e.Idedizione equals p.Idedizione
                              join pa in _context.Partecipantis on p.Idpartecipante equals pa.Idpartecipante
                              select new
                              {
                                  e.Idedizione,
                                  e.DataInizio,
                                  e.Luogo,
                                  c.Titolo,
                                  c.Durata,
                                  pa.Cognome,
                                  pa.Residenza,
                                  p.Giudizio,
                                  p.IdPartecipazione
                              }).Where(c => c.Titolo == corso).ToList();

            return PartialView("_TabellaPartecipazioni");
        }
        #endregion

    }
}
