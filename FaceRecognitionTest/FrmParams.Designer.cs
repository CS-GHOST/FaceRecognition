namespace FaceRecognitionTest
{
    partial class FrmParams
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParams));
            this.pnlProperty = new System.Windows.Forms.Panel();
            this.dgvProperty = new System.Windows.Forms.DataGridView();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDescrible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperty)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlProperty
            // 
            this.pnlProperty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProperty.Controls.Add(this.pnlContent);
            this.pnlProperty.Controls.Add(this.pnlBottom);
            this.pnlProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProperty.Location = new System.Drawing.Point(0, 0);
            this.pnlProperty.Name = "pnlProperty";
            this.pnlProperty.Size = new System.Drawing.Size(555, 415);
            this.pnlProperty.TabIndex = 1;
            // 
            // dgvProperty
            // 
            this.dgvProperty.AllowUserToAddRows = false;
            this.dgvProperty.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvProperty.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProperty.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProperty.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProperty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProperty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clName,
            this.clType,
            this.clValue,
            this.clDescrible});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProperty.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProperty.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvProperty.Location = new System.Drawing.Point(0, 0);
            this.dgvProperty.Name = "dgvProperty";
            this.dgvProperty.RowHeadersVisible = false;
            this.dgvProperty.RowHeadersWidth = 30;
            this.dgvProperty.RowTemplate.Height = 25;
            this.dgvProperty.Size = new System.Drawing.Size(553, 376);
            this.dgvProperty.TabIndex = 0;
            // 
            // clName
            // 
            this.clName.Frozen = true;
            this.clName.HeaderText = "属性";
            this.clName.MinimumWidth = 100;
            this.clName.Name = "clName";
            this.clName.ReadOnly = true;
            this.clName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clType
            // 
            this.clType.Frozen = true;
            this.clType.HeaderText = "数据类型";
            this.clType.Name = "clType";
            this.clType.ReadOnly = true;
            this.clType.Width = 140;
            // 
            // clValue
            // 
            this.clValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clValue.Frozen = true;
            this.clValue.HeaderText = "值";
            this.clValue.Name = "clValue";
            this.clValue.Width = 151;
            // 
            // clDescrible
            // 
            this.clDescrible.HeaderText = "说明";
            this.clDescrible.MinimumWidth = 150;
            this.clDescrible.Name = "clDescrible";
            this.clDescrible.ReadOnly = true;
            this.clDescrible.Width = 150;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(394, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(63, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(476, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 376);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(553, 37);
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvProperty);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(553, 376);
            this.pnlContent.TabIndex = 2;
            // 
            // FrmParams
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 415);
            this.Controls.Add(this.pnlProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmParams";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.FrmParams_Load);
            this.pnlProperty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperty)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlProperty;
        private System.Windows.Forms.DataGridView dgvProperty;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDescrible;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlContent;

    }
}