using UnityEngine;
using DG.Tweening;

public class HamburgerDropdown : MonoBehaviour {
    public RectTransform dropdownPanel;
    public CanvasGroup canvasGroup;
    public float showY = -160f;
    public float hideY = 0f;
    public float duration = 0.3f;

    private bool isOpen = false;

    void Start() {
        dropdownPanel.anchoredPosition = new Vector2(dropdownPanel.anchoredPosition.x, hideY);
        dropdownPanel.localScale = Vector3.zero;
        canvasGroup.alpha = 0;
        dropdownPanel.gameObject.SetActive(false);
    }

    public void ToggleDropdown() {
        isOpen = !isOpen;

        if (isOpen) {
            dropdownPanel.gameObject.SetActive(true);
            dropdownPanel.DOAnchorPosY(showY, duration).SetEase(Ease.OutCubic);
            dropdownPanel.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
            canvasGroup.DOFade(1, duration);
        }
        else {
            dropdownPanel.DOAnchorPosY(hideY, duration).SetEase(Ease.InCubic);
            dropdownPanel.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
            canvasGroup.DOFade(0, duration).OnComplete(() => dropdownPanel.gameObject.SetActive(false));
        }
    }
}
