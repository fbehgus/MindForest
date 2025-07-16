using UnityEngine;
using DG.Tweening;

public class CameraSwipeController : MonoBehaviour {
    [Header("카메라 & 타겟")]
    public Camera mainCamera;
    public Transform[] cameraTargets;
    public float moveDuration = 0.5f;
    public Ease easeType = Ease.InOutCubic;

    [Header("화면별 UI 그룹")]
    public GameObject[] shopUI;
    public GameObject[] mainUI;
    public GameObject[] statsUI;

    private Tween currentTween;

    public void MoveToPage(int index) {
        if (index < 0 || index >= cameraTargets.Length) return;

        // 카메라 이동 처리
        Vector3 currentPos = mainCamera.transform.position;
        Vector3 targetPos = new Vector3(cameraTargets[index].position.x, currentPos.y, currentPos.z);

        if (currentTween != null && currentTween.IsActive()) currentTween.Kill();

        currentTween = mainCamera.transform.DOMove(targetPos, moveDuration)
                                           .SetEase(easeType)
                                           .OnComplete(() => ActivateUI(index));
    }

    void ActivateUI(int index) {
        // 0: 상점, 1: 메인, 2: 통계
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
