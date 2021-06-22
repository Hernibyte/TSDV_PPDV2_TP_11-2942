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
    [SerializeField] public Explosion explosionPref;
    [HideInInspector] public float localDamage = 20f;

    private Vector3 scaleExplosion;

    void Awake(){
        shootLayer = ShootLayer.Enemy;
        scaleExplosion = new Vector3(150,150,1);
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


        switch (shootLayer)
        {
            case ShootLayer.Enemy:

                if(Contains(playerLayer, collider.gameObject.layer)){
                    DamageCharacter(collider);
                }

                break;
            case ShootLayer.Player:

                if (Contains(enemyLayer, collider.gameObject.layer)){
                    DamageCharacter(collider);
                }

            break;
        }
    }

    public void DamageCharacter(Collider2D collider)
    {
        IHittable hit = collider.GetComponent<IHittable>();
        hit.TakeDamage(localDamage);
        if (explosionPref != null)
        {
            Explosion go = Instantiate(explosionPref, collider.transform.position, Quaternion.identity);
            go.transform.localScale = scaleExplosion;
        }
        Destroy(this.gameObject);
    }

    public bool Contains(LayerMask mask, int layer){
        return mask == (mask | (1 << layer));
    }

}
