﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2.Features
{
    public partial class PhanQuyen : MetroFramework.Forms.MetroForm
    {
        public PhanQuyen()
        {
            InitializeComponent();
        }

        Database database = new Database();

        private void PhanQuyen_Load(object sender, EventArgs e)
        {
            string sql = "Select * from BoPhan";
            DataTable dt = database.Read(sql);
            cmbBoPhan.DataSource = dt;
            cmbBoPhan.ValueMember = "BoPhanID";
            cmbBoPhan.DisplayMember = "TenBoPhan";
        }

        private void cmbBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNhanVien.DataSource = null;
            string BoPhanID = cmbBoPhan.SelectedValue.ToString();
            string sql = @"select a.NhanVienID, a.TenNV from NhanVien a 
                            inner join NhanVienBoPhan b on a.NhanVienID = b.NhanVienID and b.BoPhanID = '" + BoPhanID + "'";
            DataTable dtNhanVien = database.Read(sql);
            cmbNhanVien.DataSource = dtNhanVien;
            cmbNhanVien.ValueMember = "NhanVienID";
            cmbNhanVien.DisplayMember = "TenNV";
        }

        private void cmbNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedValue != null)
            {
                cmbNguoiDung.DataSource = null;
                string NhanVienID = cmbNhanVien.SelectedValue.ToString();
                string sql = @"select a.NguoiDungID, a.TenDangNhap from NguoiDung a 
                           inner join NhanVien b on a.NhanVienID = b.NhanVienID and b.NhanVienID = '" + NhanVienID + "'";
                DataTable dtNguoiDung = database.Read(sql);
                cmbNguoiDung.DataSource = dtNguoiDung;
                cmbNguoiDung.ValueMember = "NguoiDungID";
                cmbNguoiDung.DisplayMember = "TenDangNhap";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "delete from QuyenNguoiDung where NguoiDungID = '" + cmbNguoiDung.SelectedValue + "'";
                database.Execute(sql);
                if (cbQLHT.Checked == true)
                {
                    sql = "Insert into QuyenNguoiDung(NguoiDungID, NghiepVuID) values ('" + cmbNguoiDung.SelectedValue + "', '1')";
                    database.Execute(sql);
                }
                if (cbQLDanhMuc.Checked == true)
                {
                    sql = "Insert into QuyenNguoiDung(NguoiDungID, NghiepVuID) values ('" + cmbNguoiDung.SelectedValue + "', '2')";
                    database.Execute(sql);
                }
                if (cbQLNhanSu.Checked == true)
                {
                    sql = "Insert into QuyenNguoiDung(NguoiDungID, NghiepVuID) values ('" + cmbNguoiDung.SelectedValue + "', '3')";
                    database.Execute(sql);
                }
                if (cbQLTienLuong.Checked == true)
                {
                    sql = "Insert into QuyenNguoiDung(NguoiDungID, NghiepVuID) values ('" + cmbNguoiDung.SelectedValue + "', '4')";
                    database.Execute(sql);
                }
                if (cbTKBaoCao.Checked == true)
                {
                    sql = "Insert into QuyenNguoiDung(NguoiDungID, NghiepVuID) values ('" + cmbNguoiDung.SelectedValue + "', '5')";
                    database.Execute(sql);
                }
                MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoi = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            if (hoi == DialogResult.Yes)
                this.Close();
        }

        private void lblBoPhan_Click(object sender, EventArgs e)
        {

        }
    }
}
