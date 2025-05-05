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
            FirebaseService firebaseService = new FirebaseService("//");
            db = firebaseService.FirestoreDb;
            LoadUsers();
        }

        private async void btnAddAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = cmbUsers.SelectedValue.ToString();
                DocumentReference userRef = db.Collection("users").Document(userId);
                DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    var user = snapshot.ConvertTo<Dictionary<string, object>>();
                    double sessionPrice = Convert.ToDouble(user["SessionPrice"]);
                    double paidAmount = user.ContainsKey("PaidAmount") ? Convert.ToDouble(user["PaidAmount"]) : 0;

                    // Randevuları al
                    var appointments = user.ContainsKey("Appointments") ?
                        (List<object>)user["Appointments"] :
                        new List<object>();

                    // YENİ RANDEVU EKLE (Otomatik tamamlanmamış olarak)
                    appointments.Add(new Dictionary<string, object>
            {
                { "Date", dtpAppointmentDate.Value.ToString("dd.MM.yyyy HH:mm") },
                { "IsCompleted", false }
            });

                    // SADECE TAMAMLANMAMIŞ RANDEVULARI SAY
                    int pendingAppointments = appointments.Count(a =>
                        !((Dictionary<string, object>)a)["IsCompleted"].Equals(true));

                    double newTotalDebt = pendingAppointments * sessionPrice;
                    double remainingDebt = newTotalDebt - paidAmount;

                    await userRef.UpdateAsync(new Dictionary<string, object>
            {
                { "Appointments", appointments },
                { "TotalDebt", newTotalDebt },
                { "RemainingDebt", Math.Max(0, remainingDebt) }
            });

                    MessageBox.Show($"Randevu eklendi!\n" +
                                  $"Tamamlanmamış randevu: {pendingAppointments}\n" +
                                  $"Toplam borç: {newTotalDebt} TL\n" +
                                  $"Ödenen: {paidAmount} TL\n" +
                                  $"Kalan borç: {remainingDebt} TL");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
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
