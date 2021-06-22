using System.Collections;
using UnityEngine;

interface IHittable
{
    void TakeDamage(float damage);
}

public class Character : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] float shootSpawnPosition;
    [SerializeField] public float fireRate;
    public float speed;
    private Vector3 targetPos;
    public float timer = 0.5f;
    [SerializeField] ScreenShake cameraShake;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] GameObject explosionPrefab;

    private void Start()
    {
        fireRate = 1.0f;
        cameraShake = Camera.main.GetComponent<ScreenShake>();
    }
    public void CalculateFireRateShoot(){
        timer += Time.deltaTime;
    }

    public void ExplosionAttack(){
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 10000f, enemyMask);
        foreach(Collider2D collider in hitColliders){
            GameObject go = Instantiate(explosionPrefab, collider.transform.position, Quaternion.identity);
            go.transform.localScale = new Vector3(150f, 150f, 0f);
            Destroy(collider);
        }
    }

    void Shoot_Default(ShootLayer _shootLayer, float _damage)
    {
        if(timer > fireRate * 0.5f)
        {
            GameManager.Get()?.BulletShooted();
            AudioManager.Get()?.Play("shoot1");
            Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), Quaternion.identity);
            bulletGo.myDirection = Direction.Up;
            bulletGo.localDamage = _damage;
            bulletGo.shootLayer = _shootLayer;
            timer = 0f;
            StartCoroutine(cameraShake?.CameraShake(.2f, 4f));
        }
    }

    void Shoot_Burst(ShootLayer _shootLayer, float _damage){
        if(timer > fireRate)
        {
            AudioManager.Get()?.Play("shoot2");
            StartCoroutine(Burst(_shootLayer, _damage));
            timer = 0f;
        }
    }

    IEnumerator Burst(ShootLayer _shootLayer, float _damage){
        for(int i = 0; i < 3; i++){
            GameManager.Get()?.BulletShooted();
            Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), Quaternion.identity);
            StartCoroutine(cameraShake.CameraShake(.2f, 2f));
            bulletGo.myDirection = Direction.Up;
            bulletGo.localDamage = _damage;
            bulletGo.shootLayer = _shootLayer;
            yield return new WaitForSeconds(0.15f);
        }

        yield return null;
    }

    void Shoot_Cono(ShootLayer _shootLayer, float _damage){
        if(timer > fireRate)
        {
            Quaternion a = new Quaternion(0f, 0f, -0.4f, 1f);
            Quaternion b = new Quaternion(0f, 0f, 1f, 1f);
            AudioManager.Get()?.Play("shoot3");

            for (int i = 0; i < 8; i-=-1){
                GameManager.Get()?.BulletShooted();
                Bullet bulletGo = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + shootSpawnPosition, transform.position.z), a);
                bulletGo.myDirection = Direction.Up;
                bulletGo.localDamage = _damage;
                bulletGo.shootLayer = _shootLayer;
                a = Quaternion.Lerp(a, b, 1f/10f);
            }
            timer = 0f;
            StartCoroutine(cameraShake?.CameraShake(.2f, 4f));
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

    public Vector3 Movement_Target(Vector3 target, ref bool lastPosSelected)
    {
        if (!lastPosSelected)
        {
            targetPos = target;
            lastPosSelected = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, (speed * 3) * Time.deltaTime);

        return targetPos;
    }

    public Vector3 Movement_PointToPoint(Vector3 targetA, Vector3 targetB, RespawnPoint myStartPoint, ref bool lastPosSelected)
    {
        if (!lastPosSelected)
        {
            switch (myStartPoint.respawnID)
            {
                case 1:
                targetPos = new Vector3(targetB.x,targetA.y, targetA.z);
                    break;
                case 2:
                targetPos = new Vector3(targetA.x, targetB.y, targetB.z);
                    break;
            }
            lastPosSelected = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos/* * Mathf.Sin(Time.deltaTime * speed)*/, (speed * 3) * Time.deltaTime);

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
