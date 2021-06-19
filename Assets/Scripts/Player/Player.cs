using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IHittable
{
    [SerializeField] float energy = 100f;
    [SerializeField] float damage;

    

    void Update(){
        Shoot_Default(ShootLayer.Player, damage);
    }

    void LateUpdate()
    {
        Movement_Clamped(speed);
    }

    public void TakeDamage(float damage){
        energy -= damage;
        If_Die();
    }

    void If_Die(){
        if(energy <= 0f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}