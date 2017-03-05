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
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> getAllDaiLy()
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

        public ObservableCollection<LOAIDL> getAllLoaiDL()
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

        public ObservableCollection<QUAN> getAllQuan()
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

        public bool insertDaiLy(DAILY dl)
        {
            try
            {
                using (QLDLEntities context = new QLDLEntities())
                {
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
        
    }
}
