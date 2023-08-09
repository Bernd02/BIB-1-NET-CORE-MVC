using BIBData.Models;
using Microsoft.EntityFrameworkCore;

namespace BIBData.Repositories
{
	public class SQLUitleningRepository : IUitleningRepository
	{
		private readonly BIBDbContext _context;


		// --------------------------------------------------
		public SQLUitleningRepository(BIBDbContext context)
		{
			_context = context;
		}


		// --------------------------------------------------
		public void Add(Uitlening nieuweUitlening)
		{
			_context.Add(nieuweUitlening);
			_context.SaveChanges();
		}


		public Uitlening? Get(int uitleenId)
		{
			return _context.Uitleningen.Find(uitleenId);
		}


		public IEnumerable<Uitlening> GetAll()
		{
			return _context.Uitleningen;
		}

		public void SetReturnDate(int uitleenobjectId, DateTime now)
		{
			var uitlening = _context.Uitleningen
				.Where(u => u.Tot == null)
				.Where(u => u.Uitleenobject.Id == uitleenobjectId)
				.FirstOrDefault();

			if (uitlening != null) {
				uitlening.Tot = now;
				_context.SaveChanges();
			}
		}

		public Uitlening? GetOpenstaandeUitleningVoorUitleenobject(int uitleenobjectId)
		{
			var uitlening = _context.Uitleningen
				.Include(u => u.Uitleenobject)
				.Include(u => u.Lener)
				.Where(u => u.Tot == null)
				.FirstOrDefault(u => u.Uitleenobject.Id == uitleenobjectId);
			return uitlening;
		}

		public IEnumerable<Uitlening> GetOpenstaandeUitleningenVanLener(int lenerId)
		{
			var uitleningen = _context.Uitleningen
				.Include(u => u.Lener)
				.Include(u => u.Uitleenobject)
				.Where(u => u.Lener.Id == lenerId)
				.Where(u => u.Tot == null)
				.OrderBy(u => u.Van);
			return uitleningen;
		}
	}
}