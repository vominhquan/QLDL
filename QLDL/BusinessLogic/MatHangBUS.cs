using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class MatHangBUS
    {
        public ObservableCollection<MATHANG> getAllMatHang()
        {
            ObservableCollection<MATHANG> allMH = new ObservableCollection<MATHANG>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var alldmathang = from mh in context.MATHANGs
                               select mh;
                foreach (var item in alldmathang)
                    allMH.Add(item);
            }
            return allMH;
        }
    }
}
