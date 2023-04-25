using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QLKS
{
    public partial class QuanLyKhachSan : Form
    {
        SqlConnection cnn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
        
        public QuanLyKhachSan()
        {
            InitializeComponent();
        }
        private void QuanLyKhachSan_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView3.DefaultCellStyle.ForeColor = Color.Black;
            KhachHang();
            DatPhong();
            Phong();
            DichVu();
        }
        public void Xoa()
        {
            txtid.Clear();
            txtht.Clear();
            cbgt.Text = " ";
            txtdc.Clear();
            cbqg.Text = " ";

            txtidp.Clear();
            txttp.Clear();
            cblp.Text = " ";
            cbkp.Text = " ";
            txtdg.Clear();
            cbtt.Text = " ";

            txtiddv.Clear();
            txttdv.Clear();
            txtdgdv.Clear();
        }
        //Khách hàng
        public void KhachHang()
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView1.Rows[e.RowIndex];
            txtid.Text = Convert.ToString(row.Cells["IdKh"].Value);
            txtht.Text = Convert.ToString(row.Cells["TenKh"].Value);
            dns.Text = Convert.ToString(row.Cells["NgaySinh"].Value);
            cbgt.Text = Convert.ToString(row.Cells["GioiTinh"].Value);
            txtdc.Text = Convert.ToString(row.Cells["DiaChi"].Value);
            cbqg.Text = Convert.ToString(row.Cells["QuocGia"].Value);
            dnt.Text = Convert.ToString(row.Cells["NgayThue"].Value);
        }   
        private void button1_Click_1(object sender, EventArgs e) // Tìm kiếm khách hàng
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang where TenKh like N'%" + textBox4.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            textBox4.Clear();
        }
        private void button2_Click_1(object sender, EventArgs e) // Thêm khách hàng
        {
            if (txtid.Text != "")
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into KhachHang values ('" + txtid.Text + "', N'" + txtht.Text + "', '" + dns.Text + "', N'" + cbgt.Text + "', N'" + txtdc.Text + "', N'" + cbqg.Text + "', '" + dnt.Text + "')", cnn);
                cmd.ExecuteNonQuery();
                Xoa();
                KhachHang();
            }
            else
            {
                MessageBox.Show("Vui lòng điền ID khách hàng");
            }
            cnn.Close();
        }
        private void button3_Click_1(object sender, EventArgs e) // Sửa khách hàng
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("update KhachHang set TenKh=N'" + txtht.Text + "', NgaySinh='" + dns.Text + "', GioiTinh=N'" + cbgt.Text + "', DiaChi=N'" + txtdc.Text + "', QuocGia=N'" + cbqg.Text + "', NgayThue='" + dnt.Text + "' where IdKh='" + txtid.Text + "'", cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
            Xoa();
            KhachHang();
        }
        private void button4_Click_1(object sender, EventArgs e) // Xóa khách hàng
        {
            if (txtid.Text != "")
            {
                if(MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("delete KhachHang where IdKh='" + txtid.Text + "'", cnn);
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    Xoa();
                    KhachHang();
                }
            }
        }
        
        // Phòng
        public void Phong()
        {
            SqlCommand cmd = new SqlCommand("select * from Phong", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView2.Rows[e.RowIndex];
            txtidp.Text = Convert.ToString(row.Cells["IdP"].Value);
            txttp.Text = Convert.ToString(row.Cells["TenP"].Value);
            cblp.Text = Convert.ToString(row.Cells["LoaiP"].Value);
            cbkp.Text = Convert.ToString(row.Cells["KieuP"].Value);
            txtdg.Text = Convert.ToString(row.Cells["DonGia"].Value);
            cbtt.Text = Convert.ToString(row.Cells["TinhTrang"].Value);
        }
        private void button5_Click_1(object sender, EventArgs e) // Tìm kiếm phòng
        {
            SqlCommand cmd = new SqlCommand("select * from Phong where TenP like N'%" + textBox5.Text + "%' or TinhTrang like N'%" + textBox5.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView2.DataSource = dt;
            textBox5.Clear();
        }

        private void button6_Click_1(object sender, EventArgs e) // Thêm phòng
        {
            if (txtidp.Text != "")
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into Phong values ('" + txtidp.Text + "', '" + txttp.Text + "', N'" + cblp.Text + "', N'" + cbkp.Text + "', '" + txtdg.Text + "', N'" + cbtt.Text + "')", cnn);
                cmd.ExecuteNonQuery();
                Xoa();
                Phong();
            }
            else
            {
                MessageBox.Show("Vui lòng điền ID phòng");
            }
            cnn.Close();
        }

        private void button7_Click_1(object sender, EventArgs e) // Sửa phòng
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("update Phong set TenP=N'" + txttp.Text + "', LoaiP=N'" + cblp.Text + "', KieuP=N'" + cbkp.Text + "', DonGia='" + txtdg.Text + "', TinhTrang=N'" + cbtt.Text + "' where IdP='" + txtidp.Text + "'", cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
            Xoa();
            Phong();
        }

        private void button8_Click_1(object sender, EventArgs e) // Xóa phòng
        {
            if (txtidp.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("delete Phong where IdP='" + txtidp.Text + "'", cnn);
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    Xoa();
                    Phong();
                }
            }
        }
        // Dịch vụ
        public void DichVu()
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView3.DataSource = dt;
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView3.Rows[e.RowIndex];
            txtiddv.Text = Convert.ToString(row.Cells["IdDv"].Value);
            txttdv.Text = Convert.ToString(row.Cells["TenDv"].Value);
            txtdgdv.Text = Convert.ToString(row.Cells["DonGia"].Value);
        }
        private void button10_Click(object sender, EventArgs e) // Tìm kiếm dịch vụ
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu where TenDv like N'%" + textBox6.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView3.DataSource = dt;
            textBox6.Clear();
        }

        private void button11_Click(object sender, EventArgs e) // Thêm dịch vụ
        {
            if (txtiddv.Text != "")
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into DichVu values ('" + txtiddv.Text + "', N'" + txttdv.Text + "', '" + txtdgdv.Text + "')", cnn);
                cmd.ExecuteNonQuery();
                Xoa();
                DichVu();
            }
            else
            {
                MessageBox.Show("Vui lòng điền ID phòng");
            }
            cnn.Close();
        }

        private void button12_Click(object sender, EventArgs e) // Sửa dịch vụ
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("update DichVu set TenDv=N'" + txttdv.Text + "', DonGia='" + txtdgdv.Text + "' where IdDv='" + txtiddv.Text + "'", cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
            Xoa();
            DichVu();
        }

        private void button13_Click(object sender, EventArgs e) // Xóa dịch vụ
        {
            if (txtiddv.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("delete DichVu where IdDv='" + txtiddv.Text + "'", cnn);
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    Xoa();
                    DichVu();
                }
            }
        }
        // Đặt phòng
        public void DatPhong()
        {
            /*
            cnn.Open();
            SqlCommand cmd = new SqlCommand("exec TinhTrang 'P101'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter();
            SqlDataReader re = cmd.ExecuteReader();
            while (re.Read())
            {
                if (re.GetValue(0).ToString() == "Còn trống")
                {
                    btn105.BackColor = Color.Green;
                }
                else if (re.GetValue(0).ToString() == "Đang sửa chữa")
                {
                    btn105.BackColor = Color.Orange;
                }
                else
                {
                    btn105.BackColor = Color.Red;
                }
            }
            cnn.Close();
            */
        }
        private void btn101_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Red;
            ctrl.Enabled = false; 
        }
    }
}
