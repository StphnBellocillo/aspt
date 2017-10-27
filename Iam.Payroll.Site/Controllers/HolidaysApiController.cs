using System;
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
using Iam.Payroll.Site.Models.ViewModels;

namespace Iam.Payroll.Site.Controllers
{
    public class HolidaysApiController : ApiController
    {
        readonly HolidayService svcH;
        readonly TypeService svcT;

        public HolidaysApiController(HolidayService svcH, TypeService svcT)
        {
            this.svcH = svcH;
            this.svcT = svcT;
        }


        //GET: ALL Holiday
        [Route("Holiday/All")]
        [HttpGet]
        public List<HolidayEx> GetAll()
        {
            return svcH.GetAll("");
        }


        //[Route("Holiday/Search")]
        //[HttpGet]
        //public HolidaySearchViewModel GetPaged(string SearchText = "", int PageNo = 1, int PageSize = 10, string orderby = "Desc")
        //{
        //    return Search(SearchText, PageNo, PageSize, orderby);
        //}

        [Route("Holidays")]
        [HttpGet]
        public HolidaySearchViewModel Search(string SearchText = "", int? typeId = null, string orderby = "", string orderField = "", int PageNo = 1, int PageSize = 10, DateTime? calendar = null)
        {


            HolidaySearchViewModel oView = new HolidaySearchViewModel()
            {
                Results = svcH.GetPaged(SearchText, typeId, orderby, PageNo, PageSize, orderField, calendar),
                Count = svcH.Count(SearchText, typeId, calendar)
            };

            return oView;

        }



        // GET: ID
        [Route("Holiday/{Id}")]
        [HttpGet]
        public HolidayEx Get(int Id)
        {
            HolidayEx holiday = svcH.Get(Id);
            return holiday;
        }

        // POST: Save
        [Route("Holiday/add")]
        [HttpPost]
        public HolidayEx Add(HolidayEx holiday)
        {
            HolidayEx r;
            if (holiday.Id > 0)
            {
                r = svcH.Update(holiday);
            }
            else
            {
                r = svcH.Add(holiday);
            }
            return r;
        }



        // PUT: api/Attendance/5

        public void Put(int id, [FromBody]string value)
        {

        }

        //DELETE 
        [Route("Holiday/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            var r = svcH.Get(id);
            svcH.Remove(r);
        }
    }
}
