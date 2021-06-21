using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]int enemiesINeedRespawn;
    [Header("RESPAWN INFO")]
    [SerializeField] public EnemyType enemiesIWillRespawn;
    [SerializeField] public float timePerRespawn;
    [SerializeField] private int enemiesRespawned;
    [SerializeField][Tooltip("Only needed if enemy is ENEMY_B")] public RespawnPoint pointA;
    [SerializeField][Tooltip("Only needed if enemy is ENEMY_B")] public RespawnPoint pointB;
    [Header("ENEMY TO RESPAWN (PREFAB)")]
    [SerializeField] public EnemyFSM enemy;
    private float timer;

    private void Start()
    {
        enemiesRespawned = 0;
        if (GameManager.Get() != null)
            enemiesINeedRespawn = (int)(GameManager.Get().amountEnemiesLevel_1 * 0.5f);
        timer = timePerRespawn;
    }

    private void Update()
    {
        if (timer < timePerRespawn)
            timer += Time.deltaTime;
        else
        {
            if(enemiesRespawned < enemiesINeedRespawn)
            {

                switch (enemiesIWillRespawn)    
                {
                    case EnemyType.EnemyA:
                        RespawnEnemyA();
                        break;
                    case EnemyType.EnemyB:
                        RespawnEnemyB();
                        break;
                    case EnemyType.BossEnemy:
                        break;
                }
                enemiesRespawned++;
                timer = 0;
            }
        }
    }

    public void RespawnEnemyA()
    {
        Instantiate(enemy, new Vector3(Random.Range(100, Screen.width), transform.position.y, transform.position.z), Quaternion.identity);
    }

    public void RespawnEnemyB()
    {
        EnemyFSM enemyCreated;
        Vector3 pointAVec =pointA.transform.position;
        Vector3 pointBVec =pointB.transform.position;
        int randomSpawnPoint = Random.Range(0, 100);
        if(randomSpawnPoint <= 50)
        {
            enemyCreated = Instantiate(enemy, pointAVec, Quaternion.identity);
            enemyCreated.myRespawnPoint = pointA;
            
        }
        else
        {
            enemyCreated = Instantiate(enemy, pointBVec, Quaternion.identity);
            enemyCreated.myRespawnPoint = pointB;
        }
        enemyCreated.respawnA = pointA;
        enemyCreated.respawnB = pointB;
    }
}
