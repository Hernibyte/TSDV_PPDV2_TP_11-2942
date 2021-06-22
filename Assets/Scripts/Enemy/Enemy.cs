using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    [SerializeField] float energy = 100f;
    [SerializeField][Range(0,100)] float spawnPowerUpProbability;
    private float probValue;

    public void TakeDamage(float damage){
        energy -= damage;
        If_Die();
    }

    void If_Die(){
        if(energy <= 0f)
        {
            SpawnPowerUp();
            GameManager.Get()?.AddPoints(100);
            GameManager.Get()?.DecreaseEnemyCount();
            Destroy(gameObject);
        }
    }

    void SpawnPowerUp()
    {
        probValue = Random.Range(0, 100);

        if(probValue <= spawnPowerUpProbability)
        {
            Instantiate(GameManager.Get()?.SpawnRandomPowerUp(), transform.position, Quaternion.identity);
        }
    }
}
