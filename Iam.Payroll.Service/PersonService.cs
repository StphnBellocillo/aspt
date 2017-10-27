using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Iam.Payroll.Common;


namespace Iam.Payroll.Services
{
    public class PersonService
    {
        readonly IPersonRepository repo;

        public PersonService(IPersonRepository repo)
        {
            this.repo = repo;
        }

        public List<PersonEx> GetAll(string where)
        {
            return repo.GetAll(where);
        }


        public List<PersonEx> GetPaged(string where = "", int? DepartmentId = null, string orderby = "", int PageNo = 1, int PageSize = 10)
        {
            return repo.GetPaged(where, DepartmentId, orderby, PageNo, PageSize);
        }

        public int Count(string where, int? DepartmentId)
        {
            return repo.Count(where, DepartmentId);
        }

        public PersonEx Get(int Id)
        {
            return repo.Get(Id);
        }

        public PersonEx Add(PersonEx item)
        {
            return repo.Add(item);
        }


        public PersonEx Update(PersonEx item)
        {
            return repo.Update(item);
        }

        public void Remove(PersonEx item)
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