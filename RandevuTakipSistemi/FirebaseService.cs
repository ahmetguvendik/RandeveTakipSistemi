﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace RandevuTakipSistemi
{

    public class FirebaseService
    {
        public FirestoreDb FirestoreDb { get; private set; }

        public FirebaseService(string jsonPath)
        {
            // FirebaseApp zaten başlatılmadıysa başlat
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "//");
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(jsonPath)
                });
            }

            // Firestore bağlantısını kur
            FirestoreDb = FirestoreDb.Create("//");
        }
    }

}

