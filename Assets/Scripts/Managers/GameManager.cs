using System.Collections.Generic;
using UnityEngine;

public enum EndState
{
    Win, 
    Lose 
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Get()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("POWER UPS LIST")]
    [SerializeField] public List<PowerUp> allPowerUps;
    [Header("PLAYER SCORE")]
    [SerializeField] int gamePoints = 0;
    [Header("ENEMIES IN LEVEL")]
    [SerializeField] public int amountEnemiesPerLevel;
    [SerializeField] public int actualEnemiesAmount;
    [SerializeField] public EndState playerStateAtEnd;
    [SerializeField] private int enemiesDestroyed;
    [SerializeField] private int powerUpsTaked;
    [SerializeField] private int bulletsFired;


    public void BulletShooted()
    {
        bulletsFired++;
    }
    public int GetBulletShooted()
    {
        return bulletsFired;
    }
    public void IncreasePowerUPSTaked()
    {
        powerUpsTaked++;
    }
    public int GetAmountPowerUPSTaked()
    {
        return powerUpsTaked;
    }
    public void AddPoints(int _points){
        Player player = FindObjectOfType<Player>();
        if(player != null){
            gamePoints += _points * player.pointsMultiplerAmount;
            player.updateDataUI?.Invoke();
        }
    }
    public int GetEnemiesDestroyed()
    {
        return enemiesDestroyed;
    }
    public EndState GetPlayerState()
    {
        return playerStateAtEnd;
    }
    public int GetPoints()
    {
        return gamePoints;
    }
    public void SetActualAmountOfEnemies(int enemies)
    {
        actualEnemiesAmount += enemies;
    }

    public void GameOver(bool isDead)
    {
        if (isDead)
            playerStateAtEnd = EndState.Lose;
        else
            playerStateAtEnd = EndState.Win;

        SceneLoader.Get()?.LoadSceneByName("EndScreen");
    }

    public void DecreaseEnemyCount()
    {
        if (actualEnemiesAmount > 0)
            actualEnemiesAmount--;

        enemiesDestroyed++;

        if (actualEnemiesAmount <= 0)
            GameOver(false);
    }

    public PowerUp GetPowerUpPerID(int ID)
    {
        for (int i = 0; i < allPowerUps.Count; i++)
        {
            if(allPowerUps[i] != null)
            {
                if(allPowerUps[i].ID == ID)
                {
                    return allPowerUps[i];
                }
            }
        }
        return null;
    }

    public PowerUp SpawnRandomPowerUp()
    {
        int randPowerUp = Random.Range(0, allPowerUps.Count);

        if (allPowerUps[randPowerUp] != null)
            return allPowerUps[randPowerUp];

        return null;
    }
}
