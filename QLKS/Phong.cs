using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace QLKS
{
    public partial class Phong : Form
    {
        SqlConnection cnn;

        public Phong()
        {
            InitializeComponent();
        }
        private void KetNoi()
        {
            cnn = new SqlConnection(@"Data Source=LAPTOP-H6Q3TTJ9\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
            cnn.Open();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Phong_Load(object sender, EventArgs e)
        {
            this.dsPhong.DefaultCellStyle.ForeColor = Color.Black;
            KetNoi();
            P();
        }
        public void Xoa()
        {
            txtID.Clear();
            cbLoaiPhong.Text = "";
            cbKieuPhong.Text = "";
            txtDonGia.Clear();
            cbTinhTrang.Text = "";
        }
        public void P()
        {
            SqlCommand cmd = new SqlCommand("select * from Phong", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsPhong.DataSource = dt;
        }

        private void dsPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dsPhong.Rows[e.RowIndex];
            txtID.Text = Convert.ToString(row.Cells["Column1"].Value);
            cbLoaiPhong.Text = Convert.ToString(row.Cells["Column2"].Value);
            cbKieuPhong.Text = Convert.ToString(row.Cells["Column3"].Value);
            txtDonGia.Text = Convert.ToString(row.Cells["Column4"].Value);
            cbTinhTrang.Text = Convert.ToString(row.Cells["Column5"].Value);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Phong where IdP like N'%" + txtTimKiem.Text + "%' or TinhTrang like N'%" + txtTimKiem.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsPhong.DataSource = dt;
            txtTimKiem.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtID.Text)) || (string.IsNullOrEmpty(cbLoaiPhong.Text)) || (string.IsNullOrEmpty(cbKieuPhong.Text)) || (string.IsNullOrEmpty(txtDonGia.Text)) || (string.IsNullOrEmpty(cbTinhTrang.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into Phong values ('" + txtID.Text + "', N'" + cbLoaiPhong.Text + "', N'" + cbKieuPhong.Text + "', '" + txtDonGia.Text + "', N'" + cbTinhTrang.Text + "')", cnn);
                MessageBox.Show("Thêm phòng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();
                Xoa();
                P();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update Phong set  LoaiP=N'" + cbLoaiPhong.Text + "', KieuP=N'" + cbKieuPhong.Text + "', DonGia='" + txtDonGia.Text + "', TinhTrang=N'" + cbTinhTrang.Text + "' where IdP='" + txtID.Text + "'", cnn);
            MessageBox.Show("Cập nhật phòng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.ExecuteNonQuery();
            Xoa();
            P();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete Phong where IdP='" + txtID.Text + "'", cnn);
                    MessageBox.Show("Xóa phòng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.ExecuteNonQuery();
                    Xoa();
                    P();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập ID phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
