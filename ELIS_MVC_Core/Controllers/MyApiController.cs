using ELIS_MVC_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace ELIS_MVC_Core.Controllers
{
    public class MyApiController : Controller
    {
        private readonly CorsiContext _context;

        public MyApiController(CorsiContext context)
        {
            _context = context;
        }
        #region CORSI

        // CORSI
        public async Task<IActionResult> ElencoCorsi()
        {
            string url = "http://localhost:5279/api/Corsis";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Corsi>>(jsonData);

            return View(result);
        }

        public IActionResult ElencoCorsiFiltro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ElencoCorsiFiltro(int id)
        {
            string url = $"http://localhost:5279/api/Corsis/{id}";

            ViewBag.id = id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            Corsi result = JsonConvert.DeserializeObject<Corsi>(jsonData);

            return View(result);
        }

        public IActionResult JQueryWebApi()
        {
            return View();
        }

        #endregion

        #region PARTECIPANTI

        // PARTECIPANTI

        public async Task<IActionResult> ElencoPartecipanti()
        {
            string url = "http://localhost:5279/api/Partecipantis";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Partecipanti>>(jsonData);

            return View(result);
        }

		public async Task<IActionResult> EditPartecipante(int id)
		{
			string url = $"http://localhost:5279/api/Partecipantis/{id}";

			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync(url);
			string jsonData = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<Partecipanti>(jsonData);

			return View(result);
		}

		[HttpPost]
		public async Task<IActionResult> EditPartecipante(int id, Partecipanti partecipante)
		{
			string url = $"http://localhost:5279/api/Partecipantis/{id}";

			HttpClient client = new HttpClient();
			var nuovoRecord = new StringContent(JsonConvert.SerializeObject(partecipante), Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PutAsync(url, nuovoRecord);
			response.EnsureSuccessStatusCode(); 

			return RedirectToAction("ElencoPartecipanti");
		}

		public IActionResult ElencoPartecipantiFiltro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ElencoPartecipantiFiltro(int id)
        {
            string url = $"http://localhost:5279/api/Partecipantis/{id}";

            ViewBag.id = id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            Partecipanti result = JsonConvert.DeserializeObject<Partecipanti>(jsonData);

            return View(result);
        }

        public IActionResult JQueryWebApiPartecipanti()
        {
            return View();
        }

		/*
         public async Task<IActionResult> ModificaPartecipante(int id)
        {
            string url = $"http://localhost:5208/api/Partecipantes/{id}";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Partecipante>(jsonData);

            ViewBag.partecipante = result;

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> ModificaPartecipante(int id,string nome,string cognome,DateTime? data,string residenza,string studio )
        {
            string url = $"http://localhost:5208/api/Partecipantes/{id}";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Partecipante>(jsonData);

            if (nome != null)
            {
                result.Nome = nome;
            }
            else
            {
                result.Nome = result.Nome;

            }

            if (cognome != null)
            {
                result.Cognome = cognome;
            }
            else
            {
                result.Cognome = result.Cognome;

            }
            if (data != null)
            {
                result.DataNascita = data.Value;
            }
            else
            {
                result.DataNascita = result.DataNascita;

            }

            if (residenza != null)
            {
                result.Residenza = residenza;
            }
            else
            {
                result.Residenza = result.Residenza;

            }

            if (studio != null)
            {
                result.TitoloDiStudio = studio;
            }
            else
            {
                result.TitoloDiStudio = result.TitoloDiStudio;

            }

            var json = JsonConvert.SerializeObject(result);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var update = await client.PutAsync(url, content);


          
                return RedirectToAction("ElencoPartecipanti");
          


           
        }
         */

		public IActionResult NuovoPartecipante()
		{
            ViewBag.NuovoId = _context.Partecipantis.Max(p => p.Idpartecipante) + 1;
            return View();
		}

		#endregion

		#region API ESTERNA
		public async Task<IActionResult> WebApiEsterna()
        {
            string url = "https://webapicustomers.azurewebsites.net/api/Customers";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Customer>>(jsonData);

            return View(result);
        }
		#endregion

	}
}
