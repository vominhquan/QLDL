using System;
using System.Collections.Generic;
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
    /// Interaction logic for ReportPreview.xaml
    /// </summary>
    /// 
    public partial class ReportPreview : Window
    {
        private bool _isReportViewerLoaded;

        public ReportPreview()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                this._reportViewer.LocalReport.ReportEmbeddedResource = "QLDL.Report.HoSoDaiLy.rdlc";
                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        } 

    }
}
