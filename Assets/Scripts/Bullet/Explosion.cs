using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speed = 2f;
    [SerializeField] ScreenShake cameraShake;
    private float timer =1;
    void Start()
    {
        animator.SetTrigger("Explode");
        cameraShake = Camera.main.GetComponent<ScreenShake>();
        StartCoroutine(cameraShake?.CameraShake(.3f, 12f));
        Destroy(gameObject, timer);
    }

    private void Update()
    {
        transform.position += -transform.up * speed;
    }
}
