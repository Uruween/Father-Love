using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<ItemData> listItems = new();
    public ItemData SearchItemByID(int id)
    {
        return listItems.Find(x => x.id == id);
    }
}
