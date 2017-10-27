using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iam.Payroll.Common
{
    public class ImageEx
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string OwnerType { get; set; }
        public string OwnerId { get; set; }
        public bool IsDefault { get; set; }
    }
}
