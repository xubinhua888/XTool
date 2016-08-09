namespace XTool.App
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTemplateDown = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHawbCode = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblCountStr = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImport,
            this.toolStripSeparator1,
            this.btnTemplateDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1071, 38);
            this.toolStrip1.TabIndex = 131;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(88, 35);
            this.btnImport.Text = "导入运单号";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // btnTemplateDown
            // 
            this.btnTemplateDown.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnTemplateDown.Image = ((System.Drawing.Image)(resources.GetObject("btnTemplateDown.Image")));
            this.btnTemplateDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTemplateDown.Name = "btnTemplateDown";
            this.btnTemplateDown.Size = new System.Drawing.Size(76, 35);
            this.btnTemplateDown.Text = "历史运单";
            this.btnTemplateDown.Click += new System.EventHandler(this.btnTemplateDown_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F);
            this.label1.Location = new System.Drawing.Point(20, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 40);
            this.label1.TabIndex = 132;
            this.label1.Text = "运单号";
            // 
            // txtHawbCode
            // 
            this.txtHawbCode.Font = new System.Drawing.Font("宋体", 40F);
            this.txtHawbCode.Location = new System.Drawing.Point(163, 60);
            this.txtHawbCode.Name = "txtHawbCode";
            this.txtHawbCode.Size = new System.Drawing.Size(551, 68);
            this.txtHawbCode.TabIndex = 133;
            this.txtHawbCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHawbCode_KeyDown);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("宋体", 70F);
            this.lblMessage.Location = new System.Drawing.Point(115, 143);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(322, 94);
            this.lblMessage.TabIndex = 134;
            this.lblMessage.Text = "运单号";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AllowUserToResizeColumns = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvResult.Location = new System.Drawing.Point(12, 280);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(1047, 246);
            this.dgvResult.TabIndex = 135;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "分运单号";
            this.Column1.HeaderText = "分运单号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 78;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "状态";
            this.Column2.HeaderText = "状态";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 54;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "时间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 54;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(12, 265);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(53, 12);
            this.lblCount.TabIndex = 136;
            this.lblCount.Text = "扫描记录";
            // 
            // lblCountStr
            // 
            this.lblCountStr.AutoSize = true;
            this.lblCountStr.Font = new System.Drawing.Font("宋体", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCountStr.Location = new System.Drawing.Point(720, 63);
            this.lblCountStr.Name = "lblCountStr";
            this.lblCountStr.Size = new System.Drawing.Size(185, 53);
            this.lblCountStr.TabIndex = 137;
            this.lblCountStr.Text = "运单号";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1071, 538);
            this.Controls.Add(this.lblCountStr);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtHawbCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "口岸扫描";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnTemplateDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHawbCode;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label lblCountStr;

    }
}

