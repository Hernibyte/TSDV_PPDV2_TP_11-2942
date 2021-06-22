using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Player : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI points;
    [SerializeField] public Image lifeBar;
    private Player playerRef;
    void Start()
    {
        playerRef = FindObjectOfType<Player>();
        playerRef.updateDataUI += UpdateDataUI;
    }
    private void OnDisable()
    {
        playerRef.updateDataUI -= UpdateDataUI;
    }

    public void UpdateDataUI()
    {
        lifeBar.fillAmount = (playerRef.energy / playerRef.restoreEnergy);
        if(points != null)
            points.text = "Points:" + GameManager.Get()?.GetPoints().ToString();
    }
}
