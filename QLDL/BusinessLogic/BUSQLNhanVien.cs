using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QLDL.DataAccess;

namespace QLDL.BusinessLogic
{
    public class BUSQLNhanVien
    {
        /// <summary>
            /// Init Singleton for BUSQLNhanVien
            /// </summary>
        private BUSQLNhanVien() { }
        public static volatile BUSQLNhanVien instance;
        public static BUSQLNhanVien Instance
            {
                get
                {
                    if (instance == null)
                        instance = new BUSQLNhanVien();
                    return BUSQLNhanVien.instance;
                }
            }

        public bool Insert(string ten, DateTime ngay, string dc, int macv, string acc, string pass)
        {
            if (ten == "" || dc == "")
                return false;
            if (acc == "") acc = "admin";
            if (pass == "") pass = "admin";
            return DAOQLNhanVien.Instance.Insert(ten,ngay,dc,macv,acc,pass);
        }


        public bool Delete(int manv)
        {
            return DAOQLNhanVien.Instance.Delete(manv);
        }


        public bool Update(int manv,string ten, DateTime ngay, string dc, int macv) 
        {
            if (ten == "" || dc == "" || macv == 0)
                return false;
            NHANVIEN nv = new NHANVIEN()
            {
                MANV = manv,
                TENNV = ten,
                NGAYSINH = ngay,
                DIACHI = dc,
                MACHUCVU = macv
            };
            return DAOQLNhanVien.Instance.Update(nv);
        }

        public ObservableCollection<NHANVIEN> Search(int macv, string ten, string dc)
        {
            return DAOQLNhanVien.Instance.Search(macv,ten,dc);
        }
    }
}
