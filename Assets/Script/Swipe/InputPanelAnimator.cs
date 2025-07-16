using UnityEngine;
using DG.Tweening;

public class InputPanelAnimator : MonoBehaviour {
    public RectTransform panelRect;
    public float showY = 0f;         // �ö�� ��ġ
    public float hideY = -800f;      // ������ ��ġ
    public float duration = 0.5f;    // �ִϸ��̼� �ӵ�

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
