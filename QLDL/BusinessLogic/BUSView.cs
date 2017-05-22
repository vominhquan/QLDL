using DAODLL;
using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUSView
    {
        /// <summary>
            /// Init Singleton for BUSView
            /// </summary>
            private BUSView() { }
            public static volatile BUSView instance;
            public static BUSView Instance
            {
                get
                {
                    if (instance == null)
                        instance = new BUSView();
                    return BUSView.instance;
                }
            }
            

            public ObservableCollection<DAILY> GetAllDaiLy()
            {
                return DAOView.Instance.GetAllDaiLy();
            }

            public ObservableCollection<QUAN> GetAllQuan()
            {
                return DAOView.Instance.GetAllQuan();
            }

            public ObservableCollection<LOAIDL> GetAllLoaiDL()
            {
                return DAOView.Instance.GetAllLoaiDL();
            }

            public ObservableCollection<DVT> GetAllDVT()
            {
                return DAOView.Instance.GetAllDVT();
            }

            public ObservableCollection<CHUCVU> GetAllCV()
            {
                return DAOView.Instance.GetAllCV();
            }
    }
}
