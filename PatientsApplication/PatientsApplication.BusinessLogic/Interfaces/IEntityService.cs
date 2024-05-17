using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;

namespace PatientsApplication.BusinessLogic.Interfaces
{
    public interface IEntityService<TEntityDto>
    {
        public ServiceResult<IEnumerable<TEntityDto>> Get();

        public ServiceResult<IEnumerable<TEntityDto>> GetRange(Guid[] recordIds);

        public ServiceResult<TEntityDto> GetById(Guid recordId);

        public Task<ServiceResult<Guid>> Create(TEntityDto record);

        public Task<ServiceResult<IEnumerable<Guid>>> CreateRange(TEntityDto[] records);

        public Task<ServiceResult<int>> Delete(Guid recordId);

        public Task<ServiceResult<int>> DeleteRange(Guid[] recordsIds);

        public Task<ServiceResult<int>> Update(TEntityDto record);

        public Task<ServiceResult<int>> UpdateRange(TEntityDto[] records);
    }
}
