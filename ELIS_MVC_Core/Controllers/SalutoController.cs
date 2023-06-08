using Microsoft.AspNetCore.Mvc;

namespace ELIS_MVC_Core.Controllers
{
	public class SalutoController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
