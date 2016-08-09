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
                if (item.Equals(colstatus))
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
                    dgvResult.DataSource = null;
                    btnOK.Enabled = false;
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
            dgvResult.DataSource = dtImportData;
            if (dgvResult.Rows.Count > 0)
            {
                btnOK.Enabled = true;
            }
        }

        #endregion

        #region 保存数据

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<OrderItem> lstItem = new List<OrderItem>();
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                lstItem.Add(new OrderItem()
                {
                    hawb_code = row.Cells[Column1.Name].Value.ToString(),
                    order_status = row.Cells[Column2.Name].Value.ToString()
                });
            }
            OrderBLL.AddOrderItem(lstItem);
            MessageBox.Show("保存成功!");
            this.Close();
        }

        #endregion

    }
}
