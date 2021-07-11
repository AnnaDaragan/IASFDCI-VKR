namespace IASFDCI
{
    partial class MailSendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailSendForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfCodeTextBox = new System.Windows.Forms.TextBox();
            this.ConfBut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(504, 182);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.label1.Font = new System.Drawing.Font("Sylfaen", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(30, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(441, 62);
            this.label1.TabIndex = 1;
            this.label1.Text = "На указанный электронный адрес отправлен код, введите его в строку подтверждения";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfCodeTextBox
            // 
            this.ConfCodeTextBox.Font = new System.Drawing.Font("Sylfaen", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConfCodeTextBox.Location = new System.Drawing.Point(117, 96);
            this.ConfCodeTextBox.Name = "ConfCodeTextBox";
            this.ConfCodeTextBox.Size = new System.Drawing.Size(290, 38);
            this.ConfCodeTextBox.TabIndex = 2;
            this.ConfCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConfBut
            // 
            this.ConfBut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfBut.Font = new System.Drawing.Font("Sylfaen", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConfBut.Location = new System.Drawing.Point(189, 147);
            this.ConfBut.Name = "ConfBut";
            this.ConfBut.Size = new System.Drawing.Size(146, 35);
            this.ConfBut.TabIndex = 3;
            this.ConfBut.Text = "Подтвердить";
            this.ConfBut.UseVisualStyleBackColor = true;
            this.ConfBut.Click += new System.EventHandler(this.ConfBut_Click);
            // 
            // MailSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IASFDCI.Properties.Resources.backIm;
            this.ClientSize = new System.Drawing.Size(528, 206);
            this.Controls.Add(this.ConfBut);
            this.Controls.Add(this.ConfCodeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MailSendForm";
            this.Text = "Подтвержедение электронной почты";
            this.Load += new System.EventHandler(this.MailSendForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ConfCodeTextBox;
        private System.Windows.Forms.Button ConfBut;
    }
}