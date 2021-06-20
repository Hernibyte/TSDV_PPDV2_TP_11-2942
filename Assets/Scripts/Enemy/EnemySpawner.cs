using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]int enemiesINeedRespawn;
    [Header("RESPAWN INFO")]
    [SerializeField] public EnemyType enemiesIWillRespawn;
    [SerializeField] public float timePerRespawn;
    [Header("ENEMY TO RESPAWN (PREFAB)")]
    [SerializeField] public EnemyFSM enemy;
    private float timer;

    private void Start()
    {
        if(GameManager.Get() != null)
            enemiesINeedRespawn = Mathf.RoundToInt(GameManager.Get().amountEnemiesLevel_1 * 0.5f);
    }

    private void Update()
    {
        if (timer < timePerRespawn)
            timer += Time.deltaTime;
        else
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

            timer = 0;
        }
    }

    public void RespawnEnemyA()
    {
        Instantiate(enemy, new Vector3(Random.Range(100, Screen.width), transform.position.y, transform.position.z), Quaternion.identity);
    }

    public void RespawnEnemyB()
    {

    }
}
