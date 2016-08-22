using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XTool.BLL;

namespace XTool.App
{
    public partial class frmOrderList : Form
    {
        public frmOrderList()
        {
            InitializeComponent();
        }

        private void frmOrderList_Load(object sender, EventArgs e)
        {
            dgvResult.AutoGenerateColumns = false;
            dgvResult.DataSource = OrderBLL.GetOrderBatchList();
        }

        private void btnOrderList_Click(object sender, EventArgs e)
        {
            if (dgvResult.CurrentRow == null)
            {
                return;
            }
            try
            {
                int order_id = int.Parse(dgvResult.CurrentRow.Cells[Column1.Name].Value.ToString());
                new frmOrderView(order_id).ShowDialog();
                dgvResult.DataSource = OrderBLL.GetOrderBatchList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvResult.CurrentRow == null)
            {
                return;
            }
            try
            {
                if (MessageBox.Show("确定要删除选中的数据吗，删除后无法恢复?", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    int id = int.Parse(dgvResult.CurrentRow.Cells[Column1.Name].Value.ToString());
                    OrderBLL.DeleteOrderBatch(id);
                    dgvResult.DataSource = OrderBLL.GetOrderBatchList();
                    MessageBox.Show("删除成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            if (dgvResult.CurrentRow == null)
            {
                return;
            }
            try
            {
                int id = int.Parse(dgvResult.CurrentRow.Cells[Column1.Name].Value.ToString());
                frmLog log = new frmLog();
                log.SetDataSource(OrderBLL.GetOrderBatchLogList(id));
                log.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
