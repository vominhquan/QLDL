using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class PhieuXuatBUS
    {

        public ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy> getAllPhieuXuat()
        {
            ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy> allPX = new ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var allphieuxuat = from vw in context.vw_PhieuXuat_NhanVien_DaiLy
                                   select vw;
                foreach (var item in allphieuxuat)
                    allPX.Add(item);
            }
            return allPX;
        }

        public ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy> getPhieuXuatByDaiLy(int madl)
        {
            ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy> PXs = new ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var allphieuxuat = from vw in context.vw_PhieuXuat_NhanVien_DaiLy
                                   where vw.MADL == madl
                                   select vw;
                foreach (var item in allphieuxuat)
                    PXs.Add(item);
            }
            return PXs;
        }

        public ObservableCollection<vw_PhieuXuat_CTPX_MatHang> getCTPXPhieuXuatByMaPhieu(int maphieu)
        {
            ObservableCollection<vw_PhieuXuat_CTPX_MatHang> PXs = new ObservableCollection<vw_PhieuXuat_CTPX_MatHang>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var allphieuxuat = from vw in context.vw_PhieuXuat_CTPX_MatHang
                                   where vw.MAPHIEU == maphieu
                                   select vw;
                foreach (var item in allphieuxuat)
                    PXs.Add(item);
            }
            return PXs;
        }


        public bool insertPhieuXuat(CTPX[] arr_ctpx,PHIEUXUATHANG pxh)
        {
            try
            {
                using (QLDLEntities context = new QLDLEntities())
                {
                    // do EF ko hỗ trợ auto increase primary key khi bị chiếu tới nên dùng thủ công
                    context.Database.ExecuteSqlCommand("insert into phieuxuathang(madl,ngaylap,tongtien,sotientra,conlai,nguoixuat) values({0},{1},{2},{3},{4},{5});",
                        pxh.MADL, DateTime.Now, pxh.TONGTIEN, pxh.SOTIENTRA, pxh.CONLAI, pxh.NGUOIXUAT);

                    DAILY daily = context.DAILies.FirstOrDefault(dl => dl.MADL == pxh.MADL);
                    daily.SONO += (pxh.TONGTIEN - pxh.SOTIENTRA);

                    context.SaveChanges();
                    /// lấy mã phiếu cuối cùng (dòng mới vừa nhập) nếu hệ thống chạy song song là tạch :((
                    int value = int.Parse(context.PHIEUXUATHANGs
                            .OrderByDescending(p => p.MAPHIEU)
                            .Select(r => r.MAPHIEU)
                            .First().ToString());
                    // tạo các chi tiết phiếu xuất
                    foreach (CTPX ct in arr_ctpx)
                    {
                        ct.MAPHIEU = value;
                        context.CTPXes.Add(ct);
                    }
                    context.SaveChanges();
                    return true;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("{0}", e);
                return false;
            }
        }


    }
}
