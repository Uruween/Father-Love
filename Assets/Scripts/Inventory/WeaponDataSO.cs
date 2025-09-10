using UnityEngine;

[CreateAssetMenu(fileName = "Weapon SO", menuName = "Items Data / New Weapon SO")]
public class WeaponDataSO : AtributtesControllerSO
{
    [SerializeField] int _damage;

    public int Dmg { get => _damage; set => _damage = value; }
}
