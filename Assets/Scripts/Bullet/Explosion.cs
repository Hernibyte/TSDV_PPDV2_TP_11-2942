using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Animator animator;
    private float timer =1;
    void Start()
    {
        animator.SetTrigger("Explode");
        Destroy(gameObject, timer);
    }
}
