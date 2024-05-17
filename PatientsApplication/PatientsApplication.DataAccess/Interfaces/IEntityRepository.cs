using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;

namespace PatientsApplication.DataAccess.Interfaces
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> Get();

        public IEnumerable<T> GetRange(Guid[] recordIds);

        public T GetById(Guid recordId);

        public Task<Guid> Create(T record);

        public Task<IEnumerable<Guid>> CreateRange(IEnumerable<T> records);

        public Task<int> Delete(T record);

        public Task<int> DeleteRange(IEnumerable<T> records);

        public Task<int> Update(T record);

        public Task<int> UpdateRange(IEnumerable<T> records);
    }

}
