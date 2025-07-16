// FirebaseFirestoreDatabase.cs
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseFirestoreDatabase : MonoBehaviour
{
    private FirebaseFirestore db;
    private CollectionReference lettersCollection;
    public List<StoreLetter> loadedLetters = new();

    void Awake()
    {
        InitializeFirebase();
    }

    private async void InitializeFirebase()
    {
        Debug.Log("Firebase 초기화 중...");
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (dependencyStatus == DependencyStatus.Available)
        {
            db = FirebaseFirestore.DefaultInstance;
            lettersCollection = db.Collection("letters");
            Debug.Log("Firebase 초기화 성공. Firestore 컬렉션 참조 설정됨.");
            LoadLettersFromFirestore();
        }
        else
        {
            Debug.LogError($"Firebase 종속성 해결 실패: {dependencyStatus}");
        }
    }

    // 편지 추가 (Add)
    public async void AddLetter(string content, string senderID)
    {
        // 1. 저장할 데이터 객체 생성
        StoreLetter newLetter = new StoreLetter
        {
            Content = content,
            Timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            SenderID = senderID,
        };

        // 2. 문서 ID를 결정합니다. 여기서는 senderID을 ID로 사용합니다.
        //    만약 senderID가 비어있거나 고유하지 않다면 문제가 될 수 있으니 주의해야 합니다.
        string documentId = $"{senderID}_{System.DateTime.Now.ToString("yyyyMMdd_HHmmssfff")}";

        // 3. Document() 메서드로 원하는 ID를 가진 문서 참조를 얻은 후, SetAsync()로 데이터를 저장합니다.
        await lettersCollection.Document(documentId).SetAsync(newLetter)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    Debug.Log($"Firestore에 편지 추가 성공! 문서 ID: {documentId}");
                    loadedLetters.Add(newLetter);
                }
                else
                {
                    Debug.LogError($"Firestore에 편지 추가 실패: {task.Exception}");
                }
            });
    }

    // 편지 불러오기 (Read)
    public async void LoadLettersFromFirestore()
    {
        if (lettersCollection == null)
        {
            Debug.LogError("Firestore 컬렉션이 초기화되지 않았습니다.");
            return;
        }

        Debug.Log("Firestore에서 편지 불러오는 중...");
        QuerySnapshot snapshot = await lettersCollection.GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return task.Result;
                }
                else
                {
                    Debug.LogError($"Firestore에서 편지 불러오기 실패: {task.Exception}");
                    return null;
                }
            });

        if (snapshot != null)
        {
            loadedLetters.Clear();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // 변경: ConvertTo<Letter>() 대신 ConvertTo<StoreLetter>()
                    StoreLetter letter = document.ConvertTo<StoreLetter>();
                    if (letter != null)
                    {
                        loadedLetters.Add(letter);
                    }
                }
            }
            Debug.Log($"Firestore에서 편지 {loadedLetters.Count}개 불러오기 성공!");
        }
    }

    // 모든 편지 삭제 (Delete All)
    public async void ClearAllLettersInFirestore()
    {
        if (lettersCollection == null)
        {
            Debug.LogError("Firestore 컬렉션이 초기화되지 않았습니다.");
            return;
        }

        Debug.Log("Firestore의 모든 편지 삭제 중...");
        QuerySnapshot snapshot = await lettersCollection.GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return task.Result;
                }
                else
                {
                    Debug.LogError($"모든 편지 불러오기 실패 (삭제 전): {task.Exception}");
                    return null;
                }
            });

        if (snapshot != null)
        {
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                await document.Reference.DeleteAsync()
                    .ContinueWithOnMainThread(deleteTask =>
                    {
                        if (deleteTask.IsCompletedSuccessfully)
                        {
                            Debug.Log($"문서 삭제 성공: {document.Id}");
                        }
                        else
                        {
                            Debug.LogError($"문서 삭제 실패: {deleteTask.Exception}");
                        }
                    });
            }
            loadedLetters.Clear();
            Debug.Log("Firestore의 모든 편지 삭제 요청 완료!");
        }
    }

    // 특정 편지 삭제 (예시: 문서 ID를 알고 있을 때)
    public async void DeleteLetterById(string documentId)
    {
        if (lettersCollection == null) return;

        DocumentReference docRef = lettersCollection.Document(documentId);
        await docRef.DeleteAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    Debug.Log($"Firestore 문서 삭제 성공: {documentId}");
                    // 로컬 리스트에서도 제거 (옵션)
                    // loadedLetters.RemoveAll(l => l.Content == "삭제된 편지 내용"); // 적절한 조건으로 교체
                }
                else
                {
                    Debug.LogError($"Firestore 문서 삭제 실패: {task.Exception}");
                }
            });
    }

    // 데이터 변경 실시간 감지 (옵션)
    public void ListenForLetterChanges()
    {
        if (lettersCollection == null) return;

        lettersCollection.Listen(snapshot =>
        {
            Debug.Log("Firestore 데이터 변경 감지!");
            loadedLetters.Clear();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // 변경: ConvertTo<Letter>() 대신 ConvertTo<StoreLetter>()
                    StoreLetter letter = document.ConvertTo<StoreLetter>();
                    if (letter != null)
                    {
                        loadedLetters.Add(letter);
                    }
                }
            }
        });
    }
}