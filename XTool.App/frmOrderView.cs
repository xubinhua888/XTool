using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XTool.BLL;
using XTool.File;

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
            DataTable dtResult = OrderBLL.GetOrderItemList(order_id,0);
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
                    if (lstAddColumn.Contains(item1.Key))
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("总单号");
            dt.Columns.Add("运单号");
            dt.Columns.Add("清关类型");
            dt.Columns.Add("件数");
            dt.Columns.Add("是否查验");
            dt.Columns.Add("入库时间");
            dt.Columns.Add("状态");
            dt.Columns.Add("导入时间");
            dt.Columns.Add("到货时间");
            
            foreach (DataGridViewRow item in dgvResult.Rows)
            {
                DataRow row = dt.NewRow();
                foreach (DataGridViewColumn column in dgvResult.Columns)
                {
                    string columnHeaderText = column.HeaderText.Trim();
                    if (string.Equals(columnHeaderText, "总单号"))
                    {
                        row["总单号"] = item.Cells[column.Index].Value;
                    }
                    else if (string.Equals(columnHeaderText, "分运单号"))
                    {
                        row["运单号"] = item.Cells[column.Index].Value;
                    }
                    else if (string.Equals(columnHeaderText, "海关类别"))
                    {
                        row["清关类型"] = item.Cells[column.Index].Value + "类";
                    }
                    else if (string.Equals(columnHeaderText, "件数"))
                    {
                        row["件数"] = item.Cells[column.Index].Value;
                    }
                    else if (string.Equals(columnHeaderText, "扫描时间"))
                    {
                        row["到货时间"] = item.Cells[column.Index].Value;
                        if (item.Cells[column.Index].Value == null || string.IsNullOrWhiteSpace(item.Cells[column.Index].Value.ToString()))
                        {
                            row["状态"] = "没信息";
                        }
                        else
                        {
                            row["状态"] = "到货";
                        }
                    }
                    else if (string.Equals(columnHeaderText, "导入时间"))
                    {
                        row["导入时间"] = item.Cells[column.Index].Value;
                    }
                    else if (string.Equals(columnHeaderText, "状态"))
                    {
                        if (item.Cells[column.Index].Value == null || ! string.Equals(item.Cells[column.Index].Value.ToString(),"查验"))
                        {
                            row["是否查验"] = "不查验";
                        }
                        else
                        {
                            row["是否查验"] = "查验";
                        }
                    }
                    else if (string.Equals(columnHeaderText, ""))
                    {

                    }
                }
                dt.Rows.Add(row);
            }

            SaveFileDialog objSaveFileDialog = new SaveFileDialog();
            objSaveFileDialog.Filter = "Excel(*.xls)|*.xls";
            objSaveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            if (objSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExcelHelper.TableToExcelForXLS(dt, objSaveFileDialog.FileName);
                MessageBox.Show("导出成功!");
            }
        }
    }
}
