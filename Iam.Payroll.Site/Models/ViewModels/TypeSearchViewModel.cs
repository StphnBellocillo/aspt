using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iam.Payroll.Common;


namespace Iam.Payroll.Site.Models.ViewModels
{
    public class TypeSearchViewModel
    {
        public int Count { get; set; }
        public List<TypeEx> Results { get; set; }
    }
}