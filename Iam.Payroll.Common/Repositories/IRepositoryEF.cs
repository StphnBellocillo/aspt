using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq.Dynamic;

namespace Iam.Payroll.Common
{
    public class RepositoryEF<T> : IRepository<T>
        where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly DbContext db;
        public RepositoryEF(DbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }
        public T Create()
        {
            return dbSet.Create();
        }
        public T Add(T item)
        {
            return dbSet.Add(item);

        }
        public T Update(T item)
        {
            var val = dbSet.Attach(item);
            db.Entry(item).State = EntityState.Modified;
            return val;
        }
        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void Remove(long id)
        {
            var item = dbSet.Find(id);
            dbSet.Remove(item);
        }

        public void Remove(string where)
        {
            var objects = dbSet.Where(where).AsEnumerable();
            foreach (var item in objects)
            {
                dbSet.Remove(item);
            }
        }

        public void Remove(int id)
        {
            var item = dbSet.Find(id);
            if (item != null)
            {
                dbSet.Remove(item);
            }
        }

        public void SaveChanges()
        {
            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateException updateEx)
            {
                Exception ex = updateEx.InnerException;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw new Exception(ex.Message);
            }
            catch (DbEntityValidationException dbEx)
            {
                List<dynamic> aErrors = new List<dynamic>();
                foreach (var eve in dbEx.EntityValidationErrors)
                {
                    var error = new { entity = eve.Entry.Entity.GetType().Name, state = eve.Entry.State, properties = new List<dynamic>() };
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var p = new { property = ve.PropertyName, message = ve.ErrorMessage };
                        error.properties.Add(p);
                    }
                    aErrors.Add(error);
                }
                throw dbEx;
            }
        }
        public IQueryable<T> Query()
        {
            return dbSet;
        }

        public virtual IQueryable<T> FindAll(string where = "")
        {
            return "" != where ? db.Set<T>().Where(where) : db.Set<T>();
        }

        public virtual T FindById(int id)
        {
            return db.Set<T>().Find(id);
        }


        public virtual T FindOne(string where = "")
        {
            return FindAll(where).FirstOrDefault();
        }


    }
}
