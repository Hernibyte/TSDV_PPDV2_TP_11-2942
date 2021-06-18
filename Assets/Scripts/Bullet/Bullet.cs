using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    void LateUpdate(){
        transform.position += transform.up * speed;
    }
}
