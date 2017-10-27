using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Iam.Payroll.Common;
using Iam.Payroll.Data.Models;


namespace Iam.Payroll.Data
{
    public class PersonRepository : IPersonRepository
    {
        readonly RepositoryEF<Person> repo;
        readonly IamDatabase db;

        public PersonRepository(IamDatabase db)
        {
            this.db = db;
            this.repo = new RepositoryEF<Person>(db);
        }

        public List<PersonEx> GetAll(string where)
        {
            var query = db.People.AsQueryable();

            //if (!string.IsNullOrEmpty(where))
            //{
            //    query = query.Where(d => d.Name == where).AsQueryable();
            //}

            query.OrderBy("Id Asc");

            return Transform(query);
        }

        public List<PersonEx> GetPaged(string where = "", int? DepartmentId = null, string orderby = "", int PageNo = 1, int PageSize = 10)
        {
            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("FirstName.Contains(\"" + where + "\") || LastName.Contains(\"" + where + "\") || Gender.Contains(\"" + where + "\") || Department.Name.Contains(\"" + where + "\")");
            }

            if (DepartmentId != null)
            {
                aWhere.Add("DepartmentId.Equals(" + where + ")");
            }

            if (aWhere.Count > 0)
            {
                sWhere = string.Join("&&", aWhere);
            }

            var r = "" != sWhere ? repo.Query().Where(sWhere).OrderBy(orderby) : repo.Query().OrderBy(orderby);

            if (PageNo == 0 || PageSize == 0)
            {
                return Transform(r);
            }
            else
            {
                return Transform(r.Skip((PageNo - 1) * PageSize).Take(PageSize));
            }

        }

        public int Count(string where, int? DepartmentId)
        {
            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("FirstName.Contains(\"" + where + "\") || LastName.Contains(\"" + where + "\") || Gender.Contains(\"" + where + "\")");
            }

            if (DepartmentId != null)
            {
                aWhere.Add("DepartmentId.Equals(" + where + ")");
            }

            if (aWhere.Count > 0)
            {
                sWhere = string.Join("&&", aWhere);
            }

            var r = "" != sWhere ? repo.Query().Where(sWhere).Count() : repo.Query().Count();

            return r;
        }



        //REPOSITORYEF CRUD

        public PersonEx Create()
        {
            return new PersonEx();
        }

        public PersonEx Add(PersonEx item)
        {
            var g = Transform(item);
            var person = repo.Add(g);
            repo.SaveChanges();
            item.Id = g.Id;

            return Transform(person);
        }

        public PersonEx Get(int Id)
        {
            var person = repo.Query().Where(b => b.Id == Id).FirstOrDefault();
            return Transform(person);
        }

        public PersonEx Update(PersonEx item)
        {
            var g = Transform(item);
            var person = repo.Update(g);
            repo.SaveChanges();

            return Transform(person);
        }

        public void Remove(PersonEx item)
        {
            var g = Transform(item);
            repo.Remove(g);
            repo.SaveChanges();
        }

        public void Remove(int id)
        {
            repo.Remove(id);
            repo.SaveChanges();
        }

        public void Remove(string id)
        {
            repo.Remove(int.Parse(id));
            repo.SaveChanges();
        }


        #region Exchange Object
        private List<PersonEx> Transform(IQueryable<Person> n)
        {
            List<PersonEx> v = new List<PersonEx>();
            foreach (var r in n)
            {
                v.Add(Transform(r));
            }

            return v;
        }

        private PersonEx Transform(Person n)
        {
            if (n == null)
            {
                return null;
            }

            var r = new PersonEx();
            r.Id = n.Id;
            r.FirstName = n.FirstName;
            r.LastName = n.LastName;
            r.Gender = n.Gender;
            r.DepartmentId = n.Department.Id;
            r.DepartmentName = n.Department.Name;

            return r;
        }

        private Person Transform(PersonEx n)
        {
            Person r;
            if (n.Id == 0)
            {
                r = db.People.Create();
            }
            else
            {
                r = db.People.Where(p => p.Id == n.Id).FirstOrDefault();

            }
            r.Id = n.Id;
            r.FirstName = n.FirstName;
            r.LastName = n.LastName;
            r.Gender = n.Gender;
            r.DepartmentId = n.DepartmentId;
    



            return r;
        }


    }
    #endregion 
}
