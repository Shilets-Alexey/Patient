using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;
using PatientsApplication.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PatientsApplication.DataAccess.Repositories
{
    public class LookUpRepository: ILookupRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public LookUpRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<TLookup> Get<TLookup>() where TLookup : BaseLookup
        {
            return _applicationDbContext.Set<TLookup>().ToList();
        }
    }
}
