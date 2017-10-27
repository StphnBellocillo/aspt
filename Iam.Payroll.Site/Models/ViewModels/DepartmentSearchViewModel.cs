using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iam.Payroll.Common;


namespace Iam.Payroll.Site.Models.ViewModels
{
    public class DepartmentSearchViewModel
    {
        public int Count { get; set; }
        public List<DepartmentEx> Results { get; set; }
    }
}