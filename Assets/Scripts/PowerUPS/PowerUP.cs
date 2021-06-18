using UnityEngine;

public class PowerUP : Item
{
    public enum TypePowerUP
    {
        Explosion,
        RestoreEnergy,
        IncrementDamage,
        IncrementFireRate,
        Points_X2,
        Points_X3,
        Points_X4,
        Points_X10
    }
    public TypePowerUP myType;

    public void SpawnPowerUP(float posX, float posY)
    {
        transform.position = new Vector2(posX, posY);
    }

    public void MoveItem()
    {
    }
}
