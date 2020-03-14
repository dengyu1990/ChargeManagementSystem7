namespace IFC20110208
{
    partial class frmOut
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.cboExpenditure = new System.Windows.Forms.ComboBox();
            this.btnCost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Indigo;
            this.label1.Location = new System.Drawing.Point(36, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "支出金额";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Indigo;
            this.label2.Location = new System.Drawing.Point(36, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "开销事由";
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(99, 41);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(100, 21);
            this.txtCost.TabIndex = 2;
            // 
            // cboExpenditure
            // 
            this.cboExpenditure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExpenditure.FormattingEnabled = true;
            this.cboExpenditure.Items.AddRange(new object[] {
            "电费",
            "饮用水",
            "生活用品",
            "上网费",
            "团购",
            "其它"});
            this.cboExpenditure.Location = new System.Drawing.Point(99, 80);
            this.cboExpenditure.Name = "cboExpenditure";
            this.cboExpenditure.Size = new System.Drawing.Size(100, 20);
            this.cboExpenditure.TabIndex = 3;
            // 
            // btnCost
            // 
            this.btnCost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCost.Image = global::IFC20110208.Properties.Resources.FishOut;
            this.btnCost.Location = new System.Drawing.Point(239, 41);
            this.btnCost.Name = "btnCost";
            this.btnCost.Size = new System.Drawing.Size(74, 59);
            this.btnCost.TabIndex = 4;
            this.btnCost.UseVisualStyleBackColor = true;
            this.btnCost.Click += new System.EventHandler(this.btnCost_Click);
            // 
            // frmOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 175);
            this.Controls.Add(this.btnCost);
            this.Controls.Add(this.cboExpenditure);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOut";
            this.Text = "寝费支出";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.ComboBox cboExpenditure;
        private System.Windows.Forms.Button btnCost;
    }
}