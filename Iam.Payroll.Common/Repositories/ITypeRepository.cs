using System.Collections.Generic;

namespace Iam.Payroll.Common
{
    public interface ITypeRepository : IRepository<TypeEx>
    {
        List<TypeEx> GetAll(string where);
        List<TypeEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10);
        TypeEx Get(int Id);
        int Count(string where);
    }
}
