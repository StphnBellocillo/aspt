using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Iam.Payroll.Common;

namespace Iam.Payroll.Services
{
    public class AttendanceService
    {
        readonly IAttendanceRepository repo;

        public AttendanceService(IAttendanceRepository repo)
        {
            this.repo = repo;
        }
        public List<AttendanceEx> GetAll(string where = "")
        {
            return repo.GetAll(where);
        }

        public List<AttendanceEx> GetPaged(string where, int PageNo, int PageSize, int? PersonId, string orderby, string orderField, DateTime? TimeIn, DateTime? TimeOut)
        {
            return repo.GetPaged(where, PageNo, PageSize, PersonId, orderby, orderField, TimeIn, TimeOut);
        }

        public int Count(string where, int? PersonId = null, DateTime? TimeIn = null, DateTime? TimeOut = null)
        {
            return repo.Count(where, PersonId, TimeIn, TimeOut);
        }
        public AttendanceEx Get(int Id)
        {
            return repo.Get(Id);
        }

        public AttendanceEx Add(AttendanceEx item)
        {
            return repo.Add(item);
        }


        public AttendanceEx Update(AttendanceEx item)
        {
            return repo.Update(item);
        }

        public void Remove(AttendanceEx item)
        {
            repo.Remove(item);
        }

        //REPOSITORYEF
        public void Create()
        {
            repo.Create();
        }


    
    }
}
