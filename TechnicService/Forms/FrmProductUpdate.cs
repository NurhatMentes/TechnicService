﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechnicService.Forms
{
    public partial class FrmProductUpdate : Form
    {
        public FrmProductUpdate()
        {
            InitializeComponent();
        }

        private TechnicServiceEntities _entities = new TechnicServiceEntities();
        void CategorySelection()
        {
            cbxCategory.Properties.DataSource = (from x in _entities.Category
                select new
                {
                    x.Id,
                    x.Name

                }).ToList();
        }

        void CustomerCategorySelection()
        {
            cbxCustomer.Properties.DataSource = (from x in _entities.Customers
                where x.Status == true
                select new
                {
                    ÖzelKod = x.Id,
                    UnvanAd = x.FirstName
                }).ToList();
        }

        void GetProduct()
        {
            int productId = Convert.ToInt32(cbxProduct.EditValue);
            if (productId > 0)
            {
                var product = _entities.Products.Find(productId);
                txtName.Text = product.Name;
                txtbrand.Text = product.Brand;
                txtPurchase.Text =  product.Purchase.ToString();
                txtSalesPrice.Text = product.SalesPrice.ToString();
                txtStock.Text = product.stock.ToString();
                cbxCustomer.EditValue = product.CustomerId;
            }
        }

        void ProductSelection()
        {
            cbxProduct.Properties.DataSource = (from x in _entities.Products
                select new
                {
                    x.Id,
                    x.Name

                }).ToList();
        }
        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (txtName.Text != "" && Convert.ToInt32(cbxCategory.EditValue)>0&& txtbrand.Text!="" && txtStock.Text !="" && txtPurchase.Text!="" && txtSalesPrice.Text!="" && Convert.ToInt32(cbxCustomer.EditValue) > 0)
            {
                var message = MessageBox.Show("Güncellemek istediğinize emin misini?", "Soru", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (message == DialogResult.Yes)
                {
                    int id = Int32.Parse(cbxProduct.EditValue.ToString());
                    var product = _entities.Products.Find(id);
                    product.CustomerId = Convert.ToInt32(cbxCustomer.EditValue.ToString());
                    product.Name = char.ToUpper(txtName.Text[0]) + txtName.Text.Substring(1);
                    product.Brand = char.ToUpper(txtbrand.Text[0]) + txtbrand.Text.Substring(1); ;
                    product.CategoryId = Convert.ToInt32(cbxCategory.EditValue.ToString());
                    product.Purchase = Convert.ToDecimal(txtPurchase.Text);
                    product.SalesPrice = Convert.ToDecimal(txtSalesPrice.Text);
                    product.stock = Convert.ToInt32(txtStock.Text);
                    product.Status = Convert.ToInt32(txtStock.Text) > 0 ? true : false;
                    _entities.SaveChanges();
                    MessageBox.Show("Ürün başarı ile güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                txtName.Text = "";
                txtbrand.Text = "";
                txtStock.Text = "";
                txtPurchase.Text = "";
                txtSalesPrice.Text = "";
                cbxCategory.EditValue = null;
                cbxCustomer.EditValue = null;
                cbxProduct.EditValue = null;
            }
            else
            {
                MessageBox.Show("Doldurulmayan alanları doldurunuz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmProductUpdate_Load(object sender, EventArgs e)
        {
            CategorySelection();
            ProductSelection();
            CustomerCategorySelection();
        }


        private void cbxProduct_EditValueChanged(object sender, EventArgs e)
        {
            GetProduct();
        }
    }
}
