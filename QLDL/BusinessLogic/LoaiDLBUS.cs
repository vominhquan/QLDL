using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class LoaiDLBUS
    {
        public bool InsertOrUpdateLoaiDL(LOAIDL[] loaiDLs)
        {
            try
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    foreach (LOAIDL loaiDL in loaiDLs)
                    {
                        if (loaiDL.MALOAI == 0)
                        {
                            LOAIDL item = new LOAIDL()
                            {
                                TENLOAI = loaiDL.TENLOAI,
                                SONOTOIDA = loaiDL.SONOTOIDA
                            };
                            db.LOAIDLs.Add(item);
                        }
                        else
                        {
                            LOAIDL item = db.LOAIDLs.FirstOrDefault(x => x.MALOAI == loaiDL.MALOAI);
                            item.TENLOAI = loaiDL.TENLOAI;
                            item.SONOTOIDA = loaiDL.SONOTOIDA;
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
