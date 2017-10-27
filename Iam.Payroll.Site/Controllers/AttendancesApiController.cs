using System.Linq;
using System.Net;
using Iam.Payroll.Services;
using Iam.Payroll.Common;
using Iam.Payroll.Data;
using Iam.Payroll.Site.Models;
using Microsoft.AspNet.Identity;
using Iam.Payroll.Data.Models;
using System.Collections.Generic;
using System.Web.Http;
using System;

namespace Iam.Payroll.Site.Controllers
{
    public class AttendancesApiController : ApiController
    {
        readonly AttendanceService svcA;
        readonly PersonService svcP;

        public AttendancesApiController(AttendanceService svcA, PersonService svcP)
        {
            this.svcA = svcA;
            this.svcP = svcP;
        }


        [Route("Attendances/All")]
        [HttpGet]
        public List<AttendanceEx> GetAll()
        {
            return svcA.GetAll("");
        }


        [Route("Attendances")]
        [HttpGet]
        public AttendanceSearchViewModel Search(DateTime? TimeIn = null, DateTime? TimeOut = null, string SearchText = "", int? PersonId = null, int PageNo = 1, int PageSize = 10, string orderBy = "", string orderField = "")
        {
            AttendanceSearchViewModel oView = new AttendanceSearchViewModel()
            {

                Results = svcA.GetPaged(SearchText, PageNo, PageSize, PersonId, orderBy, orderField, TimeIn, TimeOut),
                Count = svcA.Count(SearchText, PersonId, TimeIn, TimeOut)

            };

            return oView;
        }



        [Route("Attendances/{Id}")]
        [HttpGet]
        public AttendanceEx Get(int Id)
        {
            AttendanceEx attendance = svcA.Get(Id);
            return attendance;
        }

        // POST: Save
        [Route("Attendances/add")]
        [HttpPost]
        public AttendanceEx Add(AttendanceEx attendance)
        {
            AttendanceEx r;
            if (attendance.Id > 0)
            {
                r = svcA.Update(attendance);

            }
            else
            {
                r = svcA.Add(attendance);
            }
            return r;
        }




        // PUT: api/Attendance/5

        public void Put(int id, [FromBody]string value)
        {

        }

        //// DELETE: api/Attendance/5
        //[Route("attendances/delete")]
        //[HttpDelete]
        //public void Delete(int id)
        //{
        //    AttendanceEx attendance = svcA.Get(id);
        //    svcA.Delete(attendance);
        //}

        // DELETE: department
        [Route("Attendances/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            var r = svcA.Get(id);
            svcA.Remove(r);
        }
    }
}
