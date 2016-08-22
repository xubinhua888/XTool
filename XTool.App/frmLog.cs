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
    public partial class frmLog : Form
    {
        private int order_id;
        public frmLog()
        {
            InitializeComponent(); 
            dgvResult.AutoGenerateColumns = false;
        }

        public void SetDataSource(List<OrderBatchLog> lstLog)
        {
            dgvResult.DataSource = lstLog;
        }

        public void SetDataSource(List<OrderItemLog> lstLog)
        {
            dgvResult.DataSource = lstLog;
        }
    }
}
