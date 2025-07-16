using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject {
    public string itemName;              // ������ �̸�
    public ItemCategory itemType;        // Seed, WaterCan, Shovel
    public Sprite icon;                  // UI�� ��� ������
    public GameObject prefab;            // �ν��Ͻ�ȭ�� ������
}
