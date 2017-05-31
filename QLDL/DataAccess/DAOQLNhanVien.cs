using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.DataAccess
{
        public class DAOQLNhanVien
        {
            /// <summary>
            /// Init Singleton for DAOQLNhanVien
            /// </summary>
            private DAOQLNhanVien() { }
            private static volatile DAOQLNhanVien instance;
            public static DAOQLNhanVien Instance
            {
                get
                {
                    if (instance == null)
                        instance = new DAOQLNhanVien();
                    return DAOQLNhanVien.instance;
                }
            }

            /// <summary>
            /// Insert Nhanvien into database
            /// </summary>
            /// <param name="dl"></param>
            /// <returns></returns>
            public bool Insert(string ten, DateTime ngay, string dc, int macv, string acc, string pass)
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                NHANVIEN nv = new NHANVIEN()
                {
                    TENNV = ten,
                    NGAYSINH = ngay,
                    DIACHI = dc,
                    MACHUCVU = macv
                };
                db.NHANVIENs.Add(nv);
                    db.SaveChanges();

                TAIKHOAN tk = new TAIKHOAN()
                {
                    TENDANGNHAP = acc,
                    PASSWORD = pass,
                    MANV = db.NHANVIENs.Single(p => p.TENNV == ten && p.NGAYSINH == ngay && p.MACHUCVU == macv).MANV
                };
                db.TAIKHOANs.Add(tk);
                    db.SaveChanges();
                    return true;
                }
            }

            /// <summary>
            /// Delete Nhan vien
            /// </summary>
            /// <param name="ID"></param>
            public bool Delete(int manv)
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    if ((db.PHIEUXUATHANGs.Where(p => p.NGUOIXUAT == manv).Count() > 0)
                        || (db.PHIEUTHUTIENs.Where(p => p.NGUOITHU == manv).Count() > 0))
                        return false;
                    else
                    {
                        List<TAIKHOAN> li = db.TAIKHOANs.Where(p=>p.MANV == manv).ToList();
                        foreach(TAIKHOAN item in li)
                        {
                            db.TAIKHOANs.Remove(item);
                        }
                        db.SaveChanges();

                        NHANVIEN nv = db.NHANVIENs.Single(p => p.MANV == manv);
                        db.NHANVIENs.Remove(nv);
                        db.SaveChanges();
                        return true;
                    }
                }
            }

            /// <summary>
            /// Update NHANVIEN
            /// </summary>
            /// <param name="dl"></param>
            /// <returns></returns>
            public bool Update(NHANVIEN nhanvien)
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    NHANVIEN nv = db.NHANVIENs.Where(p=>p.MANV == nhanvien.MANV).Single();
                    nv.TENNV = nhanvien.TENNV;
                    //nv.NGAYSINH = nhanvien.NGAYSINH;
                    //nv.DIACHI = nhanvien.DIACHI;
                    //nv.MACHUCVU = nhanvien.MACHUCVU;
                    db.SaveChanges();
                    return true;
                }
            }
            /// <summary>
            /// Search NHan vien from database and display it to GUI
            /// Load if nhanvien's search condition equal empty string 
            /// </summary>
            public ObservableCollection<NHANVIEN> Search(int macv, string ten, string dc)
            {
                ObservableCollection<NHANVIEN> li = new ObservableCollection<NHANVIEN>();
                using (QLDLEntities db = new QLDLEntities())
                {
                    var l=db.NHANVIENs.Where(p=>p.MACHUCVU == macv && p.TENNV.Contains(ten) 
                         && p.DIACHI.Contains(dc));
                    foreach (var item in l)
                    {
                        li.Add(item as NHANVIEN);
                    }
                    return li;
                }
   
            }
        }
    }
