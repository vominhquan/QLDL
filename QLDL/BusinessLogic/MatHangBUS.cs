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
        public ObservableCollection<MATHANG> GetAllMatHang()
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
        public bool InsertOrUpdateMatHang(MATHANG[] mathangs)
        {
            try
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    foreach (MATHANG mathang in mathangs)
                    {
                        if (mathang.MAHANG == 0)
                        {
                            MATHANG item = new MATHANG()
                            {
                                TENHANG = mathang.TENHANG,
                                DONGIA = mathang.DONGIA,
                                MADVT = mathang.MADVT,
                            };
                            db.MATHANGs.Add(item);
                        }
                        else
                        {
                            MATHANG item = db.MATHANGs.FirstOrDefault(x => x.MAHANG == mathang.MAHANG);
                            item.TENHANG = mathang.TENHANG;
                            item.DONGIA = mathang.DONGIA;
                            item.MADVT = mathang.MADVT;
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
