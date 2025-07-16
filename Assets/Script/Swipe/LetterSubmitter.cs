using UnityEngine;
using TMPro;

public class LetterSubmitter : MonoBehaviour {
    public TMP_InputField inputField;
    public FakeDatabase fakeDB;
    public TreeGrowthManager TreeGrowthManager;


    public void SubmitLetter() {
        string content = inputField.text;
        TreeGrowthManager.IncreaseGrowth(1);

        if (string.IsNullOrWhiteSpace(content)) {
            Debug.Log("내용이 비어있습니다.");
            return;
        }

        fakeDB.AddLetter(content);
        Debug.Log("제출 완료!");

        inputField.text = "";
    }
}
