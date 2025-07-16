using UnityEngine;
using TMPro;

public class LetterSubmitter : MonoBehaviour
{
    public TMP_InputField inputField;
    public FirebaseFirestoreDatabase firebaseDB;
    public TreeGrowthManager TreeGrowthManager; // 기존과 동일

    public void SubmitLetter()
    {
        string content = inputField.text;
        string senderID = "senderID";

        if (string.IsNullOrWhiteSpace(content))
        {
            Debug.Log("내용이 비어있습니다.");
            return;
        }

        // TreeGrowthManager.IncreaseGrowth(1); // Firebase 성공 시 호출하도록 옮길 수 있음

        // Firebase Firestore에 편지 추가
        // AddLetter 메서드는 비동기(async void) 이므로, 바로 실행하고 기다리지 않습니다.
        firebaseDB.AddLetter(content, senderID);

        // 편지 제출이 성공하면 나무 성장을 증가시키고 싶다면,
        // AddLetter 내부에서 Firestore에 데이터가 성공적으로 추가된 후
        // TreeGrowthManager.IncreaseGrowth(1); 을 호출하는 것이 더 안전합니다.
        // 현재는 Firebsae로 데이터 전송 요청이 성공하는 즉시 호출됩니다.
        TreeGrowthManager.IncreaseGrowth(1); // 지금은 AddLetter 호출 직후에 실행

        Debug.Log("편지 제출 요청 완료!"); // 이제 로컬 저장 대신 Firestore로의 요청이므로 "요청 완료"가 더 적절합니다.

        inputField.text = "";
    }
}