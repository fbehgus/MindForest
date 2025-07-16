using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class FakeDatabase : MonoBehaviour {
    private string filePath;
    private LetterList data = new LetterList();

    void Awake() {
        filePath = Application.persistentDataPath + "/letters.json";
        Load();
    }

    public void AddLetter(string content) {
        data.letters.Add(new SerializedLetter
        {
            content = content,
            timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });
        Save();
    }

    public void Save() {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("편지가 저장되었습니다! → " + filePath);
    }

    public void Load() {
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<LetterList>(json);
        }
    }

    public void ClearAllLetters() {
        data.letters.Clear();
        Save();
    }
}
