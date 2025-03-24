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
    public partial class HomePage : Form
    {
        private FirestoreDb db;
        public HomePage()
        {
            InitializeComponent();
            FirebaseService firebaseService = new FirebaseService("randevutakipsistemi-24565-firebase-adminsdk-fbsvc-e0c50fa208.json");
            db = firebaseService.FirestoreDb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RandevuAtama randevuAtama = new RandevuAtama();
            randevuAtama.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RandevuGuncelleme randevuGuncelleme = new RandevuGuncelleme();
            randevuGuncelleme.Show();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            updateRandevu();

        }

        private async void updateRandevu()
        {
            CollectionReference usersRef = db.Collection("users");
            QuerySnapshot usersSnapshot = await usersRef.GetSnapshotAsync();

            DateTime simdikiZaman = DateTime.Now;

            foreach (DocumentSnapshot userDocument in usersSnapshot.Documents)
            {
                // Kullanıcı verilerini al
                var user = userDocument.ConvertTo<Dictionary<string, object>>();
                var appointments = (List<object>)user["Appointments"]; // List<object> olarak al

                // Randevuları kontrol et
                for (int i = 0; i < appointments.Count; i++)
                {
                    // Her bir randevuyu Dictionary<string, object> olarak cast et
                    var appointment = (Dictionary<string, object>)appointments[i];

                    // Randevu tarihini DateTime'a çevir
                    DateTime randevuTarihi = DateTime.ParseExact(appointment["Date"].ToString(), "dd.MM.yyyy HH:mm", null);

                    // Eğer randevu tarihi geçmişse ve tamamlanmamışsa
                    if (randevuTarihi < simdikiZaman && !(bool)appointment["IsCompleted"])
                    {
                        // Randevuyu tamamla
                        appointment["IsCompleted"] = true;

                        // Toplam borçtan seans ücretini düş
                        double seansUcreti = Convert.ToDouble(user["SessionPrice"]);
                        double toplamBorc = (double)user["TotalDebt"];

                        if (toplamBorc >= seansUcreti)
                        {
                            user["TotalDebt"] = toplamBorc - seansUcreti;
                        }
                        else
                        {
                            MessageBox.Show($"{user["Name"]} için borç yetersiz!");
                        }

                        // Randevuyu güncelle
                        appointments[i] = appointment;
                    }
                }

                // Kullanıcıyı güncelle
                user["Appointments"] = appointments;
                await usersRef.Document(userDocument.Id).SetAsync(user, SetOptions.MergeAll);

                // Güncelleme yapıldığını bildir
                MessageBox.Show($"{user["Name"]} için randevular güncellendi!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KullaniciListeleme kullaniciListeleme = new KullaniciListeleme();
            kullaniciListeleme.Show();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            updateRandevu();
        }
    }
}
