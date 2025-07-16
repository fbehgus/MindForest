using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    public GameObject settingsUI;
    public BGMManager BGMManager;
    private void Start() {
       settingsUI.SetActive(false);

    }


    public void OpenSettings() {
        Debug.Log("설정 메뉴 열기!");
        if (settingsUI != null)
            settingsUI.SetActive(true);
    }
    public void closeSettings() {
        Debug.Log("설정 메뉴 닫기!");
            settingsUI.SetActive(false);
    }

    public void ToggleMusic() {
        BGMManager.ToggleMute();
    }

    public void QuitGame() {
        Debug.Log("게임 종료");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
