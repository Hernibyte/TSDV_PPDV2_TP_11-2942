using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    void Update(){
        Shoot_Default(new Vector3(0f, 100f, 0f));
    }

    void LateUpdate()
    {
        Movement_Clamped(speed);
    }
}
