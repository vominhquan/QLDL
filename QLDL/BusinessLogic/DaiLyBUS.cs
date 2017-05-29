using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{

    class DaiLyBUS
    {
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> GetAllDaiLy()
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> allDL = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var alldaily = from vw in context.vwDAILY_LOAIDL_QUAN
                               select vw;
                foreach(var item in alldaily)
                    allDL.Add(item);
            }
            return allDL;
        }

        public ObservableCollection<LOAIDL> GetAllLoaiDL()
        {
            ObservableCollection<LOAIDL> allLoai = new ObservableCollection<LOAIDL>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var all = from loai in context.LOAIDLs
                          select loai;
                foreach (var item in all)
                    allLoai.Add(item);
            }
            return allLoai;
        }

        public ObservableCollection<QUAN> GetAllQuan()
        {
            ObservableCollection<QUAN> allQuan = new ObservableCollection<QUAN>();
            using (QLDLEntities context = new QLDLEntities())
            {
                var all = from quan in context.QUANs
                          select quan;
                foreach (var item in all)
                    allQuan.Add(item);
            }
            return allQuan;
        }

        #region Đại lý CRUD
        public bool InsertDaiLy(string tendl, string diachi, string dienthoai, int maquan, int loaidl)
        {
            try
            {
                using (QLDLEntities context = new QLDLEntities())
                {
                    DAILY dl = new DAILY
                    {
                        TENDL = tendl,
                        DIACHI = diachi,
                        DIENTHOAI = dienthoai,
                        NGAYTIEPNHAN = DateTime.Now,
                        MAQUAN = maquan,
                        LOAIDL = loaidl,
                        TINHTRANG = 1, // 1: đang hoạt động, 0: đã dẹp tiệm
                        SONO = 0
                    };
                    context.DAILies.Add(dl);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDaiLy
            (int madl, 
            string tendl, 
            string diachi, 
            string dienthoai,
            int maquan,
            int loaidl)
        {
            try
            {
                using (QLDLEntities context = new QLDLEntities())
                {
                    DAILY daily = context.DAILies.FirstOrDefault(dl => dl.MADL == madl);
                    daily.TENDL = tendl;
                    daily.DIACHI = diachi;
                    daily.DIENTHOAI = dienthoai;
                    daily.MAQUAN = maquan;
                    daily.LOAIDL = loaidl;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(e.ToString());
                return false;
            }
        }
        public bool UpdateDaiLy(vwDAILY_LOAIDL_QUAN DaiLy)
        {
            return UpdateDaiLy(
                DaiLy.MADL,
                DaiLy.TENDL, 
                DaiLy.DIACHI, 
                DaiLy.DIENTHOAI, 
                (int)DaiLy.MAQUAN,
                (int)DaiLy.LOAIDL
            );
        }
        public bool RemoveDaiLy(int madl)
        {
            try
            {
                using (QLDLEntities context = new QLDLEntities())
                {
                    DAILY daily = context.DAILies.FirstOrDefault(dl => dl.MADL == madl);
                    context.DAILies.Remove(daily);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        } 
        #endregion
    }
}
