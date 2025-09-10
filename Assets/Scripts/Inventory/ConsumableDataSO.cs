using UnityEngine;


public enum ConsumableTypeEnum
{
    Heal,
    Damage
}

[CreateAssetMenu(fileName = "Consumable SO", menuName = "Items Data / New Consumable SO")]

public class ConsumableDataSO : AtributtesControllerSO
{
    [SerializeField] ConsumableTypeEnum _type;
    [SerializeField] int value;

    public ConsumableTypeEnum TpeCmle { get => _type; set => _type = value; }
    public int Vl { get => value; set => this.value = value; }
}