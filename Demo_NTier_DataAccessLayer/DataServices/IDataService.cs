using Demo_NTier_DomainLayer;
using System.Collections.Generic;

namespace Demo_NTier_DataAccessLayer
{
    public interface IDataService
    {
        IEnumerable<Person> ReadAll(out DalErrorCode statusCode);
        void WriteAll(IEnumerable<Person> persons, out DalErrorCode statusCode);
    }
}
