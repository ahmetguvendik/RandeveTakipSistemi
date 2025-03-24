namespace RandevuTakipSistemi
{
    partial class RandevuGuncelleme
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
            lstAppointments = new ListBox();
            dtpNewAppointmentDate = new DateTimePicker();
            label1 = new Label();
            btnUpdateAppointment = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // cmbUsers
            // 
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(330, 94);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(200, 23);
            cmbUsers.TabIndex = 0;
            cmbUsers.SelectedIndexChanged += cmbUsers_SelectedIndexChanged;
            // 
            // lstAppointments
            // 
            lstAppointments.FormattingEnabled = true;
            lstAppointments.ItemHeight = 15;
            lstAppointments.Location = new Point(36, 48);
            lstAppointments.Name = "lstAppointments";
            lstAppointments.Size = new Size(171, 169);
            lstAppointments.TabIndex = 1;
            lstAppointments.DoubleClick += lstAppointments_DoubleClick;
            // 
            // dtpNewAppointmentDate
            // 
            dtpNewAppointmentDate.CustomFormat = "dd.MM.yyyy HH:mm";
            dtpNewAppointmentDate.Format = DateTimePickerFormat.Custom;
            dtpNewAppointmentDate.Location = new Point(330, 148);
            dtpNewAppointmentDate.Name = "dtpNewAppointmentDate";
            dtpNewAppointmentDate.Size = new Size(200, 23);
            dtpNewAppointmentDate.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(330, 48);
            label1.Name = "label1";
            label1.Size = new Size(154, 15);
            label1.TabIndex = 4;
            label1.Text = "Randevu Güncelleme Paneli";
            // 
            // btnUpdateAppointment
            // 
            btnUpdateAppointment.Location = new Point(330, 194);
            btnUpdateAppointment.Name = "btnUpdateAppointment";
            btnUpdateAppointment.Size = new Size(200, 23);
            btnUpdateAppointment.TabIndex = 0;
            btnUpdateAppointment.Text = "Güncelle";
            btnUpdateAppointment.Click += btnUpdateAppointment_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(272, 102);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 5;
            label2.Text = "Kullanıcı";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(213, 156);
            label3.Name = "label3";
            label3.Size = new Size(113, 15);
            label3.TabIndex = 6;
            label3.Text = "Güncellenecek Tarih";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 20);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 7;
            label4.Text = "Randevu Tarihleri";
            // 
            // button1
            // 
            button1.Location = new Point(552, 194);
            button1.Name = "button1";
            button1.Size = new Size(148, 23);
            button1.TabIndex = 8;
            button1.Text = "Randevuyu Sil";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // RandevuGuncelleme
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnUpdateAppointment);
            Controls.Add(label1);
            Controls.Add(dtpNewAppointmentDate);
            Controls.Add(lstAppointments);
            Controls.Add(cmbUsers);
            Name = "RandevuGuncelleme";
            Text = "RandevuGuncelleme";
            Load += RandevuGuncelleme_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbUsers;
        private ListBox lstAppointments;
        private DateTimePicker dtpNewAppointmentDate;
        private Label label1;
        private Button btnUpdateAppointment;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button1;
    }
}