// StoreLetter.cs
using Firebase.Firestore;
using System;

    [FirestoreData]
    public class StoreLetter
    {

        [FirestoreProperty]
        public string SenderID { get; set; }

        [FirestoreProperty]
        public string Content { get; set; }

        [FirestoreProperty]
        public Timestamp Timestamp { get; set; }

        public StoreLetter() { }

        public StoreLetter(string senderID, string content, Timestamp timestamp)
        {
            SenderID = senderID;    
            Content = content;
            Timestamp = timestamp;
        }
    }