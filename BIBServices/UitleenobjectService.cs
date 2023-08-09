using BIBData.Models;
using BIBData.Repositories;
using System.Net.Http.Headers;

namespace BIBServices
{
	public class UitleenobjectService
	{
		private readonly IUitleenobjectRepository _uitleenobjectRepository;


		// --------------------------------------------------
		public UitleenobjectService(IUitleenobjectRepository uitleenobjectRepository)
		{
			_uitleenobjectRepository = uitleenobjectRepository;
		}


		// --------------------------------------------------
		public IEnumerable<Uitleenobject> GetAllUilteenobjecten()
		{
			return _uitleenobjectRepository.GetAll();
		}


		public string GetUitleenobjectType(int id)
		{
			var uitleenobject = _uitleenobjectRepository.Get(id);
			return uitleenobject is Boek ? "Boek" : "Device";
		}


		public string GetDetails(int id)
		{
			if (GetUitleenobjectType(id) == "Boek") {
				var boek = _uitleenobjectRepository.GetBoek(id);
				if (boek != null) {
					return $"{boek.ISBN} ({boek.Auteur}, {boek.Aantalpaginas}p.)";
				}
				else {
					return $"Geen info gevonden over boek met id {id}";
				}
			}
			else {
				var device = _uitleenobjectRepository.GetDevice(id);
				if (device != null) {
					return $"{device.Operatingsysteem.Naam} - {device.Schermgrootte}\"";
				}
				else {
					return $"Geen info geconden over device met id {id}";
				}
			}
		}


		public Uitleenobject? GetUitleenobject(int id)
		{
			return _uitleenobjectRepository.Get(id);
		}
	}
}