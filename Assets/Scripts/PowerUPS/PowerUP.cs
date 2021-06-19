using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypePowerUP
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
        BeamShoot
    }

public class PowerUp : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    public TypePowerUP typePowerUP;

    public void ChangeTypePowerUp(TypePowerUP type){
        typePowerUP = type;
    }

    void LateUpdate(){
        transform.position += -transform.up * speed;
    }
}
