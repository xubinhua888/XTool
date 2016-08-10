using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XTool.File;
using XTool.BLL;
using System.Threading;

namespace XTool.App
{
    public partial class frmImport : Form
    {
        DataTable dtImportData;

        public frmImport()
        {
            InitializeComponent();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            dgvResult.AutoGenerateColumns = false;
        }

        #region 模板下载

        private void btnTemplateDown_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "文件(*.xls)|*.xls";
            saveFile.FileName = "运单号导入模板";
            saveFile.Title = "模板下载";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            DataTable dtTemplate = new DataTable();
            foreach (DataGridViewColumn item in dgvResult.Columns)
            {
                if (item.Equals(Column3))
                {
                    continue;
                }
                if (item.Visible && item.CellType != typeof(DataGridViewCheckBoxCell))
                {
                    dtTemplate.Columns.Add(item.HeaderText);
                }
            }

            Dictionary<int, DictionaryData> listDictionary = new Dictionary<int, DictionaryData>();
            ExcelHelper.TableToExcelForXLS(dtTemplate, saveFile.FileName, listDictionary);
            MessageBox.Show("下载模板成功!");
        }

        #endregion

        #region 数据导入

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "文件(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    dgvResult.Rows.Clear();
                    btnOK.Enabled = false;
                    btnImport.Enabled = false;
                    btnTemplateDown.Enabled = false;
                    Import(openFile.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void Import(string fileName)
        {
            dtImportData = ExcelHelper.ExcelToTable(fileName, 0);
            if (dtImportData == null && dtImportData.Columns.Count < 1 && dtImportData.Rows.Count < 1)
            {
                MessageBox.Show("从导入的文件中未加载出任何数据，请确认数据在第一个Sheet页中!");
                return;
            }
            if (!dtImportData.Columns.Contains(Column1.DataPropertyName))
            {
                MessageBox.Show(string.Format("导入的文件中不包含列:{0} !", Column1.HeaderText));
                return;
            }
            if (!dtImportData.Columns.Contains(Column2.DataPropertyName))
            {
                MessageBox.Show(string.Format("导入的文件中不包含列:{0} !", Column2.HeaderText));
                return;
            }
            foreach (DataRow drImportData in dtImportData.Rows)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvResult);
                if (drImportData[Column1.DataPropertyName] != null)
                {
                    row.Cells[Column1.Index].Value = drImportData[Column1.DataPropertyName].ToString().ToUpper();
                }
                if (drImportData[Column2.DataPropertyName] != null)
                {
                    row.Cells[Column2.Index].Value = drImportData[Column2.DataPropertyName].ToString();
                }
                row.Cells[Column3.Index].Value = "待检查";
                dgvResult.Rows.Add(row);
            }

            if (dgvResult.Rows.Count > 0)
            {
                Thread thread = new Thread(CheckData);
                thread.Start();
            }
        }

        private void CheckData()
        {
            int iCount = 0;
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (DataGridViewRow row in dgvResult.Rows)
                {
                    row.Cells[Column3.Name].Value = "检查中";
                    if (row.Cells[Column1.Index].Value == null || string.IsNullOrWhiteSpace(row.Cells[Column1.Index].Value.ToString()))
                    {
                        SetDataGridViewRowError(row, Column1.HeaderText + "不能为空");
                        continue;
                    }

                    if (row.Cells[Column2.Index].Value == null || string.IsNullOrWhiteSpace(row.Cells[Column2.Index].Value.ToString()))
                    {
                        SetDataGridViewRowError(row, Column2.HeaderText + "不能为空");
                        continue;
                    }

                    if (dic.ContainsKey(row.Cells[Column1.Index].Value.ToString()))
                    {
                        SetDataGridViewRowError(row, "重复的" + Column1.HeaderText);
                        continue;
                    }
                    dic.Add(row.Cells[Column1.Index].Value.ToString(), string.Empty);
                    List<OrderItem> lstItem = OrderBLL.FindOrderItem(row.Cells[Column1.Index].Value.ToString());
                    if (lstItem != null && lstItem.Count > 0)
                    {
                        SetDataGridViewRowError(row, "重复的" + Column1.HeaderText);
                        continue;
                    }

                    row.Cells[Column3.Name].Value = "待保存";
                    iCount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnOK.Enabled = iCount > 0;
                btnImport.Enabled = true;
                btnTemplateDown.Enabled = true;
            }
        }

        private void SetDataGridViewRowError(DataGridViewRow row, string errorMsg)
        {
            row.DefaultCellStyle.BackColor = Color.Red;
            row.DefaultCellStyle.SelectionBackColor = Color.Red;
            row.Cells[Column3.Name].Value = errorMsg;
        }

        #endregion

        #region 保存数据

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> lstRow = new List<DataGridViewRow>();
            List<OrderItem> lstItem = new List<OrderItem>();
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                if (!string.Equals(row.Cells[Column3.Name].Value.ToString(), "待保存"))
                {
                    continue;
                }
                lstItem.Add(new OrderItem()
                {
                    hawb_code = row.Cells[Column1.Name].Value.ToString(),
                    order_status = row.Cells[Column2.Name].Value.ToString()
                });
                lstRow.Add(row);
            }
            OrderBLL.AddOrderItem(lstItem);
            lstRow.ForEach(p =>
            {
                p.DefaultCellStyle.BackColor = Color.Green;
                p.Cells[Column3.Name].Value = "保存成功";
            });
            MessageBox.Show("保存成功!");
        }

        #endregion

    }
}
