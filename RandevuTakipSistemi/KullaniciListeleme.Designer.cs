namespace RandevuTakipSistemi
{
    partial class KullaniciListeleme
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
            comboBoxUsers = new ComboBox();
            label1 = new Label();
            textBoxName = new TextBox();
            textBoxPhone = new TextBox();
            textBoxPaidAmount = new TextBox();
            textBoxTotalDebt = new TextBox();
            dataGridViewAppointments = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            button1 = new Button();
            textBoxToplamOdenen = new TextBox();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointments).BeginInit();
            SuspendLayout();
            // 
            // comboBoxUsers
            // 
            comboBoxUsers.FormattingEnabled = true;
            comboBoxUsers.Location = new Point(133, 99);
            comboBoxUsers.Name = "comboBoxUsers";
            comboBoxUsers.Size = new Size(181, 23);
            comboBoxUsers.TabIndex = 0;
            comboBoxUsers.SelectedIndexChanged += comboBoxUsers_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(133, 81);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 1;
            label1.Text = "Kullanıcıyı Seçin";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(131, 152);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(183, 23);
            textBoxName.TabIndex = 2;
            // 
            // textBoxPhone
            // 
            textBoxPhone.Location = new Point(131, 193);
            textBoxPhone.Name = "textBoxPhone";
            textBoxPhone.Size = new Size(183, 23);
            textBoxPhone.TabIndex = 3;
            // 
            // textBoxPaidAmount
            // 
            textBoxPaidAmount.Location = new Point(133, 235);
            textBoxPaidAmount.Name = "textBoxPaidAmount";
            textBoxPaidAmount.Size = new Size(181, 23);
            textBoxPaidAmount.TabIndex = 4;
            // 
            // textBoxTotalDebt
            // 
            textBoxTotalDebt.Location = new Point(133, 274);
            textBoxTotalDebt.Name = "textBoxTotalDebt";
            textBoxTotalDebt.Size = new Size(181, 23);
            textBoxTotalDebt.TabIndex = 5;
            // 
            // dataGridViewAppointments
            // 
            dataGridViewAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAppointments.Location = new Point(371, 99);
            dataGridViewAppointments.Name = "dataGridViewAppointments";
            dataGridViewAppointments.Size = new Size(386, 277);
            dataGridViewAppointments.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(68, 160);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 7;
            label2.Text = "Ad Soyad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 201);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 8;
            label3.Text = "Telefon Numarası";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(39, 243);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 9;
            label4.Text = "Seans Ücreti";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(51, 282);
            label5.Name = "label5";
            label5.Size = new Size(74, 15);
            label5.TabIndex = 10;
            label5.Text = "Toplam Borç";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(371, 81);
            label6.Name = "label6";
            label6.Size = new Size(66, 15);
            label6.TabIndex = 11;
            label6.Text = "Randevular";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(305, 24);
            label7.Name = "label7";
            label7.Size = new Size(87, 15);
            label7.TabIndex = 12;
            label7.Text = "Kullanıcı Paneli";
            // 
            // button1
            // 
            button1.Location = new Point(131, 353);
            button1.Name = "button1";
            button1.Size = new Size(183, 23);
            button1.TabIndex = 13;
            button1.Text = "Güncelle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBoxToplamOdenen
            // 
            textBoxToplamOdenen.Location = new Point(133, 314);
            textBoxToplamOdenen.Name = "textBoxToplamOdenen";
            textBoxToplamOdenen.Size = new Size(181, 23);
            textBoxToplamOdenen.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(40, 322);
            label8.Name = "label8";
            label8.Size = new Size(85, 15);
            label8.TabIndex = 15;
            label8.Text = "Yaptığı Ödeme";
            // 
            // KullaniciListeleme
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label8);
            Controls.Add(textBoxToplamOdenen);
            Controls.Add(button1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dataGridViewAppointments);
            Controls.Add(textBoxTotalDebt);
            Controls.Add(textBoxPaidAmount);
            Controls.Add(textBoxPhone);
            Controls.Add(textBoxName);
            Controls.Add(label1);
            Controls.Add(comboBoxUsers);
            Name = "KullaniciListeleme";
            Text = "KullaniciListeleme";
            Load += KullaniciListeleme_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxUsers;
        private Label label1;
        private TextBox textBoxName;
        private TextBox textBoxPhone;
        private TextBox textBoxPaidAmount;
        private TextBox textBoxTotalDebt;
        private DataGridView dataGridViewAppointments;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button button1;
        private TextBox textBoxToplamOdenen;
        private Label label8;
    }
}