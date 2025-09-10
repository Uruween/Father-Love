using System;
using UnityEngine;

public class AtributtesControllerSO : ScriptableObject
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] Sprite _icon;
    [SerializeField] GameObject _prefab;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
}
