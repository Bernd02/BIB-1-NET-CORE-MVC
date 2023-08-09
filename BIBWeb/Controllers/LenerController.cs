using BIBData.Models;
using BIBServices;
using BIBWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BIBWeb.Controllers
{
	public class LenerController : Controller
	{
		private readonly LenerService _lenerService;
		private readonly UitleeningService _uitleeningService;
		private readonly ReserveringService _reserveringService;


		// --------------------------------------------------
		public LenerController(LenerService lenerService, UitleeningService uitleeningService, ReserveringService reserveringService)
		{
			_lenerService = lenerService;
			_uitleeningService = uitleeningService;
			_reserveringService = reserveringService;
		}


		// --------------------------------------------------
		public IActionResult Index()
		{
			return View(_lenerService.GetAllLeners());
		}


		public IActionResult Detail(int id)
		{
			LenerDetailViewModel? model = null;
			Lener? lener = _lenerService.GetLener(id);
			if (lener == null) {
				ViewBag.ErrorMessage = $"Geen info gevonden voor lener met id {id}";
			}
			else {
				model = new LenerDetailViewModel {
					Lener = lener,
					OpenstaandeUitleningen = _uitleeningService.GetOpenstaandeUitleningenVanLener(id),
					Reserveringen = _reserveringService.GetReserveringenVanLener(id)
				};
			}
			return View(model);
		}
	}
}