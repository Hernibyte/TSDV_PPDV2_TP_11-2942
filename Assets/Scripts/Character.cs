using UnityEngine;

public class Character : MonoBehaviour
{
    //public ShootType shootType;
    [SerializeField] Bullet bullet;
    [SerializeField] float shootSpawnPosition;
    public float speed;

    public void Shoot_Default(Vector3 direction){
        if(Input.GetKeyDown(KeyCode.E)){
            Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), Quaternion.identity);
            bulletGo.myDirection = Bullet.Direction.Up;
        }
    }

    public void Shoot_Target(Transform target, ref bool readyToShoot)
    {
        if(readyToShoot)
        {
            Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z),
                Quaternion.identity);
            bulletGo.myDirection = Bullet.Direction.Down;
            Vector3 directionToTarget = target.position - transform.position;
            bulletGo.transform.rotation = Quaternion.LookRotation(transform.forward, -directionToTarget.normalized);
            readyToShoot = false;
        }
    }
    public void Movement_Target(Transform target, float speed)
    {

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
