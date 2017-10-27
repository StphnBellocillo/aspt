using System;
using System.Collections.Generic;

namespace Iam.Payroll.Common
{
    public interface IAttendanceRepository : IRepository<AttendanceEx>
    {
        List<AttendanceEx> GetAll(string where);
        List<AttendanceEx> GetPaged(string where, int PageNo, int PageSize, int? PersonId, string orderby, string orderField, DateTime? TimeIn = null, DateTime? TimeOut = null);
        int Count(string where, int? PersonId, DateTime? TimeIn, DateTime? Timeout);
        AttendanceEx Get(int Id);
    }
}
