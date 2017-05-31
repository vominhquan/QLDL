using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class NhanVienBUS
    {
        public ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN> GetAllNhanVien()
        {
            ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN> allNV = new ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var allnhanvien = from vw in context.vwCHUCVU_NHANVIEN_TAIKHOAN
                               select vw;
                foreach (var item in allnhanvien)
                    allNV.Add(item);
                return allNV;
            }
        }
        public ObservableCollection<CHUCVU> GetALLChucVu()
        {
            ObservableCollection<CHUCVU> allChucVu = new ObservableCollection<CHUCVU>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var all = from chucvu in context.CHUCVUs
                          select chucvu;
                foreach (var item in all)
                    allChucVu.Add(item);
            }
            return allChucVu;
        }
    }
}
