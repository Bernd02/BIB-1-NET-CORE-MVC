using BIBData.Models;
using Microsoft.EntityFrameworkCore;

namespace BIBData.Repositories
{
	public class SQLLenerRepository : ILenerRepository
	{
		private readonly BIBDbContext _context;


		// --------------------------------------------------
		public SQLLenerRepository(BIBDbContext context)
		{
			_context = context;
		}


		// --------------------------------------------------
		public Lener? Get(int id)
		{
			return _context.Leners.Find(id);
		}

		public IEnumerable<Lener> GetAll()
		{
			return _context.Leners.AsNoTracking();
		}
	}
}