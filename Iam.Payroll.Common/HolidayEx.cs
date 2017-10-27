using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iam.Payroll.Common
{
    public class HolidayEx
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual TypeEx Type { get; set; }
    }
}
