using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Eventos y delegados inventario
    public delegate void InventoryUpdatedDelegate ();
    public InventoryUpdatedDelegate ItemUpdated;
    public InventoryUpdatedDelegate ItemRemoved;
    public InventoryUpdatedDelegate ItemAdded;


    Dictionary<int, int> _items = new()
    {
        {3, 10},
        {7, 6 },
        {8, 9},
        {9, 10},
        {10, 11},
    };

    public Dictionary<int, int> Items { get => _items; set => _items = value; }

    public void AddItem(int id, int amount)
    {
        if (!Items.ContainsKey(id))
        {
            Items.Add(id, amount);
            ItemAdded?.Invoke();
        }
        else
        {
            Items[id] += amount;
            ItemUpdated?.Invoke();
        }

        ShowInventory();
    }

    public void RemoveItem(int id, int amount)
    {
        if (Items.ContainsKey(id))
        {
            Items[id] -= amount;

            if (Items[id] <= 0)
            {
                Items.Remove(id);
                ItemRemoved?.Invoke();
            } 
            else 
            {
                ItemUpdated?.Invoke();
            }
        }

        ShowInventory();
    }

    public void ShowInventory()
    {
        foreach (var item in Items)
        {
            Debug.Log(item);
        }
    }
}
