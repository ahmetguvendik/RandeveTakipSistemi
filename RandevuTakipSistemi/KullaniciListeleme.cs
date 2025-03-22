using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace RandevuTakipSistemi
{
    public partial class KullaniciListeleme : Form
    {
        private FirestoreDb db;
        public KullaniciListeleme()
        {
            InitializeComponent();
            FirebaseService firebaseService = new FirebaseService("randevutakipsistemi-24565-firebase-adminsdk-fbsvc-e0c50fa208.json");
            db = firebaseService.FirestoreDb;
            LoadUsers();
        }

        private void KullaniciListeleme_Load(object sender, EventArgs e)
        {
            // DataGridView'in sütunlarını ayarla
            dataGridViewAppointments.Columns.Add("Date", "Tarih");
            dataGridViewAppointments.Columns.Add("Status", "Durum");
            // Sütun genişliklerini otomatik ayarla
            dataGridViewAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void LoadUsers()
        {
            // Firestore'dan kullanıcıları çek
            CollectionReference usersRef = db.Collection("users");
            QuerySnapshot usersSnapshot = await usersRef.GetSnapshotAsync();

            // ComboBox'ı temizle ve kullanıcıları ekle
            comboBoxUsers.Items.Clear();
            foreach (DocumentSnapshot userDocument in usersSnapshot.Documents)
            {
                var user = userDocument.ConvertTo<Dictionary<string, object>>();
                comboBoxUsers.Items.Add(user["Name"].ToString());
            }
        }
        private async void comboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen kullanıcının adını al
            string selectedUserName = comboBoxUsers.SelectedItem.ToString();

            // Firestore'dan kullanıcının detaylarını çek
            CollectionReference usersRef = db.Collection("users");
            QuerySnapshot usersSnapshot = await usersRef.WhereEqualTo("Name", selectedUserName).GetSnapshotAsync();

            if (usersSnapshot.Documents.Count > 0)
            {
                var userDocument = usersSnapshot.Documents[0];
                var user = userDocument.ConvertTo<Dictionary<string, object>>();

                // Kullanıcı bilgilerini göster
                textBoxName.Text = user["Name"].ToString();
                textBoxPhone.Text = user["Phone"].ToString();
                textBoxTotalDebt.Text = user["TotalDebt"].ToString();
                textBoxPaidAmount.Text = user["SessionPrice"].ToString();
                // Seans ücretini al
                double seansUcreti = (double)user["SessionPrice"];

                // Tamamlanan randevuları say
                var appointments = (List<object>)user["Appointments"];
                int tamamlananRandevuSayisi = 0;

                foreach (var appointment in appointments)
                {
                    var appt = (Dictionary<string, object>)appointment;
                    if ((bool)appt["IsCompleted"])
                    {
                        tamamlananRandevuSayisi++;
                    }
                }

                // Toplam ödenen miktarı hesapla
                double toplamOdenen = tamamlananRandevuSayisi * seansUcreti;

                // Kalan borcu hesapla
                double toplamBorc = (double)user["TotalDebt"];
                double kalanBorc = toplamBorc - toplamOdenen;

                // Bilgileri TextBox'larda göster
                textBoxToplamOdenen.Text = toplamOdenen.ToString();


                // Randevu geçmişini DataGridView'de göster
                dataGridViewAppointments.Rows.Clear(); // Mevcut satırları temizle

                foreach (var appointment in appointments)
                {
                    var appt = (Dictionary<string, object>)appointment;
                    dataGridViewAppointments.Rows.Add(
                        appt["Date"].ToString(),
                        (bool)appt["IsCompleted"] ? "Tamamlandı" : "Bekliyor"
                    );
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Seçilen kullanıcının adını al
            string selectedUserName = comboBoxUsers.SelectedItem.ToString();

            // Firestore'dan kullanıcının belgesini çek
            CollectionReference usersRef = db.Collection("users");
            QuerySnapshot usersSnapshot = await usersRef.WhereEqualTo("Name", selectedUserName).GetSnapshotAsync();

            if (usersSnapshot.Documents.Count > 0)
            {
                var userDocument = usersSnapshot.Documents[0];

                // TextBox'lardaki değerleri al
                string name = textBoxName.Text;
                string phone = textBoxPhone.Text;
                double totalDebt = double.Parse(textBoxTotalDebt.Text);
                double sessionPrice = double.Parse(textBoxPaidAmount.Text);
                double toplamOdeme = double.Parse(textBoxToplamOdenen.Text);// Yeni bir TextBox ekleyin veya mevcut bir değeri kullanın

                // Kullanıcı bilgilerini güncelle
                var updates = new Dictionary<string, object>
        {
            { "Name", name },
            { "Phone", phone },
            { "TotalDebt", totalDebt },
            { "SessionPrice", sessionPrice }
        };

                // Firestore'da güncelleme yap
                await userDocument.Reference.UpdateAsync(updates);

                // Kullanıcıya bilgi ver
                MessageBox.Show("Kullanıcı bilgileri güncellendi!");
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı!");
            }
        }
    }
}
