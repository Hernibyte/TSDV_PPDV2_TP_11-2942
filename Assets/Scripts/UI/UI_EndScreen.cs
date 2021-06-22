using UnityEngine;
using TMPro;

public class UI_EndScreen : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI conditionState;
    [SerializeField] public TextMeshProUGUI pointsAllMatch;
    [SerializeField] public TextMeshProUGUI enemiesDestroyed;
    [SerializeField] public TextMeshProUGUI powerUpsTaken;
    [SerializeField] public TextMeshProUGUI bulletsShooted;
    void Start()
    {
        if(GameManager.Get() != null)
        {
            switch (GameManager.Get().GetPlayerState()) 
            {
                case EndState.Win:
                    conditionState.text = "WIN";
                    AudioManager.Get()?.Play("win");
                    break;
                case EndState.Lose:
                    conditionState.text = "LOSE";
                    AudioManager.Get()?.Play("lose");
                    break;
            }
        }
        pointsAllMatch.text = "Points: " + GameManager.Get()?.GetPoints();
        enemiesDestroyed.text = "Enemies Destroyed: " + GameManager.Get()?.GetEnemiesDestroyed();
        powerUpsTaken.text = "Power UP´s Taken: " + GameManager.Get()?.GetAmountPowerUPSTaked();
        bulletsShooted.text = "Bullets Shot: " + GameManager.Get()?.GetBulletShooted();
    }
}
