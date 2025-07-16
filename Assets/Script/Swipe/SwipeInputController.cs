using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class SwipeInputController : MonoBehaviour {
    [Header("UI 참조")]
    public GameObject inputPanel;
    public TMP_InputField inputField;
    public InputPanelAnimator panelAnimator;
    public RectTransform swipeTargetUI;   // 스와이프 대상 UI
    [Header("비활성화할 버튼들")]
    public List<Button> targetButtons;

    private Vector2 startPos;
    private string saveKey = "letter_draft";
    private float swipeThreshold = 50f;
    private bool isSwipeOnTarget = false;

    void Start() {
        if (PlayerPrefs.HasKey(saveKey))
            inputField.text = PlayerPrefs.GetString(saveKey);

        inputPanel.SetActive(false);

        inputField.onValueChanged.AddListener((value) => {
            PlayerPrefs.SetString(saveKey, value);
        });
    }

    void Update() {
        // 모바일 터치
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                startPos = touch.position;
                isSwipeOnTarget = RectTransformUtility.RectangleContainsScreenPoint(swipeTargetUI, startPos);
            }
            else if (touch.phase == TouchPhase.Ended) {
                Vector2 endPos = touch.position;
                float deltaY = endPos.y - startPos.y;

                if (Mathf.Abs(deltaY) > swipeThreshold) {
                    if (deltaY > 0 && isSwipeOnTarget)
                        ShowInputPanel();   // 위로 스와이프 → 오브젝트 위일 때만
                    else if (deltaY < 0)
                        HideInputPanel();   // 아래로 스와이프 → 무조건 닫기
                }

                isSwipeOnTarget = false;
            }
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0)) {
            startPos = Input.mousePosition;
            isSwipeOnTarget = RectTransformUtility.RectangleContainsScreenPoint(swipeTargetUI, startPos);
        }
        else if (Input.GetMouseButtonUp(0)) {
            float deltaY = ((Vector2)Input.mousePosition).y - startPos.y;

            if (Mathf.Abs(deltaY) > swipeThreshold) {
                if (deltaY > 0 && isSwipeOnTarget)
                    ShowInputPanel();   // 위로 스와이프 (오브젝트 위)
                else if (deltaY < 0)
                    HideInputPanel();   // 아래로 스와이프 (무조건)
            }

            isSwipeOnTarget = false;
        }
#endif
    }

    void ShowInputPanel() {
        panelAnimator.ShowPanel();
        inputField.ActivateInputField();
        foreach (var btn in targetButtons)
            btn.interactable = false;
    }

    void HideInputPanel() {
        panelAnimator.HidePanel();

        foreach (var btn in targetButtons)
            btn.interactable = true;
    }
}
