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
    public partial class DangNhap : Form
    {
        SqlConnection cnn = new SqlConnection(@"Data Source=HELLO\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
        public DangNhap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if ((string.IsNullOrEmpty(textBox1.Text)) || (string.IsNullOrEmpty(textBox2.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from TaiKhoan where TenTk='" + textBox1.Text + "' and MatKhau='" + textBox2.Text + "'", cnn);
                SqlDataReader dt = cmd.ExecuteReader();
                if (dt.Read())
                {
                    QuanLyKhachSan q = new QuanLyKhachSan();
                    q.Show();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
            cnn.Close();
            */
            QuanLyKhachSan q = new QuanLyKhachSan();
            q.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
