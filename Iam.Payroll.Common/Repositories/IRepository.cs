using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iam.Payroll.Common
{
    public interface IRepository<T> where T : class
    {

        T Create();

        T Add(T item);  

        T Update(T item);

        void Remove(T item);

        void Remove(int id);

        void Remove(string id);

    }
}
