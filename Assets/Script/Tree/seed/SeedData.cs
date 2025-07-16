using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tree/SeedData")]
public class SeedData : ScriptableObject {
    public string seedName;
    public List<GameObject> growthPrefabs; // 0단계, 1단계, ... 성장 프리팹들
    public List<int> growthThresholds; // 각 단계별 최소 성장 수치 (예: 0, 10, 20, ...)
}
