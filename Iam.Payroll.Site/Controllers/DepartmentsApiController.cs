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
    public class DepartmentsApiController : ApiController
    {

        readonly DepartmentService svcD;

        public DepartmentsApiController(DepartmentService svcD)
        {
            this.svcD = svcD;
        }

        [Route("Departments")]
        [HttpGet]
        public List<DepartmentEx> GetAll(string where = "")
        {
            return svcD.GetAll("");
        }

        [Route("Department")]
        [HttpGet]
        public DepartmentSearchViewModel Search(string SearchText = "", int PageNo = 1, int PageSize = 10)
        {
            DepartmentSearchViewModel oView = new DepartmentSearchViewModel();
            oView.Results = svcD.GetPaged(SearchText, "Name", PageNo, PageSize);
            oView.Count = svcD.Count(SearchText);
            return oView;

        }




        // GET: api/Departments/5
        [Route("Department/{Id}")]
        [HttpGet]
        public DepartmentEx Get(int Id)
        {
            DepartmentEx department = svcD.Get(Id);
            return department;
        }


        [Route("department/add")]
        [HttpPost]
        public DepartmentEx Add(DepartmentEx department)
        {
            DepartmentEx r;
            if (department.Id > 0)
            {
                r = svcD.Update(department);
            }
            else
            {
                r = svcD.Add(department);
            }
            return r;
        }



        // PUT: department

        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: department
        [Route("Department/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            var r = svcD.Get(id);
            svcD.Remove(r);
        }
    }
}
