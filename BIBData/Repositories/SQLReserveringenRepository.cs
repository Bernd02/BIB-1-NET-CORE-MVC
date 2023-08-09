using BIBData.Models;
using Microsoft.EntityFrameworkCore;

namespace BIBData.Repositories
{
	public class SQLReserveringenRepository : IReserveringRepository
	{
		private readonly BIBDbContext _context;


		// --------------------------------------------------
		public SQLReserveringenRepository(BIBDbContext context)
		{
			_context = context;
		}


		// --------------------------------------------------
		public void Add(Reservering reservering)
		{
			_context.Reserveringen.Add(reservering);
			_context.SaveChanges();
		}


		public IEnumerable<Reservering> GetReserveringenVoorUitleenobject(int uitleenobjectId)
		{
			var reserveringen = _context.Reserveringen
				.Include(r => r.Lener)
				.Where(r => r.Uitleenobject.Id == uitleenobjectId)
				.OrderBy(r => r.GereserveerdOp);
			return reserveringen;
		}


		public bool IsGereserveerd(int uitleenobjectId)
		{
			var reservering = _context.Reserveringen
				.Include(r => r.Uitleenobject)
				.FirstOrDefault(r => r.Uitleenobject.Id == uitleenobjectId);

			return reservering != null;
		}


		public void VerwijderReservering(int itemId, int lenerId)
		{
			var reservering = _context.Reserveringen
				.Include(r => r.Uitleenobject)
				.Include(r => r.Lener)
				.Where(r => r.Uitleenobject.Id == itemId)
				.Where(r => r.Lener.Id == lenerId)
				.FirstOrDefault();

			if (reservering != null) {
				_context.Remove(reservering);
				_context.SaveChanges();
			}
		}


		public IEnumerable<Reservering> GetReserveringenVanLener(int lenerId)
		{
			var reserveringen = _context.Reserveringen
				.Include(r => r.Lener)
				.Include(r => r.Uitleenobject)
				.Where(r => r.Lener.Id == lenerId)
				.OrderBy(r => r.GereserveerdOp);
			return reserveringen;
		}
	}
}