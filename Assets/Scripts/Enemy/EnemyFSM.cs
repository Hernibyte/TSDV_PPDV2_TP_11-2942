using UnityEngine;
public enum EnemyState
{
    GoingToTarget,
    Attacking,
    GoingBack
}

public enum EnemyType
{
    EnemyA,
    EnemyB,
    BossEnemy
}

public class EnemyFSM : Character
{
    [SerializeField] Character targetEnemy;
    [SerializeField] private bool readyToShoot;
    [SerializeField] private bool targetSet;
    [SerializeField] private float timePerShoot;
    [SerializeField] private float distanceToGoBack;
    [SerializeField] private float distanceToGoTarget;

    private Vector3 lastPosTarget;

    public EnemyState myState;
    public EnemyType myType;
    private float timerToShoot;

    private void Start()
    {
        targetSet = false;
        targetEnemy = FindObjectOfType<Player>();
    }

    public void Update()
    {
        CalcTimePerShoot();

        switch (myType)
        {
            case EnemyType.EnemyA: EnemyA_Behaviour();
                break;
            case EnemyType.EnemyB:
                break;
            case EnemyType.BossEnemy:
                break;
        }
    }

    public void EnemyA_Behaviour()
    {
        switch (myState)
        {
            case EnemyState.GoingToTarget:

                if (Vector3.Distance(transform.position, lastPosTarget) >= distanceToGoBack)
                    lastPosTarget = Movement_Target(targetEnemy.transform, ref targetSet);
                else
                    myState = EnemyState.Attacking;

                break;
            case EnemyState.Attacking:

                Shoot_Target(targetEnemy.transform, ref readyToShoot);
                myState = EnemyState.GoingBack;

                break;
            case EnemyState.GoingBack:

                if (Vector3.Distance(transform.position, lastPosTarget) <= distanceToGoTarget)
                    transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                else
                {
                    myState = EnemyState.GoingToTarget;
                    targetSet = false;
                }
                break;
        }
    }

    public void EnemyB_Behaviour()
    {
        switch (myState)
        {
            case EnemyState.GoingToTarget:

                if (Vector3.Distance(transform.position, lastPosTarget) >= distanceToGoBack)
                    lastPosTarget = Movement_Target(targetEnemy.transform, ref targetSet);
                else
                    myState = EnemyState.Attacking;

                break;
            case EnemyState.Attacking:

                Shoot_Target(targetEnemy.transform, ref readyToShoot);
                myState = EnemyState.GoingBack;

                break;
            case EnemyState.GoingBack:

                if (Vector3.Distance(transform.position, lastPosTarget) <= distanceToGoTarget)
                    transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                else
                {
                    myState = EnemyState.GoingToTarget;
                    targetSet = false;
                }
                break;
        }
    }

    public void CalcTimePerShoot()
    {
        if (timerToShoot <= timePerShoot)
            timerToShoot += Time.deltaTime;
        else if (timerToShoot >= timePerShoot && myState == EnemyState.Attacking)
        {
            readyToShoot = true;
            timerToShoot = 0;
        }
    }
}