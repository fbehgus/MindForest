using UnityEngine;
using DG.Tweening;

public class CameraSwipeController : MonoBehaviour {
    [Header("ī�޶� & Ÿ��")]
    public Camera mainCamera;
    public Transform[] cameraTargets;
    public float moveDuration = 0.5f;
    public Ease easeType = Ease.InOutCubic;

    [Header("ȭ�麰 UI �׷�")]
    public GameObject[] shopUI;
    public GameObject[] mainUI;
    public GameObject[] statsUI;

    private Tween currentTween;

    public void MoveToPage(int index) {
        if (index < 0 || index >= cameraTargets.Length) return;

        // ī�޶� �̵� ó��
        Vector3 currentPos = mainCamera.transform.position;
        Vector3 targetPos = new Vector3(cameraTargets[index].position.x, currentPos.y, currentPos.z);

        if (currentTween != null && currentTween.IsActive()) currentTween.Kill();

        currentTween = mainCamera.transform.DOMove(targetPos, moveDuration)
                                           .SetEase(easeType)
                                           .OnComplete(() => ActivateUI(index));
    }

    void ActivateUI(int index) {
        // 0: ����, 1: ����, 2: ���
        SetUIGroupActive(shopUI, index == 0);
        SetUIGroupActive(mainUI, index == 1);
        SetUIGroupActive(statsUI, index == 2);
    }

    void SetUIGroupActive(GameObject[] group, bool isActive) {
        if (group == null) return;
        foreach (var go in group) {
            if (go != null) go.SetActive(isActive);
        }
    }
}
