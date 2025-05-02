namespace csTeachConnect
{
    partial class MessagingApp
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.labelIp = new System.Windows.Forms.Label();
            this.cmbIp = new System.Windows.Forms.ComboBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 18);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "名称:";
            // 
            // cmbName
            // 
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(87, 15);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(219, 21);
            this.cmbName.TabIndex = 1;
            // 
            // labelIp
            // 
            this.labelIp.AutoSize = true;
            this.labelIp.Location = new System.Drawing.Point(12, 45);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(73, 13);
            this.labelIp.TabIndex = 2;
            this.labelIp.Text = "教室端 IP:";
            // 
            // cmbIp
            // 
            this.cmbIp.FormattingEnabled = true;
            this.cmbIp.Location = new System.Drawing.Point(87, 42);
            this.cmbIp.Name = "cmbIp";
            this.cmbIp.Size = new System.Drawing.Size(219, 21);
            this.cmbIp.TabIndex = 3;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(12, 72);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(41, 13);
            this.labelMessage.TabIndex = 4;
            this.labelMessage.Text = "消息:";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(87, 69);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(219, 20);
            this.txtMessage.TabIndex = 5;
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Items.AddRange(new object[] {
            "普通提醒",
            "明显提醒",
            "混合提醒（教室端版本v0.3.0-Beta及以上）"});
            this.cmbPort.Location = new System.Drawing.Point(87, 95);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(219, 21);
            this.cmbPort.TabIndex = 6;
            this.cmbPort.Text = "混合提醒（教室端版本v0.3.0-Beta及以上）";
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(87, 122);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(219, 23);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // MessagingApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 155);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.cmbIp);
            this.Controls.Add(this.labelIp);
            this.Controls.Add(this.cmbName);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessagingApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息发送";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.ComboBox cmbIp;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Button btnSend;
    }
}
