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
    public class DepartmentRepository : IDepartmentRepository
    {
        readonly RepositoryEF<Department> repo;
        readonly IamDatabase db;

        public DepartmentRepository(IamDatabase db)
        {
            this.db = db;
            this.repo = new RepositoryEF<Department>(db);
        }

        public List<DepartmentEx> GetAll(string where = "")
        {
            var query = db.Departments.AsQueryable();

            //if (!string.IsNullOrEmpty(where))
            //{
            //    query = query.Where(d => d.Name == where).AsQueryable();
            //}

            query.OrderBy("Id Asc");

            return Transform(query);
        }

        public List<DepartmentEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10)
        {

            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Name.Contains(\"" + where + "\")");
            }

            //if (DepartmentId != null)
            //{
            //    aWhere.Add("DepartmentId.Equals(" + where + ")");
            //}

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

        public int Count(string where)
        {

            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Name.Contains(\"" + where + "\")");
            }

            //if (DepartmentId != null)
            //{
            //    aWhere.Add("DepartmentId.Equals(" + where + ")");
            //}

            if (aWhere.Count > 0)
            {
                sWhere = string.Join("&&", aWhere);
            }

            var r = "" != sWhere ? repo.Query().Where(sWhere).Count() : repo.Query().Count();

            return r;
        }

        public DepartmentEx Create()
        {
            return new DepartmentEx();
        }

        public DepartmentEx Add(DepartmentEx item)
        {
            var g = Transform(item);
            var department = repo.Add(g);
            repo.SaveChanges();
            item.Id = g.Id;

            return Transform(department);
        }

        public DepartmentEx Update(DepartmentEx item)
        {
            var g = Transform(item);
            var department = repo.Update(g);
            repo.SaveChanges();

            return Transform(department);
        }

        public DepartmentEx Get(int Id)
        {
            var department = repo.Query().Where(b => b.Id == Id).FirstOrDefault();
            return Transform(department);
        }


        public void Remove(DepartmentEx item)
        {
            var r = Transform(item);
            repo.Remove(r);
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

        private List<DepartmentEx> Transform(IQueryable<Department> n)
        {
            List<DepartmentEx> v = new List<DepartmentEx>();
            foreach (var r in n)
            {
                v.Add(Transform(r));
            }

            return v;
        }
        private DepartmentEx Transform(Department n)
        {
            if (n == null)
            {
                return null;
            }

            var r = new DepartmentEx();
            r.Id = n.Id;
            r.Name = n.Name;

            return r;
        }

        private Department Transform(DepartmentEx n)
        {
            Department r;
            if (n.Id == 0)
            {
                r = db.Departments.Create();
            }
            else
            {
                r = db.Departments.Where(p => p.Id == n.Id).FirstOrDefault();

            }
            r.Id = n.Id;
            r.Name = n.Name;


            return r;
        }
    }
    #endregion 
}
