using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum ArmorTypeEnum
{
    keys, treasure
}
 [CreateAssetMenu(fileName = "Armor SO", menuName = "Items Data / New Armor SO")]

public class ArmorItemDataSO : AtributtesControllerSO
{
    [SerializeField] ArmorTypeEnum _type;
    [SerializeField] int armorValue;

    public ArmorTypeEnum TpRmr { get => _type; set => _type = value; }
    public int ArmrVl { get => armorValue; set => armorValue = value; }
}
