using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iam.Payroll.Common;


namespace Iam.Payroll.Site.Models
{
    public class PersonSearchViewModel
    {
        public int Count { get; set; }
        public List<PersonEx> Results { get; set; }
    }
}