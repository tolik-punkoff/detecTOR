namespace DetecTOR
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.grdAnswer = new System.Windows.Forms.DataGridView();
            this.btnFind = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ipAddr = new IPAddressControlLib.IPAddressControl();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.pbConnecting = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdAnswer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnecting)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(426, 299);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(84, 23);
            this.btnAbout.TabIndex = 24;
            this.btnAbout.Text = "О программе";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(426, 268);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(84, 23);
            this.btnHelp.TabIndex = 23;
            this.btnHelp.Text = "Помощь";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(426, 225);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 37);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "Очистить загруженное";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(426, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 38);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Сохранить результат";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnViewAll
            // 
            this.btnViewAll.Location = new System.Drawing.Point(426, 141);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(86, 34);
            this.btnViewAll.TabIndex = 20;
            this.btnViewAll.Text = "Смотреть все";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(426, 100);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(86, 35);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "Обновить данные";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(426, 73);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(86, 23);
            this.btnOptions.TabIndex = 18;
            this.btnOptions.Text = "Опции";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // grdAnswer
            // 
            this.grdAnswer.AllowUserToAddRows = false;
            this.grdAnswer.AllowUserToDeleteRows = false;
            this.grdAnswer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAnswer.Location = new System.Drawing.Point(4, 73);
            this.grdAnswer.Name = "grdAnswer";
            this.grdAnswer.ReadOnly = true;
            this.grdAnswer.Size = new System.Drawing.Size(416, 249);
            this.grdAnswer.TabIndex = 17;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(220, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 16;
            this.btnFind.Text = "Найти";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMessage.Location = new System.Drawing.Point(1, 35);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(511, 35);
            this.lblMessage.TabIndex = 15;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Введите IP-адрес:";
            // 
            // ipAddr
            // 
            this.ipAddr.AllowInternalTab = false;
            this.ipAddr.AutoHeight = true;
            this.ipAddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddr.Location = new System.Drawing.Point(106, 8);
            this.ipAddr.Name = "ipAddr";
            this.ipAddr.ReadOnly = false;
            this.ipAddr.Size = new System.Drawing.Size(108, 20);
            this.ipAddr.TabIndex = 25;
            this.ipAddr.Text = "...";
            this.ipAddr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ipAddr_KeyUp);
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // 
            // pbConnecting
            // 
            this.pbConnecting.Image = global::DetecTOR.Properties.Resources.connecting;
            this.pbConnecting.Location = new System.Drawing.Point(446, 3);
            this.pbConnecting.Name = "pbConnecting";
            this.pbConnecting.Size = new System.Drawing.Size(64, 64);
            this.pbConnecting.TabIndex = 26;
            this.pbConnecting.TabStop = false;
            this.pbConnecting.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 330);
            this.Controls.Add(this.pbConnecting);
            this.Controls.Add(this.ipAddr);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnViewAll);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.grdAnswer);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DetecTOR";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.grdAnswer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnecting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.DataGridView grdAnswer;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private IPAddressControlLib.IPAddressControl ipAddr;
        private System.Windows.Forms.PictureBox pbConnecting;
        private System.Windows.Forms.SaveFileDialog dlgSave;
    }
}

