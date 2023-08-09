using BIBData.Models;

namespace BIBData.Repositories
{
	public interface ILenerRepository
	{
		Lener? Get(int id);
		IEnumerable<Lener> GetAll();
	}


	// --------------------------------------------------
	public interface IUitleenobjectRepository
	{
		Uitleenobject? Get(int id);
		IEnumerable<Uitleenobject> GetAll();
		Boek? GetBoek(int id);
		Device? GetDevice(int id);
		void SetStatus(int uitleenobjectId, Status status);
	}


	// --------------------------------------------------
	public interface IUitleningRepository
	{
		Uitlening? Get(int uitleenId);
		IEnumerable<Uitlening> GetAll();
		void Add(Uitlening nieuweUitlening);
		void SetReturnDate(int uitleenobjectId, DateTime now);
		Uitlening? GetOpenstaandeUitleningVoorUitleenobject(int uitleenobjectId);
		IEnumerable<Uitlening> GetOpenstaandeUitleningenVanLener(int lenerId);
	}


	// --------------------------------------------------
	public interface IReserveringRepository
	{
		void Add(Reservering reservering);
		IEnumerable<Reservering> GetReserveringenVoorUitleenobject(int uitleenobjectId);
		bool IsGereserveerd(int uitleenobjectId);
		void VerwijderReservering(int itemId, int lenerId);
		IEnumerable<Reservering> GetReserveringenVanLener(int lenerId);
	}
}