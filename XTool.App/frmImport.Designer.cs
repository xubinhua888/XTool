namespace XTool.App
{
    partial class frmImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTemplateDown = new System.Windows.Forms.ToolStripButton();
            this.colstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collocation_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmailno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnOK,
            this.toolStripSeparator1,
            this.btnTemplateDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(977, 38);
            this.toolStrip1.TabIndex = 130;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(88, 35);
            this.btnImport.Text = "从文件导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(52, 35);
            this.btnOK.Text = "保存";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            this.btnTemplateDown.Text = "模板下载";
            this.btnTemplateDown.Click += new System.EventHandler(this.btnTemplateDown_Click);
            // 
            // colstatus
            // 
            this.colstatus.HeaderText = "状态";
            this.colstatus.Name = "colstatus";
            this.colstatus.ReadOnly = true;
            this.colstatus.Visible = false;
            // 
            // collocation_code
            // 
            this.collocation_code.DataPropertyName = "库位";
            this.collocation_code.HeaderText = "库位";
            this.collocation_code.Name = "collocation_code";
            this.collocation_code.ReadOnly = true;
            // 
            // colmailno
            // 
            this.colmailno.DataPropertyName = "一段面单号";
            this.colmailno.HeaderText = "一段面单号";
            this.colmailno.MinimumWidth = 130;
            this.colmailno.Name = "colmailno";
            this.colmailno.ReadOnly = true;
            this.colmailno.Width = 130;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AllowUserToResizeColumns = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 38);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(977, 503);
            this.dgvResult.TabIndex = 131;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "分运单号";
            this.Column1.HeaderText = "分运单号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 59;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "状态";
            this.Column2.HeaderText = "状态";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 35;
            // 
            // frmImport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(977, 541);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "运单号导入";
            this.Load += new System.EventHandler(this.frmImport_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripButton btnTemplateDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn collocation_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmailno;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}