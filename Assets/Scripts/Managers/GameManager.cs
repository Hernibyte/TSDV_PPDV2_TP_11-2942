using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] public int amountEnemiesLevel_1;

    public void AddPoints(int _points){
        Player player = FindObjectOfType<Player>();
        if(player != null){
            gamePoints += _points * player.pointsMultiplerAmount;
        }
    }

    public void DecreaseEnemyCount()
    {
        if(amountEnemiesLevel_1 > 0)
            amountEnemiesLevel_1--;
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
