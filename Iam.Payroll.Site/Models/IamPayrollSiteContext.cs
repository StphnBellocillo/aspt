using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Iam.Payroll.Site.Models
{
    public class IamPayrollSiteContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public IamPayrollSiteContext() : base("name=IamPayrollSiteContext")
        {
        }

        public System.Data.Entity.DbSet<Iam.Payroll.Data.Models.Attendance> Attendances { get; set; }

        public System.Data.Entity.DbSet<Iam.Payroll.Data.Models.Person> People { get; set; }

        public System.Data.Entity.DbSet<Iam.Payroll.Data.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Iam.Payroll.Data.Models.Holiday> Holidays { get; set; }

        public System.Data.Entity.DbSet<Iam.Payroll.Data.Models.Type> Types { get; set; }
    }
}
