using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Iam.Payroll.Common;
using Iam.Payroll.Data.Models;

namespace Iam.Payroll.Services
{
    public class DepartmentService
    {
        readonly IDepartmentRepository repo;

        public DepartmentService(IDepartmentRepository repo)
        {
            this.repo = repo;
        }

        public List<DepartmentEx> GetAll(string where = "")
        {
            return repo.GetAll(where);
        }


        public List<DepartmentEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10)
        {
            return repo.GetPaged(where, orderby, PageNo, PageSize);
        }

        public int Count(string where)
        {
            return repo.Count(where);
        }


        public DepartmentEx Get(int Id)
        {
            return repo.Get(Id);
        }

        public DepartmentEx Add(DepartmentEx item)
        {
            return repo.Add(item);
        }


        public DepartmentEx Update(DepartmentEx item)
        {
            return repo.Update(item);
        }


        public void Remove(DepartmentEx item)
        {
            repo.Remove(item);
        }

        //REPOSITORYEF
        public void Create()
        {
            repo.Create();
        }

        public List<PersonEx> GetPaged(string where, int? departmentId, int pageNo, int pageSize, string orderField, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}