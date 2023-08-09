using BIBData.Models;
using BIBData.Repositories;

namespace BIBServices
{
	public class LenerService
	{
		private readonly ILenerRepository _lenerRepository;


		// --------------------------------------------------
		public LenerService(ILenerRepository lenerRepository)
		{
			_lenerRepository = lenerRepository;
		}


		// --------------------------------------------------
		public IEnumerable<Lener> GetAllLeners()
		{
			return _lenerRepository.GetAll();
		}


		public Lener? GetLener(int id)
		{
			return _lenerRepository.Get(id);
		}
	}
}