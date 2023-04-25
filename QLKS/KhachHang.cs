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
    public partial class KhachHang : Form
    {
        SqlConnection cnn;
        public KhachHang()
        {
            InitializeComponent();
        }
        private void KetNoi()
        {
            cnn = new SqlConnection(@"Data Source=LAPTOP-H6Q3TTJ9\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
            cnn.Open();
        }
        private void KhachHang_Load(object sender, EventArgs e)
        {
            this.dsKhachHang.DefaultCellStyle.ForeColor = Color.Black;
            KetNoi();
            KH();
            Combo();
        }
        public void Xoa()
        {
            txtID.Clear();
            txtHoTen.Clear();
            cbGioiTinh.Text = "";
            txtSdt.Text = "";
            txtDiaChi.Clear();
            cbQuocGia.Text = "";
            cbIDP.Text = "";
        }
        public void KH()
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsKhachHang.DataSource = dt;
        }
        public void Combo()
        {
            SqlCommand cmd = new SqlCommand("select * from Phong where IdP not in (select IdP from KhachHang)", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            cbIDP.DataSource = dt;
            cbIDP.DisplayMember = "IdP";
            cbIDP.ValueMember = "IdP";
        }
        private void dsKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dsKhachHang.Rows[e.RowIndex];
            txtID.Text = Convert.ToString(row.Cells["Column1"].Value);
            txtHoTen.Text = Convert.ToString(row.Cells["Column2"].Value);
            dateNgaySinh.Text = Convert.ToString(row.Cells["Column3"].Value);
            cbGioiTinh.Text = Convert.ToString(row.Cells["Column4"].Value);
            txtSdt.Text = Convert.ToString(row.Cells["Column5"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["Column6"].Value);
            cbQuocGia.Text = Convert.ToString(row.Cells["Column7"].Value);
            cbIDP.Text = Convert.ToString(row.Cells["Column8"].Value);
            dateNgayDen.Text = Convert.ToString(row.Cells["Column9"].Value);
            dateNgayDi.Text = Convert.ToString(row.Cells["Column10"].Value);
        }
        public void UpdateDH()
        {
            SqlCommand cmd = new SqlCommand("update Phong set TinhTrang=N'" + "Đã thuê" + "' where IdP in (select IdP from KhachHang)", cnn);
            cmd.ExecuteNonQuery();
        }
        public void UpdateCT()
        {
            SqlCommand cmd = new SqlCommand("update Phong set TinhTrang=N'" + "Còn trống" + "' where IdP not in (select IdP from KhachHang)", cnn);
            cmd.ExecuteNonQuery();
        }
        private void btnTimKiem_Click(object sender, EventArgs e) // Tìm kiếm Tên khách hàng và ID phòng
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang where TenKh like N'%" + txtTimKiem.Text + "%' or IdP like '%" + txtTimKiem.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsKhachHang.DataSource = dt;
            txtTimKiem.Clear();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            DateTime NgayDen = Convert.ToDateTime(dateNgayDen.Text);
            DateTime NgayDi = Convert.ToDateTime(dateNgayDi.Text);
            if ((string.IsNullOrEmpty(txtID.Text)) || (string.IsNullOrEmpty(txtHoTen.Text)) || (string.IsNullOrEmpty(cbGioiTinh.Text)) || (string.IsNullOrEmpty(txtSdt.Text)) || (string.IsNullOrEmpty(txtDiaChi.Text)) || (string.IsNullOrEmpty(cbQuocGia.Text)) || (string.IsNullOrEmpty(cbIDP.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (NgayDi < NgayDen)
            {
                MessageBox.Show("Ngày tháng năm không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into KhachHang values ('" + txtID.Text + "', N'" + txtHoTen.Text + "', '" + dateNgaySinh.Text + "', N'" + cbGioiTinh.Text + "', '" + txtSdt.Text + "', N'" + txtDiaChi.Text + "', N'" + cbQuocGia.Text + "', '" + cbIDP.Text + "', '" + dateNgayDen.Text + "', '" + dateNgayDi.Text + "')", cnn);
                MessageBox.Show("Thêm khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();
                Xoa();
                KH();
                UpdateDH();
                UpdateCT();
                Combo();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DateTime NgayDen = Convert.ToDateTime(dateNgayDen.Text);
            DateTime NgayDi = Convert.ToDateTime(dateNgayDi.Text);
            if (NgayDi < NgayDen)
            {
                MessageBox.Show("Ngày tháng năm không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update KhachHang set TenKh=N'" + txtHoTen.Text + "', NgaySinh='" + dateNgaySinh.Text + "', GioiTinh=N'" + cbGioiTinh.Text + "', Sdt='" + txtSdt.Text + "', DiaChi=N'" + txtDiaChi.Text + "', QuocGia=N'" + cbQuocGia.Text + "', IdP='" + cbIDP.Text + "', NgayDen='" + dateNgayDen.Text + "', NgayDi='" + dateNgayDi.Text + "' where IdKh='" + txtID.Text + "'", cnn);
                MessageBox.Show("Cập nhật khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();
                Xoa();
                KH();
                UpdateDH();
                UpdateCT();
                Combo();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete KhachHang where IdKh='" + txtID.Text + "'", cnn);
                    MessageBox.Show("Xóa khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.ExecuteNonQuery();
                    Xoa();
                    KH();
                    UpdateDH();
                    UpdateCT();
                    Combo();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
