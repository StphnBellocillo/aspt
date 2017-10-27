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
    public class TypesApiController : ApiController
    {

        readonly TypeService svcType;

        public TypesApiController(TypeService svcType)
        {
            this.svcType = svcType;
        }

        [Route("Types/All")]
        [HttpGet]
        public List<TypeEx> GetAll(string where = "")
        {
            return svcType.GetAll("");
        }

        [Route("Types")]
        [HttpGet]
        public TypeSearchViewModel Search(string SearchText = "", int PageNo = 1, int PageSize = 10)
        {
            TypeSearchViewModel oView = new TypeSearchViewModel();
            oView.Results = svcType.GetPaged(SearchText, "Name", PageNo, PageSize);
            oView.Count = svcType.Count(SearchText);
            return oView;

        }




        // GET: api/Types/5
        [Route("Type/{Id}")]
        [HttpGet]
        public TypeEx Get(int Id)
        {
            TypeEx type = svcType.Get(Id);
            return type;
        }


        [Route("Type/add")]
        [HttpPost]
        public TypeEx Add(TypeEx type)
        {
            TypeEx r;
            if (type.Id > 0)
            {
                r = svcType.Update(type);
            }
            else
            {
                r = svcType.Add(type);
            }
            return r;
        }



        // PUT: Type

        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: Type
        [Route("Type/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            var r = svcType.Get(id);
            svcType.Remove(r);
        }
    }
}

