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
        public bool UpdateNhanVien(vwCHUCVU_NHANVIEN_TAIKHOAN nhanvien)
        {
            try
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    NHANVIEN nv = db.NHANVIENs.Where(p => p.MANV == nhanvien.MANV).Single();
                    nv.TENNV = nhanvien.TENNV.Length > 10 ? nhanvien.TENNV.Substring(0, 10) : nhanvien.TENNV;
                    nv.NGAYSINH = nhanvien.NGAYSINH;
                    nv.DIACHI = nhanvien.DIACHI.Length > 50 ? nhanvien.DIACHI.Substring(0,50) : nhanvien.DIACHI; //.Substring(0, 50);
                    nv.MACHUCVU = nhanvien.MACHUCVU;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(e.ToString());
                return false;
            }
        }
        public vwCHUCVU_NHANVIEN_TAIKHOAN GetNhanVienByMANV(int manv)
        {
            using (QLDLEntities context = new QLDLEntities())
            {
                vwCHUCVU_NHANVIEN_TAIKHOAN nhanvien =
                    context.vwCHUCVU_NHANVIEN_TAIKHOAN.FirstOrDefault(nv => nv.MANV == manv);
                return nhanvien;
            }
        }
        public int? AddNhanVien_TaiKhoan(NHANVIEN nhanvien, TAIKHOAN taikhoan)
        {

            try
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    db.NHANVIENs.Add(nhanvien);
                    db.SaveChanges();

                    taikhoan.MANV = nhanvien.MANV;
                    db.TAIKHOANs.Add(taikhoan);
                    db.SaveChanges();
                    return nhanvien.MANV;
                }
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(e.ToString());
                return null;
            }

        }
        public int? KiemTraDangNhap(TAIKHOAN taikhoan)
        {
            try
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    var all = from tk in db.TAIKHOANs
                              select tk;
                    foreach (var item in all)
                    {
                        if(item.TENDANGNHAP.Trim() == taikhoan.TENDANGNHAP
                            && item.PASSWORD == taikhoan.PASSWORD)
                        {
                            return item.MANV;
                        }
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(e.ToString());
                return null;
            }

        }
    }
}
