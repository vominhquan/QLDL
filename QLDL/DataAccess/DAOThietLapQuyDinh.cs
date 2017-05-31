using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.DataAccess
{
    public class DAOThietLapQuyDinh
    {
        /// <summary>
        /// Singleton tech for class DAOThietLapQuyDinh
        /// </summary>
        private DAOThietLapQuyDinh() { }
        private static volatile DAOThietLapQuyDinh instance;
        public static DAOThietLapQuyDinh Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAOThietLapQuyDinh();
                return DAOThietLapQuyDinh.instance;
            }
        }

        /// <summary>
        /// insert a record into table LOAIDL- increament number of LOAIDL
        /// </summary>
        /// <param name="tenloai"></param>
        /// <param name="sonotoida"></param>
        /// <returns></returns>
        public bool InsertLoaiDL(string tenloai, int sonotoida)
        {
            using (QLDLEntities db = new QLDLEntities())
            {
                LOAIDL dl = new LOAIDL();
                dl.TENLOAI = tenloai;
                dl.SONOTOIDA = sonotoida;
                db.LOAIDLs.Add(dl);
                db.SaveChanges();
                //insert suceed
                return true;
            }
        }

        /// <summary>
        /// delete a record into table LOAIDL - descreament number of LOAIDL
        /// </summary>
        /// <param name="loaidl"></param>
        /// <returns></returns>
        public bool DeleteLAOIDL(int loaidl)
        {
            using (QLDLEntities db = new QLDLEntities())
            {
                if (db.DAILies.Where(p => p.LOAIDL == loaidl).Count() <= 0)
                {
                    LOAIDL ldl = db.LOAIDLs.Where(p => p.MALOAI == loaidl).Single();
                    db.LOAIDLs.Remove(ldl);
                    db.SaveChanges();
                    //delete succeed
                    return true;
                }
                //delete false
                return false;
            }
        }

        /// <summary>
        /// Change max-number of DAILY in QUAN
        /// </summary>
        /// <param name="maquan"></param>
        /// <param name="sodailytoida"></param>
        /// <returns></returns>
        public bool ChangeNumOfDaily(int maquan, int sodailytoida)
        {
            using (QLDLEntities db = new QLDLEntities())
            {
                QUAN quan = db.QUANs.Where(p => p.MAQUAN == maquan).Single();
                quan.SODLTOIDA = sodailytoida;
                db.SaveChanges();
                //change succeed
                return true;
            }
        }

        /// <summary>
        /// insert a record into table MATHANG- increament number of MATHANG
        /// </summary>
        /// <param name="tenhang"></param>
        /// <param name="tendvt"></param>
        /// <param name="dongia"></param>
        /// <returns></returns>
        public bool InsertMATHANG(string tenhang, int madvt, int dongia)
        {
            using(QLDLEntities db = new QLDLEntities())
            {
                MATHANG mh = new MATHANG();
                mh.TENHANG = tenhang;
                mh.MADVT = madvt;
                mh.DONGIA = dongia;
                db.MATHANGs.Add(mh);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// delete a record into table MATHANG - descreament number of MATHANG
        /// </summary>
        /// <param name="mamh"></param>
        /// <returns></returns>
        public bool DeleteMATHANG(int mamh)
        {
            using (QLDLEntities db = new QLDLEntities())
            {
                if (db.CTPXes.Where(p => p.MAHANG == mamh).Count() <= 0)
                {
                    MATHANG mh = db.MATHANGs.Where(p => p.MAHANG == mamh).Single();
                    db.MATHANGs.Remove(mh);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// insert a record into table DVT- increament number of DVT
        /// </summary>
        /// <param name="tendvt"></param>
        /// <returns></returns>
        public bool InsertDVT(string tendvt)
        {
            using (QLDLEntities db = new QLDLEntities())
            {
                DVT dvt = new DVT();
                dvt.DVT1 = tendvt;
                db.DVTs.Add(dvt);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// delete a record into table DVT - descreament number of DVT
        /// </summary>
        /// <param name="madvt"></param>
        /// <returns></returns>
        public bool DeleteDVT(int madvt)
        {
            using (QLDLEntities db = new QLDLEntities())
            {
                if (db.MATHANGs.Where(p => p.MADVT == madvt).Count() <= 0)
                {
                    DVT dvt = db.DVTs.Where(p => p.MADVT == madvt).Single();
                    db.DVTs.Remove(dvt);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Change max-number of SONOTOIDA in LOAIDL
        /// </summary>
        /// <param name="maloaidl"></param>
        /// <param name="sonotoida"></param>
        /// <returns></returns>
        public bool ChangeMaxNumOfTienNo(int maloaidl, int sonotoida)
        {
            using (QLDLEntities db = new QLDLEntities())
            {
                LOAIDL loaidl = db.LOAIDLs.Where(p => p.MALOAI == maloaidl).Single();
                loaidl.SONOTOIDA = sonotoida;
                db.SaveChanges();
                return true;
            }
        }
    }
}
