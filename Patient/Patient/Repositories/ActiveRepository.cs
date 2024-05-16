using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PatientsApplication.DataAccess.Repositories
{
    public class ActiveRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public ActiveRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Active> Get()
        {
            return _applicationDbContext.Active.ToList();
        }
    }
}
