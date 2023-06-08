using Microsoft.AspNetCore.Mvc;

namespace ELIS_MVC_Core.Controllers
{
	public class StatoController : Controller
	{
		public IActionResult Partenza()
		{
			string valore = Request.Query["txt1"].ToString();
			
			if(valore != null)
			{
				HttpContext.Session.SetString("sa",valore);
			}

			return View();
		}

		public IActionResult Arrivo()
		{
			return View();
		}
	}
}
