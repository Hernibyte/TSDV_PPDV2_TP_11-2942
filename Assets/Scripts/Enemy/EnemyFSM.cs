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
    [HideInInspector] public RespawnPoint myRespawnPoint;
    [HideInInspector] public RespawnPoint respawnA;
    [HideInInspector] public RespawnPoint respawnB;

    private Vector3 lastPosTarget;
    private bool shootFlag;

    public EnemyState myState;
    public EnemyType myType;
    private float timerToShoot;

    private void Start()
    {
        shootFlag = false;
        targetSet = false;
        targetEnemy = FindObjectOfType<Player>();
    }

    public void Update()
    {
        if (GameManager.Get() != null)
        {
            if (!GameManager.Get().IsGamePaused())
            {
                CalcTimePerShoot();

                switch (myType)
                {
                    case EnemyType.EnemyA:
                        EnemyA_Behaviour();
                        break;
                    case EnemyType.EnemyB:
                        EnemyB_Behaviour();
                        break;
                    case EnemyType.BossEnemy:
                        break;
                }
            }
        }
    }

    public void EnemyA_Behaviour()
    {
        if (targetEnemy != null)
        {
            switch (myState)
            {
                case EnemyState.GoingToTarget:

                    if (Vector3.Distance(transform.position, lastPosTarget) >= distanceToGoBack)
                        lastPosTarget = Movement_Target(targetEnemy.transform.position, ref targetSet);
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
    }

    public void EnemyB_Behaviour()
    {
        if (targetEnemy != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                            Quaternion.LookRotation(transform.forward, -targetEnemy.transform.position.normalized), speed * Time.deltaTime);
            switch (myState)
            {
                case EnemyState.GoingToTarget:

                    if (transform.position != lastPosTarget)
                    {
                        if (myRespawnPoint != null)
                            lastPosTarget = Movement_PointToPoint(respawnA.transform.position, respawnB.transform.position, myRespawnPoint, ref targetSet);
                    }
                    else
                        myState = EnemyState.GoingBack;


                    if (transform.position.x >= (lastPosTarget.x * 0.2f) && !shootFlag)
                        myState = EnemyState.Attacking;

                    //Debug.Log(lastPosTarget * 0.5f);
                    break;
                case EnemyState.Attacking:

                    Shoot_Target(targetEnemy.transform, ref readyToShoot);

                    shootFlag = true;
                    myState = EnemyState.GoingToTarget;

                    break;
                case EnemyState.GoingBack:
                    if (myRespawnPoint != null)
                    {
                        if (transform.position != myRespawnPoint.transform.position)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, myRespawnPoint.transform.position, (speed * 3) * Time.deltaTime);
                        }
                        else
                        {
                            myState = EnemyState.GoingToTarget;
                            targetSet = false;
                            shootFlag = false;
                        }
                    }
                    break;
            }
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