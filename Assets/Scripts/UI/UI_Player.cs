using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Player : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI points;
    [SerializeField] public TextMeshProUGUI typeShoot;
    [SerializeField] public TextMeshProUGUI amountNukes;
    [SerializeField] public GameObject panelPause;
    [SerializeField] public Image lifeBar;

    private Player playerRef;
    void Start()
    {
        playerRef = FindObjectOfType<Player>();
        playerRef.updateDataUI += UpdateDataUI;
        panelPause.SetActive(false);
    }
    private void Update()
    {
        if(GameManager.Get() != null)
        {
            if(GameManager.Get().IsGamePaused())
                panelPause.SetActive(true);
            else
                panelPause.SetActive(false);
        }
    }
    private void OnDisable()
    {
        playerRef.updateDataUI -= UpdateDataUI;
    }

    public void Pause_ResumeFromUI()
    {
        GameManager.Get()?.PauseGame();
    }

    public void UpdateDataUI()
    {
        lifeBar.fillAmount = (playerRef.energy / playerRef.restoreEnergy);
        if (points != null)
            points.text = "Points:" + GameManager.Get()?.GetPoints().ToString();

        if (playerRef.shootType != null)
        {
            switch (playerRef.shootType.specificType)
            {
                case SpecificPowerUp.SimpleShoot:
                    typeShoot.text = "Simple Fire";
                    break;
                case SpecificPowerUp.BurstShoot:
                    typeShoot.text = "Burst Fire";
                    break;
                case SpecificPowerUp.ConeShoot:
                    typeShoot.text = "Radial Fire";
                    break;
            }
        }
        amountNukes.text = "Nukes " + playerRef.explosionAmount.ToString();
    }
}
