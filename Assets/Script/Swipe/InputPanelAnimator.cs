using UnityEngine;
using DG.Tweening;

public class InputPanelAnimator : MonoBehaviour {
    public RectTransform panelRect;
    public float showY = 0f;         // 올라올 위치
    public float hideY = -800f;      // 내려간 위치
    public float duration = 0.5f;    // 애니메이션 속도

    void Start() {
        panelRect.anchoredPosition = new Vector2(0, hideY);
        panelRect.gameObject.SetActive(false);
    }

    public void ShowPanel() {
        panelRect.gameObject.SetActive(true);
        panelRect.DOAnchorPosY(showY, duration).SetEase(Ease.OutCubic);
    }

    public void HidePanel() {
        panelRect.DOAnchorPosY(hideY, duration).SetEase(Ease.InCubic)
            .OnComplete(() => panelRect.gameObject.SetActive(false));
    }
}
