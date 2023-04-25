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
    public partial class DichVu : Form
    {
        SqlConnection cnn;
        public DichVu()
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
            Application.Exit();
        }

        private void DichVu_Load(object sender, EventArgs e)
        {
            this.dsDichVu.DefaultCellStyle.ForeColor = Color.Black;
            KetNoi();
            DV();
        }
        public void Xoa()
        {
            txtID.Clear();
            txtTen.Clear();
            txtDonGia.Clear();
        }
        public void DV()
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsDichVu.DataSource = dt;
        }

        private void dsDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dsDichVu.Rows[e.RowIndex];
            txtID.Text = Convert.ToString(row.Cells["Column1"].Value);
            txtTen.Text = Convert.ToString(row.Cells["Column2"].Value);
            txtDonGia.Text = Convert.ToString(row.Cells["Column3"].Value);
        }

        private void btnTimKiem_Click(object sender, EventArgs e) // Tìm kiếm Tên Dịch vụ
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu where TenDv like N'%" + txtTimKiem.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsDichVu.DataSource = dt;
            txtTimKiem.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtID.Text)) || (string.IsNullOrEmpty(txtTen.Text)) || (string.IsNullOrEmpty(txtDonGia.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into DichVu values ('" + txtID.Text + "', N'" + txtTen.Text + "', '" + txtDonGia.Text + "')", cnn);
                MessageBox.Show("Thêm dịch vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();
                Xoa();
                DV();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update DichVu set  TenDv=N'" + txtTen.Text + "', DonGia=N'" + txtDonGia.Text + "' where IdDv='" + txtID.Text + "'", cnn);
            MessageBox.Show("Cập nhật dịch vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.ExecuteNonQuery();
            Xoa();
            DV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete DichVu where IdDv='" + txtID.Text + "'", cnn);
                    MessageBox.Show("Xóa dịch vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.ExecuteNonQuery();
                    Xoa();
                    DV();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập ID dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
