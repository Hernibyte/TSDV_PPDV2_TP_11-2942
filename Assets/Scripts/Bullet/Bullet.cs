using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    public enum Direction
    {
        Up,
        Down
    }
    public Direction myDirection;

    void LateUpdate(){
        switch (myDirection)
        {
            case Direction.Up:
                transform.position += transform.up * speed;
                break;
            case Direction.Down:
                transform.position += -transform.up * speed;
                break;
        }
    }
}
