namespace DetecTOR
{
    partial class frmOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataUrl = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFormatOptions = new System.Windows.Forms.Button();
            this.rbDataFormatIPAddInfo = new System.Windows.Forms.RadioButton();
            this.rbDataFormatIPList = new System.Windows.Forms.RadioButton();
            this.chkLoadUpdate = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNetworkOption = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Адрес файла данных";
            // 
            // txtDataUrl
            // 
            this.txtDataUrl.Location = new System.Drawing.Point(123, 4);
            this.txtDataUrl.Name = "txtDataUrl";
            this.txtDataUrl.Size = new System.Drawing.Size(272, 20);
            this.txtDataUrl.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFormatOptions);
            this.groupBox1.Controls.Add(this.rbDataFormatIPAddInfo);
            this.groupBox1.Controls.Add(this.rbDataFormatIPList);
            this.groupBox1.Location = new System.Drawing.Point(7, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 69);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Формат (установите формат в зависимости от получаемых данных):";
            // 
            // btnFormatOptions
            // 
            this.btnFormatOptions.Location = new System.Drawing.Point(195, 16);
            this.btnFormatOptions.Name = "btnFormatOptions";
            this.btnFormatOptions.Size = new System.Drawing.Size(186, 35);
            this.btnFormatOptions.TabIndex = 2;
            this.btnFormatOptions.Text = "Дополнительные параметры формата...";
            this.btnFormatOptions.UseVisualStyleBackColor = true;
            this.btnFormatOptions.Click += new System.EventHandler(this.btnFormatOptions_Click);
            // 
            // rbDataFormatIPAddInfo
            // 
            this.rbDataFormatIPAddInfo.AutoSize = true;
            this.rbDataFormatIPAddInfo.Checked = true;
            this.rbDataFormatIPAddInfo.Location = new System.Drawing.Point(6, 19);
            this.rbDataFormatIPAddInfo.Name = "rbDataFormatIPAddInfo";
            this.rbDataFormatIPAddInfo.Size = new System.Drawing.Size(183, 17);
            this.rbDataFormatIPAddInfo.TabIndex = 1;
            this.rbDataFormatIPAddInfo.TabStop = true;
            this.rbDataFormatIPAddInfo.Text = "IP и дополнительные сведения";
            this.rbDataFormatIPAddInfo.UseVisualStyleBackColor = true;
            this.rbDataFormatIPAddInfo.CheckedChanged += new System.EventHandler(this.rbDataFormatIPAddInfo_CheckedChanged);
            // 
            // rbDataFormatIPList
            // 
            this.rbDataFormatIPList.AutoSize = true;
            this.rbDataFormatIPList.Location = new System.Drawing.Point(6, 42);
            this.rbDataFormatIPList.Name = "rbDataFormatIPList";
            this.rbDataFormatIPList.Size = new System.Drawing.Size(75, 17);
            this.rbDataFormatIPList.TabIndex = 0;
            this.rbDataFormatIPList.Text = "Только IP";
            this.rbDataFormatIPList.UseVisualStyleBackColor = true;
            // 
            // chkLoadUpdate
            // 
            this.chkLoadUpdate.AutoSize = true;
            this.chkLoadUpdate.Location = new System.Drawing.Point(7, 106);
            this.chkLoadUpdate.Name = "chkLoadUpdate";
            this.chkLoadUpdate.Size = new System.Drawing.Size(192, 17);
            this.chkLoadUpdate.TabIndex = 3;
            this.chkLoadUpdate.Text = "Обновлять данные при загрузке";
            this.chkLoadUpdate.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(7, 129);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(320, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNetworkOption
            // 
            this.btnNetworkOption.Location = new System.Drawing.Point(205, 102);
            this.btnNetworkOption.Name = "btnNetworkOption";
            this.btnNetworkOption.Size = new System.Drawing.Size(190, 23);
            this.btnNetworkOption.TabIndex = 6;
            this.btnNetworkOption.Text = "Параметры соединения с сетью";
            this.btnNetworkOption.UseVisualStyleBackColor = true;
            this.btnNetworkOption.Click += new System.EventHandler(this.btnNetworkOption_Click);
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 157);
            this.Controls.Add(this.btnNetworkOption);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkLoadUpdate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtDataUrl);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Опции";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDataUrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDataFormatIPAddInfo;
        private System.Windows.Forms.RadioButton rbDataFormatIPList;
        private System.Windows.Forms.CheckBox chkLoadUpdate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFormatOptions;
        private System.Windows.Forms.Button btnNetworkOption;
    }
}