using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iam.Payroll.Common
{
    public class AttendanceEx
    {

        public int Id { get; set; }
        public Nullable<int> PersonId { get; set; }
        public string PersonName { get; set; }
        public System.DateTime TimeIn { get; set; }
        public System.DateTime TimeOut { get; set; }

    }
}
