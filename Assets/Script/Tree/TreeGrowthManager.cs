using UnityEngine;

public class TreeGrowthManager : MonoBehaviour {
    public int growthValue; // 성장 수치
    public Transform treeParent;

    public bool isPlanted = false; //씨앗심어졌는지여부

    private GameObject currentTree;
    private SeedData currentSeed;

    public void PlantSeed(SeedData seed) {
        if (isPlanted) {
            Debug.LogWarning("이미 씨앗이 심어져 있어요!");
            return; // 이미 심어졌으면 무시
        }

        currentSeed = seed;
        growthValue = 0;
        isPlanted = true; // 심어진 상태로 표시
        UpdateTreeVisual();
    }

    public void IncreaseGrowth(int v) {
        if (!isPlanted) return; // 아무것도 심지 않았다면 성장 금지
        growthValue = growthValue+v;
        UpdateTreeVisual();
    }

    private void UpdateTreeVisual() {
        if (currentSeed == null || currentSeed.growthPrefabs.Count == 0)
            return;

        // 이전 트리 제거
        if (currentTree != null)
            Destroy(currentTree);

        // 해당 성장 수치에 맞는 프리팹 찾기
        GameObject selectedPrefab = currentSeed.growthPrefabs[0];
        for (int i = 0; i < currentSeed.growthThresholds.Count; i++) {
            if (growthValue >= currentSeed.growthThresholds[i]) {
                selectedPrefab = currentSeed.growthPrefabs[i];
            }
            else break;
        }

        currentTree = Instantiate(selectedPrefab, treeParent);
    }
}
