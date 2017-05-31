using QLDL.BusinessLogic;
using QLDL.Class;
using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for PhieuXuat.xaml
    /// </summary>
    /// 


    public partial class PhieuXuat : Window
    {
        //public vwDAILY_LOAIDL_QUAN Vwdl { get; set; }
        //private ObservableCollection<MATHANG> listMatHang;
        //public ICollectionView collectionView;
        //public string searchstring;
        //public Predicate<object> searchFilter;
        //private MatHangBUS mhbus = new MatHangBUS();
        //private PhieuXuatBUS pxbus = new PhieuXuatBUS();
        //private PhieuThuBUS ptbus = new PhieuThuBUS();
        //private ObservableCollection<CTPX> listCTPX;
        //private ObservableCollection<CTPXUserControl> listUserControl;

        public PhieuXuat(vwDAILY_LOAIDL_QUAN View)
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                PhieuXuatHang = new PHIEUXUATHANG()
                {
                    NGUOIXUAT = 1,
                    MADL = View.MADL,
                    NGAYLAP = DateTime.Now,
                    TONGTIEN = 0,
                    SOTIENTRA = 0,
                    CONLAI = 0
                },
                DanhSachChiTietPhieuXuat = new ObservableCollection<CTPX>()
            };
        }
        public class State : INotifyPropertyChanged
        {
            #region Init INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            #endregion

            #region (ObservableCollection) Chi tiết phiếu xuất
            private PHIEUXUATHANG phieuXuatHang;

            public PHIEUXUATHANG PhieuXuatHang
            {
                get => phieuXuatHang;
                set => phieuXuatHang = value;
            }
            #endregion

            #region (ObservableCollection) Danh sách chi tiết phiếu xuất
            private ObservableCollection<CTPX> danhSachChiTietPhieuXuat;
            public ObservableCollection<CTPX> DanhSachChiTietPhieuXuat
            {
                get => danhSachChiTietPhieuXuat;
                set => danhSachChiTietPhieuXuat = value;
            }
            #endregion

            public void Update()
            {
                PhieuXuatHang.TONGTIEN = 0;
                foreach (CTPX ct in CTPXs)
                {
                    MATHANG mh = MatHang.ToList().Find(x => x.MAHANG == ct.MAHANG);
                    if(mh != null)
                    {
                        PhieuXuatHang.TONGTIEN += (decimal)(
                            mh.DONGIA * ct.SOLUONG
                        );
                    }
                }
                PhieuXuatHang.CONLAI = PhieuXuatHang.TONGTIEN - PhieuXuatHang.SOTIENTRA;
                OnPropertyChanged("DanhSachChiTietPhieuXuat");
                OnPropertyChanged("PhieuXuatHang");
            }

            #region List
            public CTPX[] CTPXs
            {
                get => DanhSachChiTietPhieuXuat.ToArray();
            }
            public ObservableCollection<MATHANG> MatHang
            {
                get => new MatHangBUS().GetAllMatHang();
            }
            #endregion
        };
        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            ((State)DataContext).DanhSachChiTietPhieuXuat.Remove(
                ListViewDSPX.SelectedItem as CTPX
            );
            Update();
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            int MatHangCount = ((State)DataContext).MatHang.Count,
                DSCTPXCount = ((State)DataContext).DanhSachChiTietPhieuXuat.Count;

            if (MatHangCount > DSCTPXCount)
            {
                ((State)DataContext).DanhSachChiTietPhieuXuat
                    .Add(new CTPX()
                    {
                        MAHANG = -1,
                        SOLUONG = 1
                    }
                );
            }
            Update();
        }

        private void Update()
        {
            ((State)DataContext).Update(); 
        }
        private void ComboBoxUpdate(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void Update(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        private void LapPhieuXuat(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Khi đã tạo không thể thay đổi", "Xác nhận", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Update();
                if (new PhieuXuatBUS().InsertPhieuXuat(
                    (DataContext as State).CTPXs,
                    (DataContext as State).PhieuXuatHang)
                )
                {
                    MessageBox.Show("Đã thêm thành công");
                    DialogResult = true;
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }


        //private void ThemPhieuXuat(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("Khi đã tạo không thể thay đổi", "Xác nhận", MessageBoxButton.YesNo);

        //    if (result == MessageBoxResult.Yes)
        //    {
        //        CTPX[] arr_ctpx = listCTPX.ToArray();
        //        decimal tongtien = 0;
        //        foreach (CTPX ct in arr_ctpx)
        //        {
        //            tongtien += (decimal)(listMatHang.ToList().Find(x => x.MAHANG == ct.MAHANG).DONGIA * ct.SOLUONG);
        //        }

        //        PHIEUXUATHANG px = new PHIEUXUATHANG()
        //        {
        //           `
        //        };
        //        px.CONLAI = tongtien - px.SOTIENTRA;
        //        if (px.CONLAI < 0) px.CONLAI = 0;
        //        px.NGUOIXUAT = 1;

        //        ///if sotientra !=0 nhảy sang tab thu tiền
        //        if (pxbus.InsertPhieuXuat(arr_ctpx, px))
        //        {
        //            MessageBox.Show("Đã thêm thành công");
        //            Vwdl.SONO += px.CONLAI;
        //            DialogResult = true;
        //        }
        //        else
        //            MessageBox.Show("Có lỗi xảy ra");
        //    }

        //}

        //private void InitialData()
        //{                        
        //    // tab xuất hàng
        //    listMatHang = mhbus.GetAllMatHang();
        //    listCTPX = new ObservableCollection<CTPX>();
        //    listUserControl = new ObservableCollection<CTPXUserControl>();
        //    CreateCTPX();
        //}

        #region Report

        //private void OpenReport(object sender, RoutedEventArgs e)
        //{
        //    //ReportPreview rp = new ReportPreview();
        //    //rp.Show();
        //}

        #endregion

        //private void AddCTPX(object sender, RoutedEventArgs e)
        //{
        //    CreateCTPX();
        //    svCTPX.ScrollToBottom();
        //    if (listCTPX.Count >= listMatHang.Count)
        //        btnAddCTPX.IsEnabled = false;
        //}

        #region add
        //void CreateCTPX()
        //{
        //    // add user control
        //    CTPXUserControl ctpxuc = new CTPXUserControl();
        //    spCTPX.Children.Add(ctpxuc);

        //    //get cbb data
        //    ctpxuc.cbb.ItemsSource = listMatHang;
        //    ctpxuc.cbb.DisplayMemberPath = "TENHANG";
        //    ctpxuc.cbb.SelectedValuePath = "MAHANG";

        //    //create a ctpx and add it to list
        //    CTPX ct = new CTPX()
        //    {
        //        SOLUONG = 1
        //    };
        //    listCTPX.Add(ct);

        //    //bind MAHANG to item
        //    var binding = new Binding()
        //    {
        //        Source = ct,
        //        Path = new PropertyPath("MAHANG")
        //    };
        //    ctpxuc.cbb.SetBinding(ComboBox.SelectedValueProperty, binding);

        //    //bind SOLUONG
        //    var bindSOLUONG = new Binding()
        //    {
        //        Source = ct,
        //        Path = new PropertyPath("SOLUONG")
        //    };
        //    ctpxuc.sl.SetBinding(TextBox.TextProperty, bindSOLUONG);
        //} 
        #endregion




        // to do
        // CRUD PX-CTPX 2 trong 1
        // hàng nào chọn rồi thì hủy trong list danh sách cbb sau, ko cho chọn nữa
        // if (số tiền trả > 0) then nhảy sang tab tạo phiếu thu tiền

    }
}
