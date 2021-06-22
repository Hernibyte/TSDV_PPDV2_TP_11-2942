using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBackground : MonoBehaviour
{
    SpriteRenderer sprite;

    void Awake(){
        sprite = GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer GetSprite(){
        return sprite;
    }
}
