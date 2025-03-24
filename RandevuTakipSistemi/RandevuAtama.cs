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

namespace RandevuTakipSistemi
{
    public partial class RandevuAtama : Form
    {
        private FirestoreDb db;
        public RandevuAtama()
        {
            InitializeComponent();
            FirebaseService firebaseService = new FirebaseService("randevutakipsistemi-24565-firebase-adminsdk-fbsvc-e0c50fa208.json");
            db = firebaseService.FirestoreDb;
            LoadUsers();
        }

        private async void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin!");
                return;
            }

            try
            {
                string userId = cmbUsers.SelectedValue.ToString();
                DateTime appointmentDate = dtpAppointmentDate.Value;

                // Kullanıcı verilerini getir
                DocumentReference userRef = db.Collection("users").Document(userId);
                DocumentSnapshot userSnapshot = await userRef.GetSnapshotAsync();

                if (userSnapshot.Exists)
                {
                    var user = userSnapshot.ConvertTo<Dictionary<string, object>>();
                    double sessionPrice = Convert.ToDouble(user["SessionPrice"]);
                    var appointments = user.ContainsKey("Appointments") ?
                        (List<object>)user["Appointments"] :
                        new List<object>();

                    // Yeni randevu oluştur
                    var newAppointment = new Dictionary<string, object>
            {
                { "Date", appointmentDate.ToString("dd.MM.yyyy HH:mm") },
                { "IsCompleted", false }
            };

                    // Randevuyu listeye ekle
                    appointments.Add(newAppointment);

                    // Toplam borcu güncelle (randevu sayısı x seans ücreti)
                    double newTotalDebt = appointments.Count * sessionPrice;

                    // Firestore'da güncelleme yap
                    await userRef.UpdateAsync(new Dictionary<string, object>
            {
                { "Appointments", appointments },
                { "TotalDebt", newTotalDebt }
            });

                    MessageBox.Show($"Randevu eklendi!\nToplam randevu: {appointments.Count}\nGüncel borç: {newTotalDebt} TL");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevu eklenirken hata oluştu: " + ex.Message);
            }
        }

        private async void LoadUsers()
        {
            QuerySnapshot usersSnapshot = await db.Collection("users").GetSnapshotAsync();
            cmbUsers.DataSource = usersSnapshot.Documents.Select(doc => new
            {
                Id = doc.Id,
                Name = doc.GetValue<string>("Name")
            }).ToList();
            cmbUsers.DisplayMember = "Name";
            cmbUsers.ValueMember = "Id";
        }
    }
}
