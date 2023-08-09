using BIBData.Models;
using Microsoft.EntityFrameworkCore;

namespace BIBData.Repositories
{
	public class SQLUitleenobjectRepository : IUitleenobjectRepository
	{
		private readonly BIBDbContext _context;


		// --------------------------------------------------
		public SQLUitleenobjectRepository(BIBDbContext context)
		{
			_context = context;
		}


		// --------------------------------------------------
		public Uitleenobject? Get(int id)
		{
			return _context.Uitleenobjecten.Find(id);
		}

		public IEnumerable<Uitleenobject> GetAll()
		{
			return _context.Uitleenobjecten.AsNoTracking();
		}

		public Boek? GetBoek(int id)
		{
			return _context.Boeken.Find(id);
		}

		public Device? GetDevice(int id)
		{
			return _context.Devices
				.Include(d => d.Operatingsysteem)
				.FirstOrDefault(d => d.Id == id);
		}

		public void SetStatus(int uitleenobjectId, Status status)
		{
			var item = _context.Uitleenobjecten.Find(uitleenobjectId);
			if (item != null) {
				item.Status = status;
				_context.SaveChanges();
			}
		}
	}
}