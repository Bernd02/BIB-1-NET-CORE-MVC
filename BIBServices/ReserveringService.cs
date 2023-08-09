using BIBData.Models;
using BIBData.Repositories;
using System.Security.AccessControl;

namespace BIBServices
{
	public class ReserveringService
	{
		private readonly IReserveringRepository _reserveringRepository;
		private readonly IUitleenobjectRepository _uitleenobjectRepository;
		private readonly ILenerRepository _lenerRepository;


		// --------------------------------------------------
		public ReserveringService(IReserveringRepository reserveringRepository, IUitleenobjectRepository uitleenobjectRepository, ILenerRepository lenerRepository)
		{
			_reserveringRepository = reserveringRepository;
			_uitleenobjectRepository = uitleenobjectRepository;
			_lenerRepository = lenerRepository;
		}


		// --------------------------------------------------
		public IEnumerable<Reservering> GetReserveringenVoorUitleenobject(int id)
		{
			return _reserveringRepository.GetReserveringenVoorUitleenobject(id);
		}


		public void ItemReserveren(int itemId, int lenerId)
		{
			var item = _uitleenobjectRepository.Get(itemId);
			var lener = _lenerRepository.Get(lenerId);
			if (item != null && lener != null) {
				_reserveringRepository.Add(new Reservering {
					Uitleenobject = item,
					Lener = lener,
					GereserveerdOp = DateTime.Now
				});
			}
		}


		public Lener? GetEersteLenerOpReserveringsLijst(int uitleenObjectId)
		{
			return _reserveringRepository
				.GetReserveringenVoorUitleenobject(uitleenObjectId)
				.FirstOrDefault()?
				.Lener;
		}


		public void ReserveringVerwijderen(int itemId, int lenerId)
		{
			_reserveringRepository.VerwijderReservering(itemId, lenerId);
		}


		public IEnumerable<Reservering> GetReserveringenVanLener(int lenerId)
		{
			return _reserveringRepository.GetReserveringenVanLener(lenerId);
		}
	}
}