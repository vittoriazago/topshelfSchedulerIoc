using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShelfScheduler
{
    public class Repository<T> : IRepository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> Search()
        {
            return items;
        }
       
        public void Clear()
        {
            items = new List<T>();
        }
    }
}
