using UnityEngine;

public class EnemyFSM : Character
{
    [SerializeField] Character targetEnemy;
    [SerializeField] private bool readyToShoot;
    [SerializeField] private bool targetSet;
    [SerializeField] private float timePerShoot;
    [SerializeField] private float distanceToGoBack;
    [SerializeField] private float distanceToGoTarget;

    private Vector3 lastPosTarget;
    public enum State
    {
        GoingToTarget,
        Attacking,
        GoingBack
    }
    public State myState;
    private float timerToShoot;

    private void Start()
    {
        targetSet = false;
    }

    public void Update()
    {
        CalcTimePerShoot();

        switch (myState)
        {
            case State.GoingToTarget:

                if (Vector3.Distance(transform.position, lastPosTarget) >= distanceToGoBack)
                    lastPosTarget = Movement_Target(targetEnemy.transform, ref targetSet);
                else
                    myState = State.Attacking;

                break;
            case State.Attacking:

                Shoot_Target(targetEnemy.transform, ref readyToShoot);
                myState = State.GoingBack;

                break;
            case State.GoingBack:

                if(Vector3.Distance(transform.position, lastPosTarget) <= distanceToGoTarget)
                    transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                else
                {
                    myState = State.GoingToTarget;
                    targetSet = false;
                }
                break;
        }

    }
    public void CalcTimePerShoot()
    {
        if (timerToShoot <= timePerShoot)
            timerToShoot += Time.deltaTime;
        else if(timerToShoot >= timePerShoot && myState == State.Attacking)
        {
            readyToShoot = true;
            timerToShoot = 0;
        }
    }
}