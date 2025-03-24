using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandevuTakipSistemi
{
    public partial class RandevuGuncelleme : Form
    {
        private FirestoreDb db;

        public RandevuGuncelleme()
        {
            InitializeComponent();
            FirebaseService firebaseService = new FirebaseService("randevutakipsistemi-24565-firebase-adminsdk-fbsvc-e0c50fa208.json");
            db = firebaseService.FirestoreDb;
            LoadUsers();
        }

        private async void RandevuGuncelleme_Load(object sender, EventArgs e)
        {
            // Uygulama açıldığında randevuları kontrol et


            MessageBox.Show("Randevular kontrol edildi!" + DateTime.Now);
        }

        private async Task CheckAppointments()
        {
            try
            {
                QuerySnapshot usersSnapshot = await db.Collection("users").GetSnapshotAsync();
                foreach (var userDoc in usersSnapshot.Documents)
                {
                    var user = userDoc.ConvertTo<Dictionary<string, object>>();
                    if (user.ContainsKey("Appointments") && user["Appointments"] is List<object> appointmentsList)
                    {
                        foreach (var appointmentObj in appointmentsList)
                        {
                            if (appointmentObj is Dictionary<string, object> appointment)
                            {
                                string dateTimeStr = appointment.ContainsKey("dateTime") ? appointment["dateTime"].ToString() : null;
                                bool isCompleted = appointment.ContainsKey("IsCompleted") && (bool)appointment["IsCompleted"];

                                if (!string.IsNullOrEmpty(dateTimeStr) && !isCompleted)
                                {
                                    // Tarih ve saat bilgisini doğru şekilde parse et
                                    DateTime appointmentDateTime = DateTime.ParseExact(
                                        dateTimeStr,
                                        "dd.MM.yyyy HH:mm",
                                        CultureInfo.InvariantCulture
                                    );

                                    // Debug: Randevu tarihini ve şu anki zamanı göster
                                    MessageBox.Show($"Randevu Tarihi: {appointmentDateTime}\nŞu Anki Zaman: {DateTime.Now}");

                                    // Randevu zamanı geçmişse
                                    if (DateTime.Now >= appointmentDateTime)
                                    {
                                        // Borç düşme işlemi
                                        double sessionPrice = user.ContainsKey("SessionPrice") ? (double)user["SessionPrice"] : 0;
                                        double totalDebt = user.ContainsKey("TotalDebt") ? (double)user["TotalDebt"] : 0;

                                        if (totalDebt >= sessionPrice)
                                        {
                                            totalDebt -= sessionPrice;
                                            appointment["IsCompleted"] = true; // Randevuyu tamamlandı olarak işaretle

                                            // Firestore'da güncelle
                                            DocumentReference userRef = db.Collection("users").Document(userDoc.Id);
                                            await userRef.UpdateAsync(new Dictionary<string, object>
                                    {
                                        { "TotalDebt", totalDebt },
                                        { "Appointments", appointmentsList }
                                    });

                                            MessageBox.Show($"{user["Name"]} için randevu tamamlandı ve borç güncellendi!");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Randevu zamanı henüz gelmedi.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Randevu tarihi eksik veya boş!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevu kontrolü sırasında bir hata oluştu: " + ex.Message);
            }
        }


        // Kullanıcıları ComboBox'a yükle
        private async void LoadUsers()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcılar yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        // ComboBox'tan kullanıcı seçildiğinde randevuları ListBox'a yükle
        private async void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue != null)
            {
                string userId = cmbUsers.SelectedValue.ToString();
                await LoadAppointments(userId);
            }
        }

        // Randevuları ListBox'a yükle
        private async Task LoadAppointments(string userId)
        {
            try
            {
                DocumentReference userRef = db.Collection("users").Document(userId);
                DocumentSnapshot userSnapshot = await userRef.GetSnapshotAsync();
                if (userSnapshot.Exists)
                {
                    var user = userSnapshot.ConvertTo<Dictionary<string, object>>();

                    // Appointments alanının varlığını ve türünü kontrol et
                    if (user.ContainsKey("Appointments") && user["Appointments"] is List<object> appointmentsList)
                    {
                        // Randevuları ListBox'a yükle
                        lstAppointments.Items.Clear(); // ListBox'ı temizle
                        foreach (var appointmentObj in appointmentsList)
                        {
                            if (appointmentObj is Dictionary<string, object> appointment)
                            {
                                string date = appointment.ContainsKey("Date") ? appointment["Date"].ToString() : "Tarih Yok";
                                bool isCompleted = appointment.ContainsKey("IsCompleted") && (bool)appointment["IsCompleted"];
                                string appointmentInfo = $"{date} - {(isCompleted ? "Tamamlandı" : "Bekliyor")}";
                                lstAppointments.Items.Add(appointmentInfo); // ListBox'a ekle
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcının randevusu bulunamadı veya randevu listesi geçersiz.");
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevular yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        // Randevu tarihini güncelle
        private async void btnUpdateAppointment_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue == null || lstAppointments.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir kullanıcı ve randevu seçin!");
                return;
            }

            try
            {
                string userId = cmbUsers.SelectedValue.ToString();
                DateTime newAppointmentDate = dtpNewAppointmentDate.Value;

                // Kullanıcının randevularını al
                DocumentReference userRef = db.Collection("users").Document(userId);
                DocumentSnapshot userSnapshot = await userRef.GetSnapshotAsync();
                var user = userSnapshot.ConvertTo<Dictionary<string, object>>();

                // Appointments alanının varlığını ve türünü kontrol et
                if (user.ContainsKey("Appointments") && user["Appointments"] is List<object> appointmentsList)
                {
                    var selectedAppointment = appointmentsList[lstAppointments.SelectedIndex] as Dictionary<string, object>;
                    if (selectedAppointment != null)
                    {
                        // Randevu tarihini güncelle
                        selectedAppointment["Date"] = newAppointmentDate.ToString("dd.MM.yyyy HH:mm");

                        // Firestore'da güncelle
                        await userRef.UpdateAsync("Appointments", appointmentsList);
                        MessageBox.Show("Randevu tarihi güncellendi!");

                        // Randevuları yeniden yükle
                        await LoadAppointments(userId);
                    }
                    else
                    {
                        MessageBox.Show("Seçilen randevu geçersiz.");
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcının randevusu bulunamadı veya randevu listesi geçersiz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevu güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void lstAppointments_DoubleClick(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue == null || lstAppointments.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir kullanıcı ve randevu seçin!");
                return;
            }

            try
            {
                string userId = cmbUsers.SelectedValue.ToString();

                // Kullanıcının verilerini al
                DocumentReference userRef = db.Collection("users").Document(userId);
                DocumentSnapshot userSnapshot = await userRef.GetSnapshotAsync();
                var user = userSnapshot.ConvertTo<Dictionary<string, object>>();

                // Appointments kontrolü
                if (user.ContainsKey("Appointments") && user["Appointments"] is List<object> appointmentsList)
                {
                    // Seans ücretini ve toplam borcu al
                    double sessionPrice = Convert.ToDouble(user["SessionPrice"]);
                    double totalDebt = Convert.ToDouble(user["TotalDebt"]);

                    // Kullanıcıya silme işlemini onaylat
                    var confirmResult = MessageBox.Show(
                        $"Bu randevuyu silmek istediğinize emin misiniz?\n\n" +
                        $"Toplam borca {sessionPrice} TL silinecek!\n" +
                        $"Yeni borç: {totalDebt - sessionPrice} TL",
                        "Randevu Silme",
                        MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        // Seçili randevuyu listeden kaldır
                        appointmentsList.RemoveAt(lstAppointments.SelectedIndex);

                        // Toplam borca 1 seans ücreti ekle
                        totalDebt -= sessionPrice;

                        // Firestore'da güncelle
                        await userRef.UpdateAsync(new Dictionary<string, object>
                {
                    { "Appointments", appointmentsList },
                    { "TotalDebt", totalDebt }
                });

                        MessageBox.Show(
                            $"Randevu başarıyla silindi!\n" +
                            $"Yeni borç: {totalDebt} TL");

                        // Listeyi yenile
                        await LoadAppointments(userId);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcının randevusu bulunamadı veya randevu listesi geçersiz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevu silinirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}