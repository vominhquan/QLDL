using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class DonViTinhBUS
    {
        public ObservableCollection<DVT> GetAllDVT()
        {
            ObservableCollection<DVT> allDVT = new ObservableCollection<DVT>();
            using (QLDLEntities db = new QLDLEntities())
            {
                var all = from dvt in db.DVTs
                                  select dvt;
                foreach (var item in all)
                    allDVT.Add(item);
                return allDVT;
            }
        }
    }
}
