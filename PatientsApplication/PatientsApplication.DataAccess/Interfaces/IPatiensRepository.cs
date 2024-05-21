using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;

namespace PatientsApplication.DataAccess.Interfaces
{
    public interface IPatiensRepository : IEntityRepository<Patient> {

        public int Count(IEnumerable<Guid> patientsId);

        public int Count();

        public IEnumerable<Patient> GetByDate(Func<Patient, bool> filterMethod);
    }
}
