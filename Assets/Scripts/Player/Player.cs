using UnityEngine;

public class Player : Character, IHittable
{
    [SerializeField] float energy = 100f;
    [SerializeField] float damage;
    [SerializeField] LayerMask powerUp;
    [SerializeField] PowerUp shootType = null;
    [SerializeField] PowerUp consumible = null;
    [SerializeField] PowerUp pointsMultipler = null;

    private void Start()
    {
        shootType = GameManager.Get()?.GetPowerUpPerID(7);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(shootType != null)
                Shoot_Type(shootType.specificType, ShootLayer.Player, damage);
        }
    }

    void LateUpdate()
    {
        Movement_Clamped(speed);
    }

    public void TakeDamage(float damage){
        energy -= damage;
        If_Die();
    }

    void If_Die(){
        if(energy <= 0f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Contains(powerUp, collision.gameObject.layer))
        {
            PowerUp powerUpIn = collision.gameObject.GetComponent<PowerUp>();

            if (powerUpIn == null)
                return;

            switch (powerUpIn.typePowerUP)
            {
                case TypePowerUP.Consumible:
                    consumible = GameManager.Get()?.GetPowerUpPerID(powerUpIn.ID);
                    break;
                case TypePowerUP.PointsMultiplers:
                    pointsMultipler = GameManager.Get()?.GetPowerUpPerID(powerUpIn.ID);
                    break;
                case TypePowerUP.ShootType:
                    shootType = GameManager.Get()?.GetPowerUpPerID(powerUpIn.ID);
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
    public bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

}