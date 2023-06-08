using ELIS_MVC_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ELIS_MVC_Core.Controllers
{
    public class MyApiController : Controller
    {
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

        #endregion

        public async Task<IActionResult> WebApiEsterna()
        {
            string url = "https://webapicustomers.azurewebsites.net/api/Customers";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Customer>>(jsonData);

            return View(result);
        }

    }
}
