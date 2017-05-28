using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.Class
{
    public class GroupFilter
    {
        private List<Predicate<object>> filters;

        public Predicate<object> Filter { get; private set; }

        public GroupFilter()
        {
            filters = new List<Predicate<object>>();
            Filter = InternalFilter;
        }

        private bool InternalFilter(object o)
        {
            foreach (var filter in filters)
            {
                if (!filter(o))
                {
                    return false;
                }
            }
            return true;
        }

        public void AddFilter(Predicate<object> filter)
        {
            filters.Add(filter);
        }
        public void AddFilter(Predicate<object> filter, bool condition)
        {
            if(condition) AddFilter(filter);
        }

        public void RemoveFilter(Predicate<object> filter)
        {
            if (filters.Contains(filter))
            {
                filters.Remove(filter);
            }
        }
    }
}
