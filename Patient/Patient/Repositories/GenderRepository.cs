using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PatientsApplication.DataAccess.Repositories
{
    public class GenderRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public GenderRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Gender> Get()
        {
            return _applicationDbContext.Genders.ToList();
        }
    }
}
