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
            SetDataSource();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvResult.CurrentRow == null)
            {
                return;
            }
            try
            {
                if (MessageBox.Show("确定要删除选中的运单号吗,删除后无法恢复?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    int id = int.Parse(dgvResult.CurrentRow.Cells[Column1.Name].Value.ToString());
                    OrderBLL.DeleteOrderItem(id);
                    SetDataSource();
                    MessageBox.Show("删除成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDataSource()
        {
            DataTable dtResult = OrderBLL.GetOrderItemList(order_id);
            AddColumn(dtResult);
            dgvResult.DataSource = dtResult;
        }

        private void AddColumn(DataTable dtResult)
        {
            if (dtResult == null)
            {
                return;
            }
            Dictionary<DataRow, Dictionary<string, string>> dic = new Dictionary<DataRow, Dictionary<string, string>>();
            List<string> lstAddColumn = new List<string>();
            foreach (DataRow dc in dtResult.Rows)
            {
                Dictionary<string, string> dicContent = JsonToDictionary(dc["Content"]);
                foreach (var item in dicContent)
                {
                    if (!lstAddColumn.Contains(item.Key))
                    {
                        lstAddColumn.Add(item.Key);
                    }
                }
                dic.Add(dc, dicContent);
            }

            lstAddColumn.ForEach(p => dtResult.Columns.Add(p));

            foreach (var item in dic)
            {
                foreach (var item1 in item.Value)
                {
                    if(lstAddColumn.Contains(item1.Key))
                    {
                        item.Key[item1.Key] = item1.Value;
                    }
                }
            }

            dtResult.Columns.Remove("BatchID");
            dtResult.Columns.Remove("Content");


            List<string> lstColumnName = new List<string>();
            foreach (DataGridViewColumn dc in dgvResult.Columns)
            {
                lstColumnName.Add(dc.DataPropertyName);
                lstColumnName.Add(dc.HeaderText);
            }

            foreach (DataColumn dc in dtResult.Columns)
            {
                if (lstColumnName.Contains(dc.ColumnName))
                {
                    continue;
                }
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = dc.ColumnName;
                column.HeaderText = dc.ColumnName;
                column.Name = dc.ColumnName;
                column.ReadOnly = true;
                dgvResult.Columns.Add(column);
                lstColumnName.Add(dc.ColumnName);
            }
        }

        private Dictionary<string, string> JsonToDictionary(object obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
            {
                return new Dictionary<string, string>();
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(obj.ToString());
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
                log.SetDataSource(OrderBLL.GetOrderItemLogList(id));
                log.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
