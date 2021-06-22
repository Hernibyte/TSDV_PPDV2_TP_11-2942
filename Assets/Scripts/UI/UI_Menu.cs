using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    void Start()
    {
        if(GameManager.Get() != null)
        {
            if (!GameManager.Get().GetIfSongMenuAlreadyStart())
            {
                AudioManager.Get()?.Play("menu");
                GameManager.Get().SongMenuStarted();
            }
        }
    }
}
