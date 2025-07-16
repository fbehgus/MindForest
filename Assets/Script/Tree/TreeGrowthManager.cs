using UnityEngine;

public class TreeGrowthManager : MonoBehaviour {
    public int growthValue; // ���� ��ġ
    public Transform treeParent;

    public bool isPlanted = false; //���ѽɾ�����������

    private GameObject currentTree;
    private SeedData currentSeed;

    public void PlantSeed(SeedData seed) {
        if (isPlanted) {
            Debug.LogWarning("�̹� ������ �ɾ��� �־��!");
            return; // �̹� �ɾ������� ����
        }

        currentSeed = seed;
        growthValue = 0;
        isPlanted = true; // �ɾ��� ���·� ǥ��
        UpdateTreeVisual();
    }

    public void IncreaseGrowth(int v) {
        if (!isPlanted) return; // �ƹ��͵� ���� �ʾҴٸ� ���� ����
        growthValue = growthValue+v;
        UpdateTreeVisual();
    }

    private void UpdateTreeVisual() {
        if (currentSeed == null || currentSeed.growthPrefabs.Count == 0)
            return;

        // ���� Ʈ�� ����
        if (currentTree != null)
            Destroy(currentTree);

        // �ش� ���� ��ġ�� �´� ������ ã��
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
