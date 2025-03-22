using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Google.Cloud.Firestore;

namespace RandevuTakipSistemi
{
    public partial class Form1 : Form
    {
        private FirestoreDb db;
        public Form1()
        {
            InitializeComponent();
            // Firebase bağlantısını kur
            FirebaseService firebaseService = new FirebaseService("randevutakipsistemi-24565-firebase-adminsdk-fbsvc-e0c50fa208.json");
            db = firebaseService.FirestoreDb;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // TextBox'lardan verileri al
            string name = textBox3.Text;
            string phone = textBox1.Text;
            double sessionPrice = double.Parse(textBox2.Text);
            double totalDebt = sessionPrice * 4; // Toplam borç hesapla

            // Firestore'a eklenecek veri
            var user = new
            {
                Name = name,
                Phone = phone,
                SessionPrice = sessionPrice,
                TotalDebt = totalDebt,
                Appointments = new List<object>() // Randevuları tutacak liste

            };

            // Firestore'a veri ekle
            DocumentReference addedUserRef = await db.Collection("users").AddAsync(user);
            MessageBox.Show("Kullanıcı başarıyla eklendi!");
        }
    }
}
