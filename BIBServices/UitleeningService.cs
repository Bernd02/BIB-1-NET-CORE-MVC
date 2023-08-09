using BIBData.Models;
using BIBData.Repositories;

namespace BIBServices
{
	public class UitleeningService
	{
		private readonly IUitleningRepository _uitleningRepository;
		private readonly ILenerRepository _lenenerRepository;
		private readonly IUitleenobjectRepository _uitleenobjectRepository;
		private readonly IReserveringRepository _reserveringRepository;


		// --------------------------------------------------
		public UitleeningService(
			IUitleningRepository uitleningRepository,
			ILenerRepository lenenerRepository,
			IUitleenobjectRepository uitleenobjectRepository,
			IReserveringRepository reserveringRepository)
		{
			_uitleningRepository = uitleningRepository;
			_lenenerRepository = lenenerRepository;
			_uitleenobjectRepository = uitleenobjectRepository;
			_reserveringRepository = reserveringRepository;
		}


		// --------------------------------------------------
		public void UitleningRegisteren(int uitleenobjectId, int lenerId)
		{
			Uitleenobject? item = _uitleenobjectRepository.Get(uitleenobjectId);
			Lener? lener = _lenenerRepository.Get(lenerId);
			if (item != null && lener != null) {
				item.Status = Status.Uitgeleend;
				_uitleningRepository.Add(
					new Uitlening {
						Uitleenobject = item,
						Lener = lener,
						Van = DateTime.Now,
						Tot = null
					});
			}
		}


		public void ItemTerugbrengen(int uitleenobjectId)
		{
			_uitleningRepository.SetReturnDate(uitleenobjectId, DateTime.Now);
			if (_reserveringRepository.IsGereserveerd(uitleenobjectId)) {
				_uitleenobjectRepository.SetStatus(uitleenobjectId, Status.Gereserveerd);
			}
			else {
				_uitleenobjectRepository.SetStatus(uitleenobjectId, Status.Beschikbaar);
			}
		}


		public string? GetHuidigeUitlener(int uitleenobjectId)
		{
			Uitlening? uitlening = _uitleningRepository.GetOpenstaandeUitleningVoorUitleenobject(uitleenobjectId);
			if (uitlening == null) return null;
			return $"{uitlening.Lener.Voornaam} {uitlening.Lener.Familienaam}";
		}


		public IEnumerable<Uitlening> GetOpenstaandeUitleningenVanLener(int lenerId)
		{
			return _uitleningRepository.GetOpenstaandeUitleningenVanLener(lenerId);
		}
	}
}