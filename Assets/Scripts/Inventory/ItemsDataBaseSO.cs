using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Items Data Base SO", menuName = "Items Data / New Items Data Base SO")]
public class ItemsDataBaseSO : ScriptableObject
{
    [SerializeField] List<AtributtesControllerSO> _items;

    public List<AtributtesControllerSO> Items { get => _items; set => _items = value; }

    public AtributtesControllerSO SearchItemByID (int id) 
    {
        return Items.Find(x=>x.Id == id);
    }
}
