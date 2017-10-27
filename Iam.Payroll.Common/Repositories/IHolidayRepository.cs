using System;
using System.Collections.Generic;

namespace Iam.Payroll.Common
{
    public interface IHolidayRepository  : IRepository<HolidayEx>
    {
        List<HolidayEx> GetAll(string where);
        List<HolidayEx> GetPaged(string where, int? typeId, string orderby, int PageNo, int PageSize, string orderField, DateTime? calendar = null);
        int Count(string where, int? typeId, DateTime? calendar = null);
        HolidayEx Get(int Id);
    }
}
