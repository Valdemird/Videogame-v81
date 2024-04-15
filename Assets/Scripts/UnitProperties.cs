using UnityEngine;

[CreateAssetMenu(fileName = "UnitProperties", menuName = "UnitProperties", order = 1)]
public class UnitProperties : ScriptableObject
{
    [Tooltip("The speed at which the unit moves on the ground.")]
    public float MoveSpeed = 5f;

    [Tooltip("The speed at which the unit moves in the air.")]
    public float AirMoveSpeed = 2f;

    [Tooltip("The height of the unit's jump.")]
    public float JumpHeight = 2f;

    [Tooltip("The speed at which the unit rotates.")]
    public float RotationSpeed = 5f;

    [Tooltip("The range of the unit's weapon.")]
    public float WeaponRange = 10f;

    [Tooltip("The delay between shots in seconds.")]
    public float fireRate = 1f;
}