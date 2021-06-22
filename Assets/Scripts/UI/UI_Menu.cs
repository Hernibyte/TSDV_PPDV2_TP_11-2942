using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    void Start()
    {
        AudioManager.Get()?.Play("menu");
    }
}
