using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_NTier_DomainLayer;

namespace Demo_NTier_DataAccessLayer
{
    public interface IPersonRepository : IDisposable
    {
        IEnumerable<Person> GetAll(out DalErrorCode dalErrorCode);
        Person GetById(int id, out DalErrorCode dalErrorCode);
        void Insert(Person person, out DalErrorCode dalErrorCode);
        void Update(Person person, out DalErrorCode dalErrorCode);
        void Delete(int id, out DalErrorCode dalErrorCode);
    }
}
