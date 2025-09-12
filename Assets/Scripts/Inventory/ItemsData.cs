using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/itemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int id;
    public GameObject prefab;
}
