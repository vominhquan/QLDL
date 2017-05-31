using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDL.DataAccess;

namespace BUS
{
    public class BUSThietLapQuyDinh
    {
        /// <summary>
            /// Init Singleton for DAOQLNhanVien
            /// </summary>
        private BUSThietLapQuyDinh() { }
        public static volatile BUSThietLapQuyDinh instance;
        public static BUSThietLapQuyDinh Instance
            {
                get
                {
                    if (instance == null)
                        instance = new BUSThietLapQuyDinh();
                    return BUSThietLapQuyDinh.instance;
                }
            }

        public bool InsertLoaiDL(string tenloai, int sonotoida)
        {
            return DAOThietLapQuyDinh.Instance.InsertLoaiDL(tenloai,sonotoida);
        }


        public bool ChangeNumOfDaily(int maquan, int sodailytoida)
        {
            return DAOThietLapQuyDinh.Instance.ChangeNumOfDaily(maquan, sodailytoida);
        }


        public bool InsertMATHANG(string tenhang, int madvt, int dongia)
        {
            return DAOThietLapQuyDinh.Instance.InsertMATHANG(tenhang,madvt,dongia);
        }


        public bool InsertDVT(string tendvt)
        {
            return DAOThietLapQuyDinh.Instance.InsertDVT(tendvt);
        }


        public bool ChangeMaxNumOfTienNo(int maloaidl, int sonotoida)
        {
            return DAOThietLapQuyDinh.Instance.ChangeMaxNumOfTienNo(maloaidl,sonotoida);
        }
    }
}
