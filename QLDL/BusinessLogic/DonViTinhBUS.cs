using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDL.BusinessLogic
{
    class DonViTinhBUS
    {
        public ObservableCollection<DVT> GetAllDVT()
        {
            ObservableCollection<DVT> allDVT = new ObservableCollection<DVT>();
            using (QLDLEntities db = new QLDLEntities())
            {
                var all = from dvt in db.DVTs
                                  select dvt;
                foreach (var item in all)
                    allDVT.Add(item);
                return allDVT;
            }
        }

        public bool InsertOrUpdateDonViTinh(DVT[] DVTs)
        {
            try
            {
                using (QLDLEntities db = new QLDLEntities())
                {
                    foreach (DVT dvt in DVTs)
                    {
                        if (dvt.MADVT == 0)
                        {
                            DVT item = new DVT()
                            {
                                DVT1 = dvt.DVT1
                            };
                            db.DVTs.Add(item);
                        }
                        else
                        {
                            DVT item = db.DVTs.FirstOrDefault(x => x.MADVT == dvt.MADVT);
                            item.DVT1 = dvt.DVT1;
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
