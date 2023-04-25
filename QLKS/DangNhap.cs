﻿using System;
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
    public partial class DangNhap : Form
    {
        SqlConnection cnn;
        public DangNhap()
        {
            InitializeComponent();
        }
        private void KetNoi()
        {
            cnn = new SqlConnection(@"Data Source=LAPTOP-H6Q3TTJ9\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
            cnn.Open();
        }
        private void DangNhap_Load(object sender, EventArgs e)
        {
            KetNoi();
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtTenTK.Text)) || (string.IsNullOrEmpty(txtMatKhau.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from TaiKhoan where TenTk='" + txtTenTK.Text + "' and MatKhau='" + txtMatKhau.Text + "'", cnn);
                SqlDataReader dt = cmd.ExecuteReader();
                if (dt.Read())
                {
                    QuanLyKhachSan q = new QuanLyKhachSan();
                    q.Show();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
            txtTenTK.Clear();
            txtMatKhau.Clear();
        }

        private void lbThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
