using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            //初始化数据库
            XTool.BLL.OrderBLL.DatabaseInit(System.IO.Path.Combine(Application.StartupPath, "Data", "data.db"));
            #region 自动释放Sqlite文件
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Plugins")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Plugins"));
            }
            string strSqliteFile = Path.Combine(Application.StartupPath, "Plugins", "System.Data.SQLite.dll");
            if (!System.IO.File.Exists(strSqliteFile))
            {
                FileStream fileStream = new FileStream(strSqliteFile, FileMode.Create, FileAccess.Write, FileShare.None);
                byte[] fileByte = null;
                if (NewLife.Runtime.Is64BitOperatingSystem)
                {
                    fileByte = XTool.App.Properties.Resources.System_Data_SQLite64;
                }
                else
                {
                    fileByte = XTool.App.Properties.Resources.System_Data_SQLite;
                }
                fileStream.Write(fileByte, 0, fileByte.Length);
                fileStream.Flush();
                fileStream.Close();
            }
            #endregion

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
                    if (string.Equals(lstOrderItem[0].OrderStatus, "放行"))
                    {
                        ShowMsg(string.Format("{0}，正常!", lstOrderItem[0].OrderStatus));
                      
                    }
                    else
                    {
                        ShowError(string.Format("{0}，异常状态!", lstOrderItem[0].OrderStatus));
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
            DataTable dtResult = OrderBLL.GetOrderItemList(0, item.ID);
            AddColumn(dtResult);
            dgvResult.Rows.Insert(0, 1);
            foreach (DataGridViewColumn dgvcolumn in dgvResult.Columns)
            {
                foreach (DataColumn column in dtResult.Columns)
                {
                    if(string.Equals(column.ColumnName,dgvcolumn.DataPropertyName))
                    {
                        dgvResult.Rows[0].Cells[dgvcolumn.Index].Value = dtResult.Rows[0][column.ColumnName].ToString();
                    }
                }
            }
            dgvResult.CurrentCell = dgvResult.Rows[0].Cells[Column1.Name];
            lblCount.Text = string.Format("扫描记录-{0}", dgvResult.Rows.Count);
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
                if (lstColumnName.Contains(dc.ColumnName) || string.Equals(dc.ColumnName, "ID"))
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
    }
}
