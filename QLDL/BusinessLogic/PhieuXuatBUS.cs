using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class PhieuXuatBUS
    {

        public bool insertPhieuXuat(CTPX[] arr_ctpx,PHIEUXUATHANG pxh)
        {
            try
            {
                using (QLDLEntities context = new QLDLEntities())
                {
                    // do EF ko hỗ trợ auto increase primary key khi bị chiếu tới nên dùng thủ công
                    context.Database.ExecuteSqlCommand("insert into phieuxuathang(madl,ngaylap,tongtien,sotientra,conlai,nguoixuat) values({0},{1},{2},{3},{4},{5});",
                        pxh.MADL,DateTime.Now,pxh.TONGTIEN,pxh.SOTIENTRA,pxh.CONLAI,pxh.NGUOIXUAT); 
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
