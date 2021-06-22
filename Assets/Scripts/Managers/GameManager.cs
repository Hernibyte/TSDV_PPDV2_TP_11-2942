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

    public void AddPoints(int _points){
        Player player = FindObjectOfType<Player>();
        if(player != null){
            gamePoints += _points * player.pointsMultiplerAmount;
        }
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

        if(actualEnemiesAmount <= 0)
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
