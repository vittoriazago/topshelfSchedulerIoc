using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShelfScheduler
{
    public interface IRepository<T>
    {
        void Add(T it);

        List<T> Search();
       
        void Clear();
    }
}
