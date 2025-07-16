using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    public GameObject settingsUI;
    public BGMManager BGMManager;
    private void Start() {
       settingsUI.SetActive(false);

    }


    public void OpenSettings() {
        Debug.Log("���� �޴� ����!");
        if (settingsUI != null)
            settingsUI.SetActive(true);
    }
    public void closeSettings() {
        Debug.Log("���� �޴� �ݱ�!");
            settingsUI.SetActive(false);
    }

    public void ToggleMusic() {
        BGMManager.ToggleMute();
    }

    public void QuitGame() {
        Debug.Log("���� ����");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
