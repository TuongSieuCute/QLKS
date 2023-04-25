using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLKS
{
    public partial class DatPhong : Form
    {
        SqlConnection cnn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
        public DatPhong()
        {
            InitializeComponent();
        }
        
        private void DatPhong_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            Dat();
        }
        public void Xoa()
        {
            textBox1.Clear();
            cbKh.Text = " ";
            cbP.Text = " ";
        }
        public void Dat()
        {
            SqlCommand cmd = new SqlCommand("select * from DatPhong", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            SqlDataAdapter da1 = new SqlDataAdapter("select * from KhachHang", cnn);
            DataTable dt1=new DataTable();
            da1.Fill(dt1);
            cbKh.DataSource = dt1;
            cbKh.DisplayMember = dt1.Columns["TenKh"].ToString();
            cbKh.ValueMember = dt1.Columns["IdKh"].ToString();

            SqlDataAdapter da2 = new SqlDataAdapter("select * from Phong where TinhTrang=N'" + "Còn trống" + "'", cnn);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cbP.DataSource = dt2;
            cbP.DisplayMember = dt2.Columns["TenP"].ToString();
            cbP.ValueMember = dt2.Columns["IdP"].ToString();
        }

        private void button1_Click_1(object sender, EventArgs e) // Tìm kiếm phiếu
        {
            SqlCommand cmd = new SqlCommand("select * from DatPhong where TenDp like N'%" + textBox2.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e) // Thêm phiếu
        {
            if (textBox1.Text != "")
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into DatPhong values ('" + textBox1.Text + "', N'" + cbKh.Text + "', N'" + cbP.Text + "', '" + dateTimePicker1.Text + "', '" + dateTimePicker2.Text + "')", cnn);
                cmd.ExecuteNonQuery();
                Xoa();
                Dat();
            }
            else
            {
                MessageBox.Show("Vui lòng điền ID phiếu");
            }
            cnn.Close();
        }

        private void button3_Click(object sender, EventArgs e) // Sửa phiếu
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("update DatPhong set NgayDen='" + dateTimePicker1.Text + "', NgayDi='" + dateTimePicker2.Text + "' where IdDp='" + textBox1.Text + "'", cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
            Xoa();
            Dat();
        }

        private void button4_Click(object sender, EventArgs e) // Xóa phiếu
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("delete DatPhong where IdDp='" + textBox1.Text + "'", cnn);
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    Xoa();
                    Dat();
                }
            }
        }
    }
}
