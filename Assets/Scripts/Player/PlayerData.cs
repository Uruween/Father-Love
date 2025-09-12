using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.AI;
using Unity.Mathematics;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public Vector3 position;
    public float health;
    public List <GameObject> inventoryItems;
    public quaternion rotationCamera;
}
