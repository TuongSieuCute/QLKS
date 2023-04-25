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
    public partial class HoaDon : Form
    {
        SqlConnection cnn;
        public HoaDon()
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

        private void HoaDon_Load(object sender, EventArgs e)
        {
            this.dsHoaDon.DefaultCellStyle.ForeColor = Color.Black;
            KetNoi();
            HD();
            Combo();
            cbIDPhong_SelectionChangeCommitted(sender, e);
        }
        public void Xoa()
        {
            txtIDHoaDon.Clear();
            txtIDKhachHang.Clear();
            cbIDPhong.ResetText();
            dateTimePicker1.ResetText();
            txtPhuThu.Clear();
        }
        public void Combo()
        {
            SqlCommand cm = new SqlCommand("Select IdP from Phong where Idp in (select IdP from khachhang)",cnn);
            SqlDataAdapter a = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            a.Fill(dt);
            cbIDPhong.DataSource= dt;
            cbIDPhong.DisplayMember= "IdP";
        }
        public void HD()
        {
            SqlCommand cmd = new SqlCommand("select IdHd, IdKh, IdP, NgayThanhToan, PhuThu from HoaDon", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsHoaDon.DataSource = dt;
        }

        private void dsHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dsHoaDon.Rows[e.RowIndex];
            txtIDHoaDon.Text = Convert.ToString(row.Cells["Column1"].Value);
            txtIDKhachHang.Text = Convert.ToString(row.Cells["Column2"].Value);
            cbIDPhong.Text = Convert.ToString(row.Cells["Column3"].Value);
            dateTimePicker1.Text = Convert.ToString(row.Cells["Column4"].Value);
            txtPhuThu.Text = Convert.ToString(row.Cells["Column5"].Value);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from HoaDon where IdHd like N'%" + txtTimKiem.Text + "%'", cnn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dsHoaDon.DataSource = dt;
            txtTimKiem.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtIDHoaDon.Text)) || (string.IsNullOrEmpty(txtIDKhachHang.Text)) || (string.IsNullOrEmpty(cbIDPhong.Text))
                || (string.IsNullOrEmpty(txtPhuThu.Text)))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT SUM(Tien) FROM SuDungDichVu WHERE IdP = '" + cbIDPhong.Text + "'", cnn);
                object checkNull = cmd.ExecuteScalar();
                long tienDichVu;
                if (checkNull == null || checkNull == DBNull.Value)
                {
                    tienDichVu = 0;
                }    
                   
                else
                    tienDichVu = Int64.Parse(cmd.ExecuteScalar().ToString());
                    cmd = new SqlCommand("SELECT DonGia FROM Phong WHERE IdP='" + cbIDPhong.Text + "'", cnn);
                    long tienPhong = Convert.ToInt64(cmd.ExecuteScalar());

                    long tienThanhToan = Int64.Parse(txtPhuThu.Text) + tienDichVu + tienPhong;
                    cmd = new SqlCommand("INSERT INTO HoaDon VALUES ('"+ txtIDHoaDon.Text +"', '" + txtIDKhachHang.Text + "', '" + cbIDPhong.Text + "', '" + dateTimePicker1.Text + "', '" + txtPhuThu.Text + "','" + tienThanhToan.ToString() + "')", cnn);
                    MessageBox.Show("Thêm hoá đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.ExecuteNonQuery();
                
                    HD();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtIDHoaDon.Text)) || (string.IsNullOrEmpty(txtIDKhachHang.Text)) || (string.IsNullOrEmpty(cbIDPhong.Text))
                || (string.IsNullOrEmpty(txtPhuThu.Text)))
            {
                HD();
            }else
            {
                SqlCommand cmd = new SqlCommand("SELECT SUM(Tien) FROM SuDungDichVu WHERE IdP = '" + cbIDPhong.Text + "'", cnn);
                object checkNull = cmd.ExecuteScalar();
                long tienDichVu;
                if (checkNull == null || checkNull == DBNull.Value)
                {
                    tienDichVu = 0;
                }

                else
                    tienDichVu = Int64.Parse(cmd.ExecuteScalar().ToString());
                cmd = new SqlCommand("SELECT SUM(Tien) FROM SuDungDichVu WHERE IdP = '" + cbIDPhong.Text + "'", cnn);
                

                cmd = new SqlCommand("SELECT DonGia FROM Phong WHERE IdP='" + cbIDPhong.Text + "'", cnn);
                long tienPhong = Convert.ToInt64(cmd.ExecuteScalar());
                
                long tienThanhToan = Int64.Parse(txtPhuThu.Text) + tienDichVu + tienPhong;
                cmd = new SqlCommand("update HoaDon set  IdP='" + cbIDPhong.Text + "', NgayThanhToan='" + dateTimePicker1.Text + "', PhuThu='" + txtPhuThu.Text + "', TienCanThanhToan='" + tienThanhToan.ToString() + "' where IdKh='" + txtIDKhachHang.Text + "'", cnn);
                
                MessageBox.Show("Cập nhật hoá đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();
                Xoa();
                HD();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtIDHoaDon.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete HoaDon where IdHd='" + txtIDHoaDon.Text + "'", cnn);
                    MessageBox.Show("Xóa hoá đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.ExecuteNonQuery();
                    Xoa();
                    HD();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập ID hoá đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            BillReport billReportForm = new BillReport();
            billReportForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SuDungDichVu suDungDichVuForm = new SuDungDichVu();
            suDungDichVuForm.ShowDialog();

        }

        private void cbIDPhong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("SELECT IdKh  FROM KhachHang WHERE IdP='" + cbIDPhong.Text + "'", cnn);
            SqlDataReader rd = cm.ExecuteReader();
            if (rd.Read())
            {
                txtIDKhachHang.Text = rd["IdKh"].ToString();
            }
            rd.Close();
        }
    }
}

