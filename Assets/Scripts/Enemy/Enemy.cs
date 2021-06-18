using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    [SerializeField] float energy = 100f;

    public void TakeDamage(float damage){
        energy -= damage;
        If_Die();
    }

    void If_Die(){
        if(energy <= 0f)
            Destroy(gameObject);
    }
}
