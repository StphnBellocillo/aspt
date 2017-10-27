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
    public class HolidayRepository : IHolidayRepository
    {
        readonly RepositoryEF<Holiday> repo;
        readonly IamDatabase db;

        public HolidayRepository(IamDatabase db)
        {
            this.db = db;
            this.repo = new RepositoryEF<Holiday>(db);
        }

        public List<HolidayEx> GetAll(string where)
        {
            var query = db.Holidays.AsQueryable();

            //if (!string.IsNullOrEmpty(where))
            //{
            //    query = query.Where(d => d.Name == where).AsQueryable();
            //}

            query.OrderBy("Id Asc");

            return Transform(query);
        }

        public List<HolidayEx> GetPaged(string where, int? typeId, string orderby, int PageNo, int PageSize, string orderField, DateTime? calendar = null)
        {
            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Name.Contains(\"" + where + "\") || Type.Name.Contains(\"" + where + "\")");
            }

            if (typeId != null)
            {
                aWhere.Add("TypeId.Equals(" + where + ")");
            }

            if (aWhere.Count > 0)
            {
                sWhere = string.Join("&&", aWhere);
            }

            var r = "" != sWhere ? repo.Query().Where(sWhere).OrderBy(orderField + " " + orderby) : repo.Query().OrderBy(orderField + " " + orderby);

            if (calendar.HasValue)
            {
                DateTime dCalendar = calendar.Value.Date;
                r = r.Where(s => s.Date >= dCalendar);
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

        public int Count(string where, int? TypeId, DateTime? calendar)
        {
            List<string> aWhere = new List<string>();

            string sWhere = "";
            if (!string.IsNullOrEmpty(where))
            {
                aWhere.Add("Name.Contains(\"" + where + "\") || Type.Name.Contains(\"" + where + "\")");
            }

            if (TypeId != null)
            {
                aWhere.Add("TypeId.Equals(" + where + ")");
            }

            if (aWhere.Count > 0)
            {
                sWhere = string.Join("&&", aWhere);
            }

            var r = "" != sWhere ? repo.Query() : repo.Query();

            if (calendar.HasValue)
            {
                DateTime dCalendar = calendar.Value.Date;
                r = r.Where(s => s.Date >= dCalendar);
            }


            return r.Count();
        }
        public HolidayEx Create()
        {
            return new HolidayEx();
        }

        public HolidayEx Add(HolidayEx item)
        {
            var g = Transform(item);
            var holiday = repo.Add(g);
            repo.SaveChanges();
            item.Id = g.Id;

            return Transform(holiday);
        }

        public HolidayEx Update(HolidayEx item)
        {
            var g = Transform(item);
            var holiday = repo.Update(g);
            repo.SaveChanges();

            return Transform(holiday);
        }

        public HolidayEx Get(int Id)
        {
            var holiday = repo.Query().Where(b => b.Id == Id).FirstOrDefault();
            return Transform(holiday);
        }


        public void Remove(HolidayEx item)
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

        private List<HolidayEx> Transform(IQueryable<Holiday> n)
        {
            List<HolidayEx> v = new List<HolidayEx>();
            foreach (var r in n)
            {
                v.Add(Transform(r));
            }

            return v;
        }
        private HolidayEx Transform(Holiday n)
        {
            if (n == null)
            {
                return null;
            }

            var r = new HolidayEx();
            r.Id = n.Id;
            r.Date = n.Date;
            r.Name = n.Name;
            r.TypeId = n.TypeId;
            r.TypeName = n.Type.Name;

            return r;
        }

        private Holiday Transform(HolidayEx n)
        {
            Holiday r;
            if (n.Id == 0)
            {
                r = db.Holidays.Create();
            }
            else
            {
                r = db.Holidays.Where(p => p.Id == n.Id).FirstOrDefault();

            }
            r.Id = n.Id;
            r.Name = n.Name;
            r.Name = n.Name;
            r.TypeId = n.TypeId;


            return r;
        }

    }
    #endregion
}
