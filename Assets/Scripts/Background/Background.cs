using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float speed;

    void LateUpdate(){
        if (GameManager.Get() != null)
        {
            if (!GameManager.Get().IsGamePaused())
            {
                transform.localPosition += new Vector3(0f, -speed, 0f);

                if (transform.localPosition.y <= (0f - Screen.height))
                    transform.localPosition = Vector3.zero;
            }
        }
    }
}
