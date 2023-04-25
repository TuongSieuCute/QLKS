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
    public partial class SuDungDichVu : Form
    {
        SqlConnection cnn;
        public SuDungDichVu()
        {
            InitializeComponent();
        }
        private void KetNoi()
        {
            cnn = new SqlConnection(@"Data Source=LAPTOP-H6Q3TTJ9\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True");
            cnn.Open();
        }
        private void SuDungDichVu_Load(object sender, EventArgs e)
        {
            this.dsSDDV.DefaultCellStyle.ForeColor = Color.Black;
            KetNoi();
            SDDV();
            Combo();
        }
        public void Xoa()
        {
            cbIDPhong.ResetText();
            cbTenDV.ResetText();
            txtDonGia.Clear();
            txtIDDV.Clear();
            txtSoLuong.Clear();
        }
        public void SDDV()
        {
            SqlCommand cmd = new SqlCommand("select TenDv, SoLuong, IdP, SuDungDichVu.IdDv, DonGia from SuDungDichVu, DichVu WHERE Sudungdichvu.IdDv = Dichvu.IdDv", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsSDDV.DataSource = dt;
        }
        public void Combo()
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            cbTenDV.DataSource = dt;
            cbTenDV.DisplayMember = "TenDv";
            cbTenDV.ValueMember = "IdDv";
            txtIDDV.DataBindings.Clear();
            txtIDDV.DataBindings.Add("Text", cbTenDV.DataSource, "IdDv");
            txtDonGia.DataBindings.Clear();
            txtDonGia.DataBindings.Add("Text", cbTenDV.DataSource, "DonGia");

            SqlCommand cmd2 = new SqlCommand("select IdP from Phong where IdP in (select IdP from KhachHang)", cnn);
            SqlDataAdapter ad2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            ad2.Fill(dt2);
            cbIDPhong.DataSource = dt2;
            cbIDPhong.DisplayMember = "IdP";
        }

        private void dsSDDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dsSDDV.Rows[e.RowIndex];
            cbTenDV.Text = Convert.ToString(row.Cells["Column1"].Value);
            txtSoLuong.Text = Convert.ToString(row.Cells["Column2"].Value);
            cbIDPhong.Text = Convert.ToString(row.Cells["Column3"].Value);
            txtIDDV.Text = Convert.ToString(row.Cells["Column4"].Value);
            txtDonGia.Text = Convert.ToString(row.Cells["Column5"].Value);
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtSoLuong.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                long tien = Int32.Parse(txtSoLuong.Text) * Int32.Parse(txtDonGia.Text);
                SqlCommand cmd = new SqlCommand("INSERT into SuDungDichVu VALUES ('" + cbIDPhong.Text + "', '" + txtIDDV.Text + "', '" + txtSoLuong.Text + "', '" + tien.ToString() + "')", cnn);
                MessageBox.Show("Thêm sử dụng dịch vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();
                capNhatTienHoaDon(tien, cbIDPhong.Text);
                Xoa();
                SDDV();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            //tien = tong tien dich vu
            long tien = Int32.Parse(txtSoLuong.Text) * Int32.Parse(txtDonGia.Text);


            SqlCommand cmd = new SqlCommand("update SuDungDichVu set  IdP=N'" + cbIDPhong.Text + "', SoLuong='" + txtSoLuong.Text + "', Tien='" + tien.ToString() + "' where IdDv='" + txtIDDV.Text + "'", cnn);
            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.ExecuteNonQuery();
            capNhatTienHoaDon(tien, cbIDPhong.Text);
            Xoa();
            SDDV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtIDDV.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete SuDungDichVu where IdDv = '" + txtIDDV.Text + "'", cnn);
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.ExecuteNonQuery();
                    capNhatTienHoaDon(0, cbIDPhong.Text);
                    Xoa();
                    
                    SDDV();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyKhachSan q = new QuanLyKhachSan();
            q.ShowDialog();
        }
        private void capNhatTienHoaDon(long tienDichVu, string idP)
        {
            SqlCommand cmd = new SqlCommand("SELECT Phong.DonGia FROM Phong, HoaDon WHERE HoaDon.IdP=phong.Idp AND HoaDon.IdP='" + idP + "'", cnn);
            long tienPhong = Convert.ToInt64(cmd.ExecuteScalar());
            cmd = new SqlCommand("SELECT HoaDon.PhuThu FROM Phong, HoaDon WHERE HoaDon.IdP=phong.Idp AND HoaDon.Idp='" + idP + "'", cnn);
            long phuThu = Convert.ToInt64(cmd.ExecuteScalar());
            long tienCanThanhToan = tienPhong + phuThu + tienDichVu;
            cmd = new SqlCommand("UPDATE HoaDon SET TienCanThanhToan=" + tienCanThanhToan.ToString() + " WHERE HoaDon.IdP='" + idP + "'",cnn);
            cmd.ExecuteNonQuery();
        }
        
    }
}
