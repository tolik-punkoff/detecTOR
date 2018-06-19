namespace DetecTOR
{
    partial class frmFormatOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormatOptions));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIPColumn = new System.Windows.Forms.TextBox();
            this.txtFieldSeparator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFlagColumn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFalseValue = new System.Windows.Forms.TextBox();
            this.txtTrueValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Колонка содержащая IP-адрес";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Разделитель значений";
            // 
            // txtIPColumn
            // 
            this.txtIPColumn.Location = new System.Drawing.Point(173, 1);
            this.txtIPColumn.Name = "txtIPColumn";
            this.txtIPColumn.Size = new System.Drawing.Size(195, 20);
            this.txtIPColumn.TabIndex = 2;
            // 
            // txtFieldSeparator
            // 
            this.txtFieldSeparator.Location = new System.Drawing.Point(173, 24);
            this.txtFieldSeparator.MaxLength = 1;
            this.txtFieldSeparator.Name = "txtFieldSeparator";
            this.txtFieldSeparator.Size = new System.Drawing.Size(24, 20);
            this.txtFieldSeparator.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Признак колонки-флага";
            // 
            // txtFlagColumn
            // 
            this.txtFlagColumn.Location = new System.Drawing.Point(173, 46);
            this.txtFlagColumn.Name = "txtFlagColumn";
            this.txtFlagColumn.Size = new System.Drawing.Size(195, 20);
            this.txtFlagColumn.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Значение флага \"ЛОЖЬ\"";
            // 
            // txtFalseValue
            // 
            this.txtFalseValue.Location = new System.Drawing.Point(173, 69);
            this.txtFalseValue.Name = "txtFalseValue";
            this.txtFalseValue.Size = new System.Drawing.Size(195, 20);
            this.txtFalseValue.TabIndex = 7;
            // 
            // txtTrueValue
            // 
            this.txtTrueValue.Location = new System.Drawing.Point(173, 95);
            this.txtTrueValue.Name = "txtTrueValue";
            this.txtTrueValue.Size = new System.Drawing.Size(195, 20);
            this.txtTrueValue.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Значение флага \"ИСТИНА\"";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(4, 125);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(291, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(85, 121);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(200, 30);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Редактировать описания колонок";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // frmFormatOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 154);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtTrueValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFalseValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFlagColumn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFieldSeparator);
            this.Controls.Add(this.txtIPColumn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFormatOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Опции формата";
            this.Load += new System.EventHandler(this.frmFormatOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIPColumn;
        private System.Windows.Forms.TextBox txtFieldSeparator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFlagColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFalseValue;
        private System.Windows.Forms.TextBox txtTrueValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEdit;
    }
}