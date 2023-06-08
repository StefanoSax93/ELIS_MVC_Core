using Microsoft.AspNetCore.Mvc;

namespace ELIS_MVC_Core.Controllers
{
	public class CalcolatriceController : Controller
	{
		public IActionResult Calcolatrici()
		{
			return View();
		}
		[HttpPost]
		[ActionName("Calcolatrici")]
		public IActionResult CalcolatriciPost()
		{
			int num1 = 0;
			int num2 = 0;
			/*Try parse se trova un numero lo converte, altrimenti restituisce null*/
			Int32.TryParse(HttpContext.Request.Form["txt7"], out num1);
			Int32.TryParse(HttpContext.Request.Form["txt8"], out num2);

			int result = num1 + num2;


			ViewData["numero1"] = num1;
			ViewData["numero2"] = num2;
			ViewBag.Risultato = result;

			return View();
		}
	}
}
