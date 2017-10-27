using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Iam.Payroll.Common;
using Iam.Payroll.Data.Models;

namespace Iam.Payroll.Services
{
    public class TypeService
    {
        readonly ITypeRepository repo;

        public TypeService(ITypeRepository repo)
        {
            this.repo = repo;
        }

        public List<TypeEx> GetAll(string where = "")
        {
            return repo.GetAll(where);
        }

        public List<TypeEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10)
        {
            return repo.GetPaged(where, orderby, PageNo, PageSize);
        }

        public int Count(string where)
        {
            return repo.Count(where);
        }


        public TypeEx Get(int Id)
        {
            return repo.Get(Id);
        }

        public TypeEx Add(TypeEx item)
        {
            return repo.Add(item);
        }


        public TypeEx Update(TypeEx item)
        {
            return repo.Update(item);
        }


        public void Remove(TypeEx item)
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
