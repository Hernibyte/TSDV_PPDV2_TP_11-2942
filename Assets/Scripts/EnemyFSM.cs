using UnityEngine;

public class EnemyFSM : Character
{
    [SerializeField] Character targetEnemy;
    [SerializeField] private bool readyToShoot;
    [SerializeField] private float timePerShoot;
    private float timer;

    public void Update()
    {
        CalcTimePerShoot();   

        Shoot_Target(targetEnemy.transform, ref readyToShoot);
    }

    public void CalcTimePerShoot()
    {
        if (timer <= timePerShoot)
            timer += Time.deltaTime;
        else
        {
            readyToShoot = true;
            timer = 0;
        }
    }
}
