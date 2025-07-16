using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tree/SeedData")]
public class SeedData : ScriptableObject {
    public string seedName;
    public List<GameObject> growthPrefabs; // 0�ܰ�, 1�ܰ�, ... ���� �����յ�
    public List<int> growthThresholds; // �� �ܰ躰 �ּ� ���� ��ġ (��: 0, 10, 20, ...)
}
