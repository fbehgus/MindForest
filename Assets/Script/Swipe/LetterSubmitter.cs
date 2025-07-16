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
            Debug.Log("������ ����ֽ��ϴ�.");
            return;
        }

        fakeDB.AddLetter(content);
        Debug.Log("���� �Ϸ�!");

        inputField.text = "";
    }
}
