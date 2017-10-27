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

namespace Iam.Payroll.Site.Controllers
{
    public class PeopleApiController : ApiController
    {
        readonly PersonService svcP;
        readonly DepartmentService svcD;

        public PeopleApiController(PersonService svcP, DepartmentService svcD)
        {
            this.svcP = svcP;
            this.svcD = svcD;
        }


        //GET: ALL People
        [Route("People/All")]
        [HttpGet]
        public List<PersonEx> GetAll()
        {
            return svcP.GetAll("");
        }


        //[Route("People/Search")]
        //[HttpGet]
        //public PersonSearchViewModel GetPaged(string SearchText = "", int PageNo = 1, int PageSize = 10, string orderby = "Desc")
        //{
        //    return Search(SearchText, PageNo, PageSize, orderby);
        //}

        [Route("People")]
        [HttpGet]
        public PersonSearchViewModel Search(string SearchText = "", int? DepartmentId = null, string orderby = "Id Asc", int PageNo = 1, int PageSize = 10)
        {

            PersonSearchViewModel oView = new PersonSearchViewModel();
            oView.Results = svcP.GetPaged(SearchText, DepartmentId, orderby, PageNo, PageSize);
            oView.Count = svcP.Count(SearchText, DepartmentId);
            return oView;

        }



        // GET: ID
        [Route("People/{Id}")]
        [HttpGet]
        public PersonEx Get(int Id)
        {
            PersonEx person = svcP.Get(Id);
            return person;
        }

        // POST: Save
        [Route("People/add")]
        [HttpPost]
        public PersonEx Add(PersonEx person)
        {
            PersonEx r;
            if (person.Id > 0)
            {
                r = svcP.Update(person);
            }
            else
            {
                r = svcP.Add(person);
            }
            return r;
        }



        // PUT: api/Attendance/5

        public void Put(int id, [FromBody]string value)
        {

        }

        //DELETE 
        [Route("People/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            var r = svcP.Get(id);
            svcP.Remove(r);
        }
    }
}
