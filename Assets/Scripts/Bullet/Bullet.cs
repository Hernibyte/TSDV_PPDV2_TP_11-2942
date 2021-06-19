using UnityEngine;

public enum ShootLayer{
    Player,
    Enemy
}
public enum Direction
{
    Up,
    Down
}
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [HideInInspector] public Direction myDirection;
    [HideInInspector] public ShootLayer shootLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask enemyLayer;
    [HideInInspector] public float localDamage = 20f;

    void Awake(){
        shootLayer = ShootLayer.Enemy;
    }

    void LateUpdate(){
        switch (myDirection)
        {
            case Direction.Up:
                transform.position += transform.up * speed;
                break;
            case Direction.Down:
                transform.position += -transform.up * speed;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        switch(shootLayer){
            case ShootLayer.Enemy:

                if(Contains(playerLayer, collider.gameObject.layer)){
                    IHittable hit = collider.GetComponent<IHittable>();
                    hit.TakeDamage(localDamage);
                    Destroy(this.gameObject);
                }

            break;
            case ShootLayer.Player:

                if (Contains(enemyLayer, collider.gameObject.layer)){
                    IHittable hit = collider.GetComponent<IHittable>();
                    hit.TakeDamage(localDamage);
                    Destroy(this.gameObject);
                }

            break;
        }
    }

    public bool Contains(LayerMask mask, int layer){
        return mask == (mask | (1 << layer));
    }

}
