﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.ReportServer.ServiceModel.ConnectionProviders;
using DevExpress.XtraBars;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using TechnicService.Forms;


namespace TechnicService
{
    public partial class Home : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        FrmProductList frmProductList;
        private Forms.FrmMalfunction frmMalfunction;
        FrmProductUpdate update;
        FrmProductAdd add;
        FrmExpenses expenses;
        FrmStatistic statistic;
        FrmStatistickMounthSell mounlyStatistic;
        FrmStatistickAnnualSalesAndAverage annualStatistic;
        FrmCustomerAdd frmCustomer;
        FrmCustomerList frmCustomerList;
        FrmCustomerUpdate frmCustomerUpdate;





        public Home()
        {
            InitializeComponent();
        }


        private void btnMalfunctions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            frmMalfunction = new FrmMalfunction();
            frmMalfunction.MdiParent = this;
            frmMalfunction.Show();

        }

        private void btnCategoryAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCategoryAdd categoryAdd = new FrmCategoryAdd();
            categoryAdd.Show();
        }

        private void btnCategoryDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCategoryDelete categoryDelete = new FrmCategoryDelete();
            categoryDelete.Show();
        }

        private void btnCategoryUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCategoryUpdate categoryUpdate = new FrmCategoryUpdate();
            categoryUpdate.Show();
        }

        private void btnProducts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barProductAdd.Visibility = BarItemVisibility.Always;
            barProductUpdate.Visibility = BarItemVisibility.Always;

            frmProductList = new FrmProductList();
            frmProductList.MdiParent = this;
            frmProductList.Show();


            frmProductList.MdiParent.Show();
            frmProductList.ProductList();
        }

        private void barProductUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {

            update = new FrmProductUpdate();
            update.MdiParent = this;
            update.Show();

        }

        private void barProductAdd_ItemClick(object sender, ItemClickEventArgs e)
        {

            add = new FrmProductAdd();
            add.MdiParent = this;
            add.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {

            frmProductList = new FrmProductList();
            frmProductList.MdiParent = this;
            frmProductList.Show();


            //---------------Database backup---------------
            string backupPath = @"E:\Yedekler/TechicService_" + DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year + ".bak";
            string server = @"(localdb)\MSSQLLocalDB";

            string backupFolder ="E:\\Yedekler";
            System.IO.Directory.CreateDirectory(backupFolder);

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(backupFolder);
            System.IO.FileInfo[] fis = di.GetFiles("*.bak");
            if (fis.Length == 0 || (DateTime.Now - fis.Max(d => d.CreationTime)).TotalMinutes >= 5)
            { 
                Server dbServer = new Server(new ServerConnection(server));
                Backup dbBackup = new Backup();
                dbBackup.Action = BackupActionType.Database;
                dbBackup.Database = "TechnicService";
                dbBackup.Devices.AddDevice(backupPath, DeviceType.File);
                dbBackup.Initialize = false;
                dbBackup.Complete += DbBackupOnComplete;
                dbBackup.SqlBackup(dbServer);
            }

        }

        private void DbBackupOnComplete(object sender, ServerMessageEventArgs e)
        {
            try
            {
                MessageBox.Show("Yedekleme Başarılı","", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            expenses = new FrmExpenses();
            expenses.MdiParent = this;
            expenses.Show();

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            statistic = new FrmStatistic();
            statistic.MdiParent = this;
            statistic.Show();

        }

        private void barMounthSell_ItemClick(object sender, ItemClickEventArgs e)
        {
            mounlyStatistic = new FrmStatistickMounthSell();
            mounlyStatistic.MdiParent = this;
            mounlyStatistic.Show();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            annualStatistic = new FrmStatistickAnnualSalesAndAverage();
            annualStatistic.MdiParent = this;
            annualStatistic.Show();

        }


        private void barReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            Report report = new Report();
            report.Show();
        }

        private void barcCustormer_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCustomerList = new FrmCustomerList();
            frmCustomerList.MdiParent = this;
            frmCustomerList.Show();
            frmCustomerList.CustomerList();
            barCustomerAdd.Visibility = BarItemVisibility.Always;
            barCustomerUpdate.Visibility = BarItemVisibility.Always;
        }

        private void barCustomerAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCustomer = new FrmCustomerAdd();
            frmCustomer.MdiParent = this;
            frmCustomer.Show();
        }

        private void barCustomerUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCustomerUpdate = new FrmCustomerUpdate();
            frmCustomerUpdate.MdiParent = this;
            frmCustomerUpdate.Show();
        }
    }

    
}
