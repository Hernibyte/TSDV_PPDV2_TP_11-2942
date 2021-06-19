using UnityEngine;
using System.Collections;

interface IHittable
{
    void TakeDamage(float damage);
}

public class Character : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] float shootSpawnPosition;
    public float speed;
    private Vector3 targetPos;
    public float timer = 0.5f;

    public void CalculateFireRateShoot(){
        timer += Time.deltaTime;
    }

    void Shoot_Default(ShootLayer _shootLayer, float _damage)
    {
        if(timer > 0.5f){
            Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), Quaternion.identity);
            bulletGo.myDirection = Direction.Up;
            bulletGo.localDamage = _damage;
            bulletGo.shootLayer = _shootLayer;
            timer = 0f;
        }
    }

    void Shoot_Burst(ShootLayer _shootLayer, float _damage){
        if(timer > 1f){
            StartCoroutine(Burst(_shootLayer, _damage));
            timer = 0f;
        }
    }

    IEnumerator Burst(ShootLayer _shootLayer, float _damage){
        for(int i = 0; i < 3; i++){
            Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), Quaternion.identity);
            bulletGo.myDirection = Direction.Up;
            bulletGo.localDamage = _damage;
            bulletGo.shootLayer = _shootLayer;
            yield return new WaitForSeconds(0.15f);
        }

        yield return null;
    }

    void Shoot_Cono(ShootLayer _shootLayer, float _damage){
        if(timer > 1f){
            Quaternion a = new Quaternion(0f, 0f, -0.4f, 1f);
            Quaternion b = new Quaternion(0f, 0f, 1f, 1f);

            for(int i = 0; i < 8; i-=-1){
                Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), a);
                bulletGo.myDirection = Direction.Up;
                bulletGo.localDamage = _damage;
                bulletGo.shootLayer = _shootLayer;
                a = Quaternion.Lerp(a, b, 1f/10f);
            }
            timer = 0f;
        }
    }
    public void Shoot_Type(SpecificPowerUp _type, ShootLayer _shootLayer, float _damage)
    {
        switch (_type)
        {
            case SpecificPowerUp.SimpleShoot:
                Shoot_Default(_shootLayer, _damage);
                break;
            case SpecificPowerUp.BurstShoot:
                Shoot_Burst(_shootLayer, _damage);
                break;
            case SpecificPowerUp.ConeShoot:
                Shoot_Cono(_shootLayer, _damage);
                break;
            case SpecificPowerUp.BeamShoot:

                break;
        }
    }

    public void Shoot_Target(Transform target, ref bool readyToShoot)
    {
        if (readyToShoot)
        {
            Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z),
                Quaternion.identity);
            bulletGo.myDirection = Direction.Down;
            Vector3 directionToTarget = target.position - transform.position;
            bulletGo.transform.rotation = Quaternion.LookRotation(transform.forward, -directionToTarget.normalized);
            readyToShoot = false;
        }
    }

    public Vector3 Movement_Target(Transform target, ref bool lastPosSelected)
    {
        if (!lastPosSelected)
        {
            targetPos = target.position;
            lastPosSelected = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, (speed * 3) * Time.deltaTime);

        return targetPos;
    }

    public void Movement_Clamped(float speed)
    {

        if (transform.localPosition.x > -(Screen.width / 2))
            if (Input.GetKey(KeyCode.LeftArrow))
                transform.position += new Vector3(-1f * speed, 0f, 0f);

        if (transform.localPosition.x < (Screen.width / 2))
            if (Input.GetKey(KeyCode.RightArrow))
                transform.position += new Vector3(1f * speed, 0f, 0f);

        if (transform.localPosition.y > -(Screen.height / 2))
            if (Input.GetKey(KeyCode.DownArrow))
                transform.position += new Vector3(0f, -1f * speed, 0f);

        if (transform.localPosition.y < (Screen.height / 2))
            if (Input.GetKey(KeyCode.UpArrow))
                transform.position += new Vector3(0f, 1f * speed, 0f);
    }
}
