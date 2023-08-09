using BIBData.Models;
using BIBServices;
using BIBWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;

namespace BIBWeb.Controllers
{
	public class UitleenobjectController : Controller
	{
		private readonly UitleenobjectService _uitleenobjectService;
		private readonly LenerService _lenerService;
		private readonly UitleeningService _uitleeningService;
		private readonly ReserveringService _reserveringService;


		// --------------------------------------------------
		public UitleenobjectController(
			UitleenobjectService uitleenobjectService,
			LenerService lenerService,
			UitleeningService uitleeningService,
			ReserveringService serveringService)
		{
			_uitleenobjectService = uitleenobjectService;
			_lenerService = lenerService;
			_uitleeningService = uitleeningService;
			_reserveringService = serveringService;
		}


		// --------------------------------------------------
		public IActionResult Index()
		{
			var objecten = _uitleenobjectService.GetAllUilteenobjecten();
			var objectenLijst = objecten.Select(
				item => new UitleenobjectDetailViewModel {
					Id = item.Id,
					Naam = item.Naam,
					Jaar = item.Jaar,
					Kostprijs = item.Kostprijs,
					Status = item.Status,
					Details = _uitleenobjectService.GetDetails(item.Id),
					Type = _uitleenobjectService.GetUitleenobjectType(item.Id)
				});
			return View(objectenLijst);
		}


		public IActionResult Detail(int id)
		{
			UitleenobjectDetailViewModel model = null!;
			Uitleenobject? item = _uitleenobjectService?.GetUitleenobject(id);
			if (item != null) {
				model = new UitleenobjectDetailViewModel {
					Id = item.Id,
					Naam = item.Naam,
					Jaar = item.Jaar,
					Kostprijs = item.Kostprijs,
					Status = item.Status,
					ImageUrl = item.ImageUrl,
					Details = _uitleenobjectService.GetDetails(item.Id),
					Type = _uitleenobjectService.GetUitleenobjectType(item.Id),
					HuidigeUitlener = _uitleeningService.GetHuidigeUitlener(item.Id),
					EersetInWachtlijst = _reserveringService.GetEersteLenerOpReserveringsLijst(item.Id),
				};
			}
			else {
				ViewBag.ErrorMessage = $"Geen info gevonden voor object met id {id}";
			}
			return View(model);
		}


		// --------------------------------------------------
		[HttpGet]
		public IActionResult Uitlenen(int id)
		{
			var item = _uitleenobjectService.GetUitleenobject(id);
			if (item != null) {
				var lenerSelectList = new List<SelectListItem>();
				foreach (var lener in _lenerService.GetAllLeners()) {
					lenerSelectList.Add(
						new SelectListItem {
							Text = $"{lener.Voornaam} {lener.Familienaam}",
							Value = lener.Id.ToString()
						});
				}
				var model = new UitleenViewModel {
					ItemId = id,
					ImageUrl = item.ImageUrl,
					Naam = item.Naam,
					Leners = lenerSelectList
				};
				return View(model);
			}
			else {
				return RedirectToAction(nameof(Index));
			}
		}


		[HttpPost]
		public IActionResult UitleningRegisteren(int itemId, int GekozenLenerId)
		{
			_uitleeningService.UitleningRegisteren(itemId, GekozenLenerId);
			return RedirectToAction(nameof(Detail), new { id = itemId });
		}


		// --------------------------------------------------
		[HttpPost]
		public IActionResult Terugbrengen(int id)
		{
			_uitleeningService.ItemTerugbrengen(id);
			return RedirectToAction(nameof(Index));
		}


		// --------------------------------------------------
		[HttpGet]
		public IActionResult Reserveren(int id)
		{
			var item = _uitleenobjectService.GetUitleenobject(id);
			if (item != null) {
				var reserveringenLijst = _reserveringService.GetReserveringenVoorUitleenobject(id);
				var lenerSelectList = new List<SelectListItem>();
				foreach (var lener in _lenerService.GetAllLeners()) {
					lenerSelectList.Add(
						new SelectListItem {
							Text = $"{lener.Voornaam} {lener.Familienaam}",
							Value = lener.Id.ToString()
						});
				}

				var model = new ReserveerViewModel {
					ItemId = item.Id,
					ImageUrl = item.ImageUrl,
					Naam = item.Naam,
					Reserveringen = reserveringenLijst,
					Leners = lenerSelectList
				};
				return View(model);
			}
			else {
				return RedirectToAction(nameof(Index));
			}
		}


		[HttpPost]
		public IActionResult ItemReserveren(int itemId, int gekozenLenerId)
		{
			_reserveringService.ItemReserveren(itemId, gekozenLenerId);
			return RedirectToAction(nameof(Detail), new { id = itemId });
		}


		// --------------------------------------------------
		public IActionResult ReserveringOphalen(int itemId, int lenerId)
		{
			_reserveringService.ReserveringVerwijderen(itemId, lenerId);
			_uitleeningService.UitleningRegisteren(itemId, lenerId);
			return RedirectToAction(nameof(Detail), new { id = itemId });
		}
	}
}