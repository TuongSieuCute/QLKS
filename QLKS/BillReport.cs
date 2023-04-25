using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class BillReport : Form
    {
        public BillReport()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-H6Q3TTJ9\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
        private void BillReport_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT IdHd FROM HoaDon",cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            ad.Fill(dt1);
            comboBox1.DataSource= dt1;
            comboBox1.DisplayMember= "IdHd";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("SELECT SuDungDichVu.Tien FROM SuDungDichVu, HoaDon WHERE SuDungDichVu.Idp=HoaDon.IdP AND HoaDon.IdHd='" + comboBox1.Text + "'",cnn);
            cnn.Open();
            object checkNull = cm.ExecuteScalar();
            if (checkNull == null || checkNull == DBNull.Value)
            {
                cm = new SqlCommand("SELECT DISTINCT Phong.DonGia AS Expr1, HoaDon.phuThu, HoaDon.Tiencanthanhtoan, HoaDon.IdP FROM HoaDon, Phong WHERE IdHd='" + comboBox1.Text + "' AND HoaDon.Idp = Phong.Idp", cnn);
                cnn.Close();
            }

            else
            {
                cm = new SqlCommand("SELECT DISTINCT TenDv, DichVu.DonGia, SuDungDichVu.SoLuong, SuDungDichVu.Tien, Phong.DonGia AS Expr1, HoaDon.phuThu, HoaDon.Tiencanthanhtoan, HoaDon.IdP FROM HoaDon, DichVu, SuDungDichVu, Phong WHERE IdHd='" + comboBox1.Text + "' AND HoaDon.Idp = SuDungDichVu.Idp AND SuDungDIchVu.IdDv = DichVu.IdDv AND HoaDon.Idp = Phong.Idp", cnn);
                cnn.Close();
            }
            SqlDataAdapter d = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            d.Fill(dt);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.ReportPath = "BillReport.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }

        private void lbThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyKhachSan q = new QuanLyKhachSan();
            q.ShowDialog();
        }
    }
}
