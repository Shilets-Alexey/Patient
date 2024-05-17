using PatientsApplication.DataAccess.Entities;

namespace PatientsApplication.DataAccess.Interfaces
{
    public interface ILookupRepository
    {
        public IEnumerable<TLookup> Get<TLookup>() where TLookup : BaseLookup;
    }
}
