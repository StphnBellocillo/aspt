using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Iam.Payroll.Common
{
    public interface IPersonRepository : IRepository<PersonEx>
    {
        List<PersonEx> GetAll(string where);
        List<PersonEx> GetPaged(string where = "", int? DepartmentId = null, string orderby = "", int PageNo = 1, int PageSize = 10);
        int Count(string where, int? DepartmentId);
        PersonEx Get(int Id);
        //void Save(PersonEx person);
        //void Delete(PersonEx person);
    }
}
