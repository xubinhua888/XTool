﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XTool.BLL;
using XTool.Common;

namespace XTool.App
{
    public partial class frmMain : Form
    {
        SoundPlayerSound sounder = new SoundPlayerSound();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dgvResult.AutoGenerateColumns = false;
            sounder.Init();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            sounder.Dispose();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            new frmImport().ShowDialog();
        }

        private void btnTemplateDown_Click(object sender, EventArgs e)
        {
            new frmOrderList().ShowDialog();
        }

        private void txtHawbCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtHawbCode.Text.Trim()) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    List<OrderItem> lstOrderItem = OrderBLL.FindOrderItem(txtHawbCode.Text.Trim().ToUpper());
                    if (lstOrderItem.Count < 1)
                    {
                        ShowError("运单号不存在!");
                        return;
                    }
                    txtHawbCode.Text = string.Empty;
                    lblCountStr.Text = OrderBLL.ScannedOrderItem(lstOrderItem[0]);
                    if (string.Equals(lstOrderItem[0].order_status, "放行"))
                    {
                        ShowMsg(string.Format("{0}，正常!", lstOrderItem[0].order_status));
                    }
                    else
                    {
                        ShowError(string.Format("{0}，异常状态!", lstOrderItem[0].order_status));
                    }
                    AddToHistory(lstOrderItem[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    txtHawbCode.SelectAll();
                    txtHawbCode.Focus();
                }
            }
        }

        private void ShowError(string message)
        {
            sounder.Error();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = message;
        }

        private void ShowMsg(string message)
        {
            sounder.Success();
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = message;
        }

        private void AddToHistory(OrderItem item)
        {
            dgvResult.Rows.Insert(0, 1);
            dgvResult.Rows[0].Cells[Column1.Name].Value = item.hawb_code;
            dgvResult.Rows[0].Cells[Column2.Name].Value = item.order_status;
            dgvResult.Rows[0].Cells[Column3.Name].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dgvResult.CurrentCell = dgvResult.Rows[0].Cells[Column1.Name];
            lblCount.Text = string.Format("扫描记录-{0}", dgvResult.Rows.Count);
        }
    }
}
