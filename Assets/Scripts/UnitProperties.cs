using UnityEngine;

[CreateAssetMenu(fileName = "UnitProperties", menuName = "UnitProperties", order = 1)]
public class UnitProperties : ScriptableObject
{
    public float MoveSpeed = 5f;
    public float AirMoveSpeed = 2f;
    public float JumpHeight = 2f;
    public float RotationSpeed = 5f;
    public float WeaponRange = 10f;
}