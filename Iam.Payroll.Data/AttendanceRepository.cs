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
    public class AttendanceRepository : IAttendanceRepository
    {
        readonly RepositoryEF<Attendance> repo;
        readonly IamDatabase db;
        

        public AttendanceRepository(IamDatabase db)
        {
            this.db = db;
            this.repo = new RepositoryEF<Attendance>(db);
        }

        public List<AttendanceEx> GetAll(string where = "")
        {
            var query = db.Attendances.AsQueryable();

            //if (!string.IsNullOrEmpty(where))
            //{
            //    query = query.Where(d => d.Name == where).AsQueryable();
            //}

            query.OrderBy("Id Asc");

            return Transform(query);
        }

        public List<AttendanceEx> GetPaged(string where, int PageNo, int PageSize, int? PersonId, string orderby, string orderField, DateTime? TimeIn = null, DateTime? TimeOut = null)
        {
            List<string> aWhere = new List<string>();
            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Person.FirstName.Contains(\"" + where + "\") || Person.LastName.Contains(\"" + where + "\")");
            }
         
            if (aWhere.Count > 0)
            {
                sWhere = string.Join("&&", aWhere);
            }
            var r = "" != sWhere ? repo.Query().Where(sWhere).OrderBy(orderField + " " + orderby) : repo.Query().OrderBy(orderField + " " + orderby);

            if (TimeIn.HasValue)
            {
                DateTime dTimeIn = TimeIn.Value.Date;
                r = r.Where(s => s.TimeIn >= dTimeIn);
            }

            if (TimeOut.HasValue)
            {
                DateTime dTimeOut= TimeOut.Value.Date.AddDays(1);
                r = r.Where(s => s.TimeOut <= dTimeOut);
            }

            if (PageNo == 0 || PageSize == 0)
            {
                return Transform(r);
            }
            else
            {
                return Transform(r.Skip((PageNo - 1) * PageSize).Take(PageSize));
            }

        }

        public int Count(string where, int? PersonId, DateTime? TimeIn, DateTime? TimeOut)
        {
            List<string> aWhere = new List<string>();
            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Person.FirstName.Contains(\"" + where + "\") || Person.LastName.Contains(\"" + where + "\")");
            }

            if (aWhere.Count > 0)
            {
                sWhere = string.Join("&&", aWhere);
            }
            var r = ("" == sWhere) ? repo.Query() : repo.Query().Where(sWhere);

            if (TimeIn.HasValue)
            {
                DateTime dTimeIn = TimeIn.Value.Date;
                r = r.Where(s => s.TimeIn >= dTimeIn);
            }

            if (TimeOut.HasValue)
            {
                DateTime dTimeOut = TimeOut.Value.Date.AddDays(1);
                
                r = r.Where(s => s.TimeOut <= dTimeOut);
            }

            return r.Count();



        }

        //CRUD 

        public AttendanceEx Create()
        {
            return new AttendanceEx();
        }

        public AttendanceEx Add(AttendanceEx item)
        {
            var g = Transform(item);
            var attendance = repo.Add(g);
            repo.SaveChanges();
            item.Id = g.Id;

            return Transform(attendance);
        }

        public AttendanceEx Get(int Id)
        {
            var attendance = repo.Query().Where(b => b.Id == Id).FirstOrDefault();
            return Transform(attendance);
        }

        public AttendanceEx Update(AttendanceEx item)
        {
            var r = Transform(item);
            var attendance = repo.Update(r);
            repo.SaveChanges();

            return Transform(attendance);
        }

        public void Remove(AttendanceEx item)
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

        private List<AttendanceEx> Transform(IQueryable<Attendance> n)
        {
            List<AttendanceEx> v = new List<AttendanceEx>();
            foreach (var r in n)
            {
                v.Add(Transform(r));
            }

            return v;
        }

        private AttendanceEx Transform(Attendance n)
        {
            if (n == null)
            {
                return null;
            }

            var r = new AttendanceEx();
            r.Id = n.Id;
            r.PersonId = n.PersonId;
            r.PersonName = n.Person.FirstName + " " + n.Person.LastName;
            r.TimeIn = n.TimeIn;
            r.TimeOut = n.TimeOut;

            return r;
        }

        private Attendance Transform(AttendanceEx n)
        {
            Attendance r;
            if (n.Id == 0)
            {
                r = db.Attendances.Create();
            }
            else
            {
                r = db.Attendances.Where(p => p.Id == n.Id).FirstOrDefault();

            }
            r.Id = n.Id;
            r.PersonId = n.PersonId;
            r.TimeIn = n.TimeIn;
            r.TimeOut = n.TimeOut;

            return r;
        }
    }
    #endregion
}
