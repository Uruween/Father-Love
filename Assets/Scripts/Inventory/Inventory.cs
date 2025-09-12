using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Eventos y delegados inventario
    public delegate void InventoryUpdatedDelegate();
    public InventoryUpdatedDelegate ItemRemoved;
    public InventoryUpdatedDelegate ItemAdded;


    public delegate void ItemUsedDelegate(int itemId);
    public ItemUsedDelegate ItemUsed;


    Dictionary<int, int> _items = new();

    public Dictionary<int, int> Items { get => _items; set => _items = value; }
    public void AddItem(int id)
    {
        if (!Items.ContainsKey(id))
        {
            Items.Add(id, 1);
            ItemAdded?.Invoke();
        }
        else
        {
            return;
        }

        ShowInventory();
    }

    public void RemoveItem(int id)
    {
        if (Items.ContainsKey(id))
        {
                Items.Remove(id);
                ItemRemoved?.Invoke();
        }

        ShowInventory();
    }
    public void UseItem(int id)
    {
        if (Items.ContainsKey(id))
        {
            ItemUsed?.Invoke(id);
        }
    }

    public void ShowInventory()
    {
        foreach (var item in Items)
        {
            Debug.Log(item);
        }
    }
}
