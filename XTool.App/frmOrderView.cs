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
    public partial class frmOrderView : Form
    {
        private int order_id;
        public frmOrderView()
        {
            InitializeComponent();
        }
        public frmOrderView(int order_id)
            : this()
        {
            this.order_id = order_id;
        }

        private void frmOrderList_Load(object sender, EventArgs e)
        {
            dgvResult.AutoGenerateColumns = false;
            dgvResult.DataSource = OrderBLL.GetOrderItemList(order_id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvResult.CurrentRow == null)
            {
                return;
            }
            try
            {
                if (MessageBox.Show("确定要删除选中的运单号吗?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = int.Parse(dgvResult.CurrentRow.Cells[Column1.Name].Value.ToString());
                    OrderBLL.DeleteOrderItem(id);
                    dgvResult.DataSource = OrderBLL.GetOrderItemList(order_id);
                    MessageBox.Show("删除成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
