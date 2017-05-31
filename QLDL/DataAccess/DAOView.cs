using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.DataAccess
{
    public class DAOView
    {
        /// <summary>
        /// Singleton tech for class DAOView
        /// </summary>
        private DAOView() { }
        public static volatile DAOView instance;
        public static DAOView Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAOView();
                return DAOView.instance;
            }
        }

        /// <summary>
        /// Get all DaiLy's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<DAILY> GetAllDaiLy()
        {
            ObservableCollection<DAILY> li = new ObservableCollection<DAILY>();
            using (QLDLEntities db = new QLDLEntities())
            {
                var l = db.DAILies.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as DAILY);
                }
                return li;
            }
        }

        /// <summary>
        /// Get all QUAN's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<QUAN> GetAllQuan()
        {
            ObservableCollection<QUAN> li = new ObservableCollection<QUAN>();
            using (QLDLEntities db = new QLDLEntities())
            {
                var l = db.QUANs.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as QUAN);
                }
                return li;
            }
        }

        /// <summary>
        /// Get all LAOIDAILY's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<LOAIDL> GetAllLoaiDL()
        {
            ObservableCollection<LOAIDL> li = new ObservableCollection<LOAIDL>();
            using (QLDLEntities db = new QLDLEntities())
            {
                var l = db.LOAIDLs.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as LOAIDL);
                }
                return li;
            }
            
        }

        /// <summary>
        /// Get all DVT's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<DVT> GetAllDVT()
        {
            ObservableCollection<DVT> li = new ObservableCollection<DVT>();
            using (QLDLEntities db = new QLDLEntities())
            {
                var l = db.DVTs.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as DVT);
                }
                return li;
            }
           
        }

        /// <summary>
        /// get all CHUC VU
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<CHUCVU> GetAllCV()
        {
            ObservableCollection<CHUCVU> li = new ObservableCollection<CHUCVU>();
            using (QLDLEntities db = new QLDLEntities())
            {
                 var l = db.CHUCVUs.Select(p=>p);
                foreach(var item in l)
                 {
                     li.Add(item as CHUCVU);
                 }
                return li;
            }
        }

        /// <summary>
        /// Get worker by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN> GetAllNV(string name)
        {
            ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN> li = new ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN>();
            using (QLDLEntities db = new QLDLEntities())
            {
                var l = db.vwCHUCVU_NHANVIEN_TAIKHOAN.Where(p => p.TENNV.Contains(name));
                foreach (var item in l)
                {
                    li.Add(item as vwCHUCVU_NHANVIEN_TAIKHOAN);
                }
                return li;
            }
        }
    }
}
