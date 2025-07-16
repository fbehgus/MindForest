// StoreLetter.cs (파일 이름과 클래스 이름이 일치해야 합니다)
using Firebase.Firestore; // 이 네임스페이스를 추가합니다.

    [FirestoreData]
    public class StoreLetter // 클래스 이름이 SerializedLetter로 변경됨
    {
        [FirestoreProperty]
        public string Content { get; set; }

        [FirestoreProperty]
        public string Timestamp { get; set; }

        public StoreLetter() { } // 기본 생성자도 SerializedLetter로 변경

        public StoreLetter(string content, string timestamp)
        {
            Content = content;
            Timestamp = timestamp;
        }
    }