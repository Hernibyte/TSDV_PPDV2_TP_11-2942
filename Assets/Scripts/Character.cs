using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public ShootType shootType;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootSpawnPosition;
    public float speed;

    public void Shoot_Default(Vector3 direction){
        if(Input.GetKeyDown(KeyCode.E)){
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), Quaternion.identity);
        }
    }

    public void Movement_Clamped(float speed){

        if(transform.localPosition.x > -(Screen.width/2))
            if(Input.GetKey(KeyCode.LeftArrow))
                transform.position += new Vector3(-1f * speed, 0f, 0f);

        if(transform.localPosition.x < (Screen.width/2))
            if(Input.GetKey(KeyCode.RightArrow))
                transform.position += new Vector3(1f * speed, 0f, 0f);


        if(transform.localPosition.y > -(Screen.height/2))
            if(Input.GetKey(KeyCode.DownArrow))
                transform.position += new Vector3(0f, -1f * speed, 0f);

        if(transform.localPosition.y < (Screen.height/2))
            if(Input.GetKey(KeyCode.UpArrow))
                transform.position += new Vector3(0f, 1f * speed, 0f);

    }
}
