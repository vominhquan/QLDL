using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class PhieuThuBUS
    {
        public ObservableCollection<vw_PhieuThu_NhanVien_DaiLy> getAllPhieuThu()
        {
            ObservableCollection<vw_PhieuThu_NhanVien_DaiLy> allPX = new ObservableCollection<vw_PhieuThu_NhanVien_DaiLy>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var allphieuxuat = from vw in context.vw_PhieuThu_NhanVien_DaiLy
                                   select vw;
                foreach (var item in allphieuxuat)
                    allPX.Add(item);
            }
            return allPX;
        }

        public ObservableCollection<vw_PhieuThu_NhanVien_DaiLy> getPhieuThuByDaiLy(int madl)
        {
            ObservableCollection<vw_PhieuThu_NhanVien_DaiLy> PXs = new ObservableCollection<vw_PhieuThu_NhanVien_DaiLy>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var allphieuxuat = from vw in context.vw_PhieuThu_NhanVien_DaiLy
                                   where vw.MADL == madl
                                   select vw;
                foreach (var item in allphieuxuat)
                    PXs.Add(item);
            }
            return PXs;
        }

        public bool insertPhieuThu(PHIEUTHUTIEN pt)
        {
            try
            {
                using (QLDLEntities context = new QLDLEntities())
                {
                    DAILY daily = context.DAILies.FirstOrDefault(dl => dl.MADL == pt.MADL);
                    daily.SONO -= pt.SOTIEN;

                    context.PHIEUTHUTIENs.Add(pt);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
    }
}
