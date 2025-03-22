namespace RandevuTakipSistemi
{
    partial class RandevuAtama
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
            cmbUsers = new ComboBox();
            dtpAppointmentDate = new DateTimePicker();
            btnAddAppointment = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // cmbUsers
            // 
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(298, 125);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(180, 23);
            cmbUsers.TabIndex = 0;
            // 
            // dtpAppointmentDate
            // 
            dtpAppointmentDate.CustomFormat = "dd.MM.yyyy HH:mm";
            dtpAppointmentDate.Format = DateTimePickerFormat.Custom;
            dtpAppointmentDate.Location = new Point(298, 180);
            dtpAppointmentDate.Name = "dtpAppointmentDate";
            dtpAppointmentDate.Size = new Size(180, 23);
            dtpAppointmentDate.TabIndex = 1;
            // 
            // btnAddAppointment
            // 
            btnAddAppointment.Location = new Point(298, 237);
            btnAddAppointment.Name = "btnAddAppointment";
            btnAddAppointment.Size = new Size(180, 23);
            btnAddAppointment.TabIndex = 2;
            btnAddAppointment.Text = "Randevu Ata";
            btnAddAppointment.UseVisualStyleBackColor = true;
            btnAddAppointment.Click += btnAddAppointment_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(324, 84);
            label1.Name = "label1";
            label1.Size = new Size(126, 15);
            label1.TabIndex = 3;
            label1.Text = "Randevu Atama Paneli";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(235, 133);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Ad Soyad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(235, 188);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 5;
            label3.Text = "Tarih";
            // 
            // RandevuAtama
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAddAppointment);
            Controls.Add(dtpAppointmentDate);
            Controls.Add(cmbUsers);
            Name = "RandevuAtama";
            Text = "RandevuAtama";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbUsers;
        private DateTimePicker dtpAppointmentDate;
        private Button btnAddAppointment;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}