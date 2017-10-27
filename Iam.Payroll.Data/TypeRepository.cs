using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Iam.Payroll.Common;
using Iam.Payroll.Data.Models;


namespace Iam.Payroll.Data
{
    public class TypeRepository : ITypeRepository
    {
        readonly RepositoryEF<Type> repo;
        readonly IamDatabase db;

        public TypeRepository(IamDatabase db)
        {
            this.db = db;
            this.repo = new RepositoryEF<Type>(db);
        }

        public List<TypeEx> GetAll(string where = "")
        {
            var query = db.Types.AsQueryable();

            //if (!string.IsNullOrEmpty(where))
            //{
            //    query = query.Where(d => d.Name == where).AsQueryable();
            //}

            query.OrderBy("Id Asc");

            return Transform(query);
        }

        public List<TypeEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10)
        {

            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Name.Contains(\"" + where + "\") || Differential.Contains(\"" + where + "\")");
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

        public int Count(string where)
        {

            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Name.Contains(\"" + where + "\") || Differential.Contains(\"" + where + "\")");
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

        public TypeEx Create()
        {
            return new TypeEx();
        }

        public TypeEx Add(TypeEx item)
        {
            var g = Transform(item);
            var types = repo.Add(g);
            repo.SaveChanges();
            item.Id = g.Id;

            return Transform(types);
        }

        public TypeEx Update(TypeEx item)
        {
            var g = Transform(item);
            var types = repo.Update(g);
            repo.SaveChanges();

            return Transform(types);
        }

        public TypeEx Get(int Id)
        {
            var types = repo.Query().Where(b => b.Id == Id).FirstOrDefault();
            return Transform(types);
        }


        public void Remove(TypeEx item)
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

        private List<TypeEx> Transform(IQueryable<Type> n)
        {
            List<TypeEx> v = new List<TypeEx>();
            foreach (var r in n)
            {
                v.Add(Transform(r));
            }

            return v;
        }
        private TypeEx Transform(Type n)
        {
            if (n == null)
            {
                return null;
            }

            var r = new TypeEx();
            r.Id = n.Id;
            r.Name = n.Name;
            r.Differential = n.Differential;

            return r;
        }

        private Type Transform(TypeEx n)
        {
            Type r;
            if (n.Id == 0)
            {
                r = db.Types.Create();
            }
            else
            {
                r = db.Types.Where(p => p.Id == n.Id).FirstOrDefault();

            }
            r.Id = n.Id;
            r.Name = n.Name;
            r.Differential = n.Differential;

            return r;
        }
    }
    #endregion
}
