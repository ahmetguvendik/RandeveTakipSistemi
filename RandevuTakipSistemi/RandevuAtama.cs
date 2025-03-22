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
            string userId = cmbUsers.SelectedValue.ToString(); // Seçilen kullanıcının ID'si
            DateTime appointmentDate = dtpAppointmentDate.Value; // Seçilen tarih

            var appointment = new
            {
                Date = appointmentDate.ToString("dd.MM.yyyy HH:mm"),
                IsCompleted = false
            };

            // Firestore'a randevu ekle
            DocumentReference userRef = db.Collection("users").Document(userId);
            await userRef.UpdateAsync("Appointments", FieldValue.ArrayUnion(appointment));
            MessageBox.Show("Randevu başarıyla eklendi!");
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
