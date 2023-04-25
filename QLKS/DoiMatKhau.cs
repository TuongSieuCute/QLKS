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
    public partial class DoiMatKhau : Form
    {
        SqlConnection cnn;
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        private void KetNoi()
        {
            cnn = new SqlConnection(@"Data Source=LAPTOP-H6Q3TTJ9\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
            cnn.Open();
        }
        private void lbThoat_Click(object sender, EventArgs e)
        {
            QuanLyKhachSan q = new QuanLyKhachSan();
            this.Hide();
            q.ShowDialog();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            KetNoi();
        }
        public void Xoa()
        {
            txtTen.Clear();
            txtCu.Clear();
            txtMoi.Clear();
            txtLai.Clear();
        }
        public void UpdateTK()
        {
            SqlCommand cmd = new SqlCommand("update TaiKhoan set MatKhau=N'" + txtLai.Text + "' where TenTk='" + txtTen.Text + "'", cnn);
            cmd.ExecuteNonQuery();
        }
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if((string.IsNullOrEmpty(txtTen.Text)) || (string.IsNullOrEmpty(txtCu.Text)) || (string.IsNullOrEmpty(txtMoi.Text)) || (string.IsNullOrEmpty(txtLai.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtCu.Text == txtMoi.Text)
            {
                MessageBox.Show("Mật khẩu cũ trùng với mật khẩu mới. Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtMoi.Text != txtLai.Text)
            {
                MessageBox.Show("Mật khẩu mới không đúng. Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                SqlCommand cmd = new SqlCommand("select * from TaiKhoan where TenTk='" + txtTen.Text + "' and MatKhau='" + txtCu.Text + "'", cnn);
                SqlDataReader dt = cmd.ExecuteReader();
                if (dt.Read())
                {
                    dt.Close();
                    UpdateTK();
                    DangNhap d = new DangNhap();
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    d.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Xoa();
        }
    }
}
