using UnityEngine;

public enum SpecificPowerUp
{
    Explosion,
    RestoreEnergy,
    IncrementDamage,
    Points_X2,
    Points_X3,
    Points_X4,
    Points_X10,
    SimpleShoot,
    BurstShoot,
    ConeShoot,
    IncreamentFireRate
}

public enum TypePowerUP
{
    Consumible,
    PointsMultiplers,
    ShootType
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] public int ID;
    public TypePowerUP typePowerUP;
    public SpecificPowerUp specificType;

    public void ChangeTypePowerUp(TypePowerUP type)
    {
        typePowerUP = type;
    }

    void LateUpdate()
    {
        transform.position += -transform.up * speed;
    }
}