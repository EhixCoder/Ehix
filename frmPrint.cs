using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_BLUEPRINTE
{
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        public DataTable ReportData { get; set; }
        public string ReportFile { get; set; }
        public string DataSetName { get; set; }
        public int ZoomLevel { get; set; }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            if (ReportData != null)
            {
                try
                {
                    var local_report = this.reportViewer1.LocalReport;
                    //string reportPath = ReportFile == "" ? Application.StartupPath + "\receipt.rdlc" : ReportFile;

                    //if (!System.IO.File.Exists(reportPath))
                    //{
                    //    reportPath = ReportFile == ""? "../receipt.rdlc": ReportFile;
                    //}

                    local_report.ReportPath = this.ReportFile;

                    //MessageBox.Show(reportPath);

                    //local_report.ReportPath = @"C:\Users\EHIXLAMB\Documents\visual studio 2015\Projects\CS_BLUEPRINTE\CS_BLUEPRINTE\receipt.rdlc";
                    local_report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(DataSetName, ReportData));
                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                    this.reportViewer1.ZoomPercent = ZoomLevel;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
