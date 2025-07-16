using UnityEngine;
using System.Collections;
using DG.Tweening;


public class DraggableObject : MonoBehaviour {
    private Vector3 originalPosition;
    private bool isDragging = false;
    private int activeTouchId = -1;

    public TreeGrowthManager treeGrowthManager;
    public SeedData basicSeed;

    void Start() {
        originalPosition = transform.position;
    }

    void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleMouseInput() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.gameObject == this.gameObject) {
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(0) && isDragging) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0) && isDragging) {
            isDragging = false;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject != this.gameObject) {
                StartCoroutine(PerformTaskThenReturn(hit.gameObject));
            }
            else {
                ReturnToOrigin();
            }
        }
    }

    void HandleTouchInput() {
        foreach (Touch touch in Input.touches) {
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchWorldPos.z = 0;

            if (touch.phase == TouchPhase.Began) {
                Collider2D hit = Physics2D.OverlapPoint(touchWorldPos);
                if (hit != null && hit.gameObject == this.gameObject) {
                    isDragging = true;
                    activeTouchId = touch.fingerId;
                }
            }
            else if (touch.fingerId == activeTouchId) {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                    if (isDragging) {
                        transform.position = touchWorldPos;
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                    isDragging = false;
                    activeTouchId = -1;
                    Collider2D hit = Physics2D.OverlapPoint(touchWorldPos);

                    if (hit != null && hit.gameObject != this.gameObject) {
                        StartCoroutine(PerformTaskThenReturn(hit.gameObject));
                    }
                    else {
                        ReturnToOrigin();
                    }
                }
            }
        }
    }

    void ReturnToOrigin() {
        transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutCubic);
    }

    IEnumerator PerformTaskThenReturn(GameObject target) {
        string itemTag = this.gameObject.tag;
        string targetTag = target.tag;

        // 조합별 효과 처리
        if (itemTag == "WaterItem" && targetTag == "tree") {
            treeGrowthManager.IncreaseGrowth(1);
        }
        else if (itemTag == "basicSeed" && targetTag == "ground") {
            if (treeGrowthManager != null) {
                treeGrowthManager.PlantSeed(basicSeed);
            }
        }
        else if (itemTag == "sap" && targetTag == "tree") {
                 treeGrowthManager.isPlanted = false;
                 Destroy(target);
                 Debug.Log("나무를 성공적으로 제거");

        }
        else {
            Debug.Log("적용할 수 없는 조합: " + itemTag + " → " + targetTag);
        }

        yield return new WaitForSeconds(1.5f);
        ReturnToOrigin();
    }
}
