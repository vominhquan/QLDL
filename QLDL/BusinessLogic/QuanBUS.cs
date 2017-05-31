using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class QuanBUS
    {
        public bool InsertOrUpdateQuan(QUAN[] quans)
        {
            try
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    foreach (QUAN quan in quans)
                    {
                        if (quan.MAQUAN == 0)
                        {
                            QUAN item = new QUAN()
                            {
                                TENQUAN = quan.TENQUAN,
                                SODLTOIDA = quan.SODLTOIDA
                            };
                            db.QUANs.Add(item);
                        }
                        else
                        {
                            QUAN item = db.QUANs.FirstOrDefault(x => x.MAQUAN == quan.MAQUAN);
                            item.TENQUAN = quan.TENQUAN;
                            item.SODLTOIDA = quan.SODLTOIDA;
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
