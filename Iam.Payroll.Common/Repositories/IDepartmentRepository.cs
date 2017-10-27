using System.Collections.Generic;

namespace Iam.Payroll.Common
{
    public interface IDepartmentRepository : IRepository<DepartmentEx>
    {
        List<DepartmentEx> GetAll(string where);
        List<DepartmentEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10);
        DepartmentEx Get(int Id);
        int Count(string where);
        //void Save(DepartmentEx item);
        //void Delete(DepartmentEx item);


    }
}
