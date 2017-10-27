using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iam.Payroll.Common
{
    public   interface IImagesRepository : IRepository<ImageEx>
    {
        List<ImageEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10);
        int Count(string where);
        ImageEx Get(int Id);
        List<ImageEx> GetAll(string where = "");
    }
}
