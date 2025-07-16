using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
    [Header("––– Scroll & Page Indicator –––")]
    [SerializeField] private Scrollbar scrollBar;
    [SerializeField] private Transform[] circleContents;

    [Header("Swipe 설정")]
    [SerializeField] private float swipeTime = 0.2f;
    [SerializeField] private float swipeDistance = 50f;

    // --- 새로 추가 ---
    [Header("탭 바 토글 (0: Game, 1: SwipeUI, 2: DragDrop)")]
    [SerializeField] private Toggle[] tabToggles;

    private float[] scrollPageValues;
    private float valueDistance;
    private int currentPage = 0;
    private int maxPage;
    private float startTouchX, endTouchX;
    private bool isSwipeMode;
    private float circleContentScale = 1.6f;

    private void Awake()
    {
        // 페이지 값 셋업
        maxPage = transform.childCount;
        scrollPageValues = new float[maxPage];
        valueDistance = 1f / (maxPage - 1f);
        for (int i = 0; i < maxPage; i++)
            scrollPageValues[i] = valueDistance * i;

        // 탭 바 토글 클릭 ↔ 페이지 연동
        for (int i = 0; i < tabToggles.Length; i++)
        {
            int idx = i;
            tabToggles[i].onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                    StartCoroutine(OnSwipeOneStep(idx));
            });
        }
    }

    private void Start()
    {
        // 초기 페이지, 탭 상태 동기화
        var contentRect = GetComponent<RectTransform>();
        if (contentRect != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        }
        SetScrollBarValue(0);
        UpdateTabBar();
    }

    public void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollBar.value = scrollPageValues[index];
        UpdateTabBar();
    }

    private void Update()
    {
        UpdateInput();
        UpdateCircleContent();
    }

    private void UpdateInput()
    {
        if (isSwipeMode) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) startTouchX = Input.mousePosition.x;
        if (Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            var t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) startTouchX = t.position.x;
            if (t.phase == TouchPhase.Ended)
            {
                endTouchX = t.position.x;
                UpdateSwipe();
            }
        }
#endif
    }

    private void UpdateSwipe()
    {
        if (Mathf.Abs(endTouchX - startTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        bool isLeft = endTouchX > startTouchX;
        if (isLeft && currentPage > 0) currentPage--;
        else if (!isLeft && currentPage < maxPage - 1) currentPage++;
        else return;

        StartCoroutine(OnSwipeOneStep(currentPage));
    }

    private IEnumerator OnSwipeOneStep(int targetPage)
    {
        isSwipeMode = true;
        float start = scrollBar.value, elapsed = 0;

        while (elapsed < swipeTime)
        {
            elapsed += Time.deltaTime;
            scrollBar.value = Mathf.Lerp(start, scrollPageValues[targetPage], elapsed / swipeTime);
            yield return null;
        }

        isSwipeMode = false;
        currentPage = targetPage;
        UpdateTabBar();
    }

    private void UpdateCircleContent()
    {
        if (circleContents.Length == 0 || scrollPageValues.Length == 0) return;
        int cnt = Mathf.Min(circleContents.Length, scrollPageValues.Length);

        for (int i = 0; i < cnt; i++)
        {
            var t = circleContents[i];
            t.localScale = Vector3.one;
            var img = t.GetComponent<Image>();
            if (img != null) img.color = Color.white;

            if (scrollBar.value >= scrollPageValues[i] - valueDistance * .5f &&
                scrollBar.value < scrollPageValues[i] + valueDistance * .5f)
            {
                t.localScale = Vector3.one * circleContentScale;
                if (img != null) img.color = Color.black;
            }
        }
    }

    // --- 탭 바 상태를 토글로 업데이트 ---
    private void UpdateTabBar()
    {
        for (int i = 0; i < tabToggles.Length; i++)
        {
            // 이벤트 중복 방지용
            tabToggles[i].SetIsOnWithoutNotify(i == currentPage);
        }
    }
}