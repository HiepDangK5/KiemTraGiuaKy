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
using static System.Windows.Forms.AxHost;

namespace KiemTraGiuaKy
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source = LAPTOP-MT9AQIP1; Initial Catalog = QLSV_NguyenDangHiep; Trusted_Connection=True;";
        private string State = "";
        public DataSet dsMain;
        public Form1()
        {
            InitializeComponent();
            GetData();
            State = "Reset";
            SetInterface(State);
            SetSourceCombo();
            SetSourceRadioButton();
        }
        #region Manage Interface
        public void SetInterface(string State)
        {
            lblError.Text = "";
            if (State == "Reset")
            {
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnGhi.Enabled = false;
                btnHuy.Enabled = false;

                txtID.Enabled = false;
                txtTenSV.Enabled = false;
                dtpNgaySinh.Enabled = false;
                txtQueQuan.Enabled = false;
                txtDiaChi.Enabled = false;
                txtGhiChu.Enabled = false;
                cboTrinhDo.Enabled = false;
                rdoNam.Enabled = false;
                rdoNu.Enabled = false;
            }
            else if (State == "Insert")
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnGhi.Enabled = true;
                btnHuy.Enabled = true;
                btnSearch.Enabled = true;

                txtID.Enabled = true;
                txtTenSV.Enabled = true;
                dtpNgaySinh.Enabled = true;
                txtQueQuan.Enabled = true;
                txtDiaChi.Enabled = true;
                txtGhiChu.Enabled = true;
                cboTrinhDo.Enabled = true;
                rdoNam.Enabled = true;
                rdoNu.Enabled = true;

                txtID.Text = "";
                txtTenSV.Text = "";
                dtpNgaySinh.Text = "";
                txtQueQuan.Text = "";
                txtDiaChi.Text = "";
                txtGhiChu.Text = "";
                txtID.Focus();
            }
            else if (State == "Update")
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnGhi.Enabled = true;
                btnHuy.Enabled = true;

                txtID.Enabled = false;
                txtTenSV.Enabled = true;
                dtpNgaySinh.Enabled = true;
                txtQueQuan.Enabled = true;
                txtDiaChi.Enabled = true;
                txtGhiChu.Enabled = true;
                cboTrinhDo.Enabled = true;
                rdoNam.Enabled = true;
                rdoNu.Enabled = true;
                txtTenSV.Focus();
            }
        }
        #endregion
        #region Public Functions 
        public void SetSourceRadioButton()
        {
            // Tạo DataTable chứa dữ liệu giới tính
            DataTable dtGioiTinh = new DataTable();
            dtGioiTinh.Columns.Add("GIA_TRI", typeof(string));
            dtGioiTinh.Columns.Add("GIOI_TINH", typeof(string));

            dtGioiTinh.Rows.Add("0", "Nam");
            dtGioiTinh.Rows.Add("1", "Nữ");
        }

        public void SetSourceCombo()
        {
            DataTable dtTrinhDo = new DataTable();
            DataColumn colValue = new DataColumn("GIA_TRI");
            DataColumn colName = new DataColumn("TRINH_DO");

            dtTrinhDo.Columns.Add(colValue);
            dtTrinhDo.Columns.Add(colName);

            DataRow dr1 = dtTrinhDo.NewRow();
            dr1["GIA_TRI"] = "0";
            dr1["TRINH_DO"] = "Tien si";
            dtTrinhDo.Rows.Add(dr1);

            DataRow dr2 = dtTrinhDo.NewRow();
            dr2["GIA_TRI"] = "1";
            dr2["TRINH_DO"] = "Thac si";
            dtTrinhDo.Rows.Add(dr2);

            DataRow dr3 = dtTrinhDo.NewRow();
            dr3["GIA_TRI"] = "2";
            dr3["TRINH_DO"] = "Dai hoc";
            dtTrinhDo.Rows.Add(dr3);

            DataRow dr4 = dtTrinhDo.NewRow();
            dr4["GIA_TRI"] = "3";
            dr4["TRINH_DO"] = "Khac";
            dtTrinhDo.Rows.Add(dr4);

            dtTrinhDo.AcceptChanges();

            cboTrinhDo.ValueMember = "GIA_TRI";
            cboTrinhDo.DisplayMember = "TRINH_DO";
            cboTrinhDo.DataSource = dtTrinhDo;
            cboTrinhDo.SelectedIndex = 0;
        }
        private void GetData()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string sql = "SELECT * FROM tblSinhVien";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dsMain = new DataSet();

                da.Fill(dsMain);

                if (dsMain != null && dsMain.Tables.Count > 0 && dsMain.Tables[0].Rows.Count > 0)
                {
                    //DataColumn colSTT = new DataColumn("STT");
                    //dsMain.Tables[0].Columns.Add(colSTT); // thêm cột mới

                    //for (int i = 0; i < dsMain.Tables[0].Rows.Count; i++)
                    //{
                    //    dsMain.Tables[0].Rows[i]["STT"] = (i + 1).ToString();
                    //}
                    dgvSinhVien.DataSource = dsMain.Tables[0];
                    lblTongSo.Text = "Tổng số: " + dsMain.Tables[0].Rows.Count + " bản ghi";

                    txtID.Text = dsMain.Tables[0].Rows[0]["ID"].ToString();
                    txtTenSV.Text = dsMain.Tables[0].Rows[0]["TenSV"].ToString();
                    dtpNgaySinh.Text = dsMain.Tables[0].Rows[0]["NgaySinh"].ToString();
                    txtQueQuan.Text = dsMain.Tables[0].Rows[0]["QueQuan"].ToString();
                    txtDiaChi.Text = dsMain.Tables[0].Rows[0]["DiaChi"].ToString();
                    txtGhiChu.Text = dsMain.Tables[0].Rows[0]["GhiChu"].ToString();
                }
                else
                {
                    lblTongSo.Text = "Tổng số: 0 bản ghi";
                    dgvSinhVien.DataSource = null;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void Insert()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //string sql = "INSERT INTO SinhVien(MaSV,TenSV,MaLop,DiaChi) " + 
                //    "VALUES('" + txtMaSV.Text.Trim() + "',N'" + txtTen.Text.Trim() + "','74DCTT27',N'"+txtDiaChi.Text.Trim()+"')";

                int gioiTinh = rdoNam.Checked ? 1 : 0;

                string sql = "INSERT INTO tblSinhVien VALUES('" + txtID.Text.Trim() + "',N'" + txtTenSV.Text.Trim() + "', '" + dtpNgaySinh.Value + "', " + gioiTinh + ", " + cboTrinhDo.SelectedValue + ", N'" + txtQueQuan.Text.Trim() + "', N'" + txtDiaChi.Text.Trim() + "', N'" + txtGhiChu.Text.Trim() + "')";

                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Ghi dữ liệu thành công", "Thông báo");
                    DataRow dr = dsMain.Tables[0].NewRow();
                    dr["Id"] = txtID.Text.Trim();
                    dr["TenSV"] = txtTenSV.Text.Trim();
                    dr["NgaySinh"] = dtpNgaySinh.Text.Trim();
                    dr["QueQuan"] = txtQueQuan.Text.Trim();
                    dr["DiaChi"] = txtDiaChi.Text.Trim();
                    dr["GhiChu"] = txtGhiChu.Text.Trim();
                    dsMain.Tables[0].Rows.Add(dr);
                    dgvSinhVien.DataSource = dsMain.Tables[0];
                    GetData();
                    State = "Reset";
                    SetInterface(State);
                }
                else
                {
                    lblError.Text = "Lỗi trong quá trình ghi dữ liệu";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void UpdateData()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int gioiTinh = rdoNam.Checked ? 1 : 0;

                string sql = "UPDATE tblSinhVien SET TenSV = '" + txtTenSV.Text.Trim() + "',NgaySinh = '" + dtpNgaySinh.Value + "',GioiTinh = '" + gioiTinh + "',TrinhDoHocVan = '" + cboTrinhDo.SelectedValue + "',QueQuan = N'" + txtQueQuan.Text.Trim() + "', DiaChi = N'" + txtDiaChi.Text.Trim() + "', GhiChu = N'" + txtGhiChu.Text.Trim() + "' WHERE Id = '" + txtID.Text.Trim() + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo");
                    GetData();
                    State = "Reset";
                    SetInterface(State);
                }
                else
                {
                    lblError.Text = "Lỗi trong quá cập nhật dữ liệu";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void Delete()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sql = "DELETE FROM tblSinhVien WHERE ID = '" + txtID.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Xóa dữ liệu thành công", "Thông báo");
                    GetData();
                    State = "Reset";
                    SetInterface(State);
                }
                else
                {
                    lblError.Text = "Lỗi trong quá trình xóa dữ liệu";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        #endregion
        #region Events
        private void btnThem_Click(object sender, EventArgs e)
        {
            State = "Insert";
            SetInterface(State);
        }
        private void btnGhi_Click(object sender, EventArgs e)
        {
            try
            {
                if (State == "Insert")
                {
                    // Ghi du lieu
                    Insert();
                }
                else
                {
                    // Update du lieu
                    UpdateData();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvSinhVien.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgvSinhVien.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgvSinhVien.Rows[selectedrowindex];
                    string cellValue = Convert.ToString(selectedRow.Cells["Id"].Value);
                    txtID.Text = cellValue;
                    txtTenSV.Text = Convert.ToString(selectedRow.Cells["TenSV"].Value);
                    dtpNgaySinh.Value = Convert.ToDateTime(selectedRow.Cells["NgaySinh"].Value);
                    txtQueQuan.Text = Convert.ToString(selectedRow.Cells["QueQuan"].Value);
                    txtDiaChi.Text = Convert.ToString(selectedRow.Cells["DiaChi"].Value);
                    txtGhiChu.Text = Convert.ToString(selectedRow.Cells["GhiChu"].Value);
                    cboTrinhDo.SelectedValue = selectedRow.Cells["TrinhDoHocVan"].Value;
                    string gioiTinh = Convert.ToString(selectedRow.Cells["GioiTinh"].Value);
                    if (gioiTinh == "0")
                    {
                        rdoNu.Checked = true;
                    }
                    else if (gioiTinh == "1")
                    {
                        rdoNam.Checked = true;
                    }
                    else
                    {
                        rdoNam.Checked = false;
                        rdoNu.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Delete();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            State = "Update";
            SetInterface(State);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtKeyword.Text.Trim();
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string sql = "SELECT * FROM tblSinhVien WHERE TENSV LIKE N'%" + keyword + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dgvSinhVien.DataSource = ds.Tables[0];
                    lblTongSo.Text = "Tổng số: " + ds.Tables[0].Rows.Count + " bản ghi";

                    txtID.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    txtTenSV.Text = ds.Tables[0].Rows[0]["TenSV"].ToString();
                    dtpNgaySinh.Text = ds.Tables[0].Rows[0]["NgaySinh"].ToString();
                    int gioiTinh = Convert.ToInt32(ds.Tables[0].Rows[0]["GioiTinh"]);
                    rdoNam.Checked = (gioiTinh == 1);
                    rdoNu.Checked = (gioiTinh == 0);
                    cboTrinhDo.SelectedValue = ds.Tables[0].Rows[0]["TrinhDoHocVan"];
                    txtQueQuan.Text = ds.Tables[0].Rows[0]["QueQuan"].ToString();
                    txtDiaChi.Text = ds.Tables[0].Rows[0]["DiaChi"].ToString();
                    txtGhiChu.Text = ds.Tables[0].Rows[0]["GhiChu"].ToString();
                }
                else
                {
                    lblTongSo.Text = "Tổng số: 0 bản ghi";
                    dgvSinhVien.DataSource = null;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            State = "Reset";
            SetInterface(State);
        }
        #endregion
    }
}
