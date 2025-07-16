using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject {
    public string itemName;              // 아이템 이름
    public ItemCategory itemType;        // Seed, WaterCan, Shovel
    public Sprite icon;                  // UI에 띄울 아이콘
    public GameObject prefab;            // 인스턴스화할 프리팹
}
