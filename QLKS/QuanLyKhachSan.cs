using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class QuanLyKhachSan : Form
    {
        public QuanLyKhachSan()
        {
            InitializeComponent();
        }
        
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            panel3.Height = pictureBox1.Height;
            panel3.Top = pictureBox1.Top;

            panelChinh.Controls.Clear();
            panelChinh.Controls.Add(panelHinh);
        }
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            panel3.Height = btnKhachHang.Height;
            panel3.Top = btnKhachHang.Top;

            panelChinh.Controls.Clear();
            KhachHang a = new KhachHang();
            a.TopLevel = false;
            a.Dock = DockStyle.Fill;
            a.FormBorderStyle = FormBorderStyle.None;
            panelChinh.Controls.Add(a);
            a.Show();
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            panel3.Height = btnPhong.Height;
            panel3.Top = btnPhong.Top;

            panelChinh.Controls.Clear();
            Phong a = new Phong();
            a.TopLevel = false;
            a.Dock = DockStyle.Fill;
            a.FormBorderStyle = FormBorderStyle.None;
            panelChinh.Controls.Add(a);
            a.Show();
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            panel3.Height = btnDichVu.Height;
            panel3.Top = btnDichVu.Top;

            panelChinh.Controls.Clear();
            DichVu a = new DichVu();
            a.TopLevel = false;
            a.Dock = DockStyle.Fill;
            a.FormBorderStyle = FormBorderStyle.None;
            panelChinh.Controls.Add(a);
            a.Show();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            panel3.Height = btnHoaDon.Height;
            panel3.Top = btnHoaDon.Top;

            panelChinh.Controls.Clear();
            HoaDon a = new HoaDon();
            a.TopLevel = false;
            a.Dock = DockStyle.Fill;
            a.FormBorderStyle = FormBorderStyle.None;
            panelChinh.Controls.Add(a);
            a.Show();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            panelHinh.Hide();
            DoiMatKhau d = new DoiMatKhau();
            d.ShowDialog();
        }

        private void lbThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
