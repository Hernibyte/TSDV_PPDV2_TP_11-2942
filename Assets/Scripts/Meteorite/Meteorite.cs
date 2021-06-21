using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour, IHittable
{
    [SerializeField] float speed = 3f;
    [SerializeField] LayerMask playerLayer;
    [HideInInspector] public float localDamage = 40f;
    [SerializeField] float localLive = 200f;

    void LateUpdate() {
        transform.position += -transform.up * speed;
    }

    public void TakeDamage(float _damage){
        localLive -= _damage;
        If_Die();
    }

    void If_Die(){
        if(localLive <= 0f)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(Contains(playerLayer, collider.gameObject.layer)){
            IHittable hit = collider.GetComponent<IHittable>();
            hit.TakeDamage(localDamage);
            Destroy(this.gameObject);
        }
    }

    bool Contains(LayerMask mask, int layer){
        return mask == (mask | (1 << layer));
    }
}
