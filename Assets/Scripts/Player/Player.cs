using UnityEngine;

public class Player : Character, IHittable
{
    [SerializeField] float energy = 100f;
    [SerializeField] float damage = 10f;
    int DamageIncrementAmount = 1;
    [SerializeField] int explosionAmount = 0;
    [SerializeField] LayerMask powerUp;
    [SerializeField] PowerUp shootType = null;
    [SerializeField] PowerUp consumible = null;
    [SerializeField] PowerUp pointsMultipler = null;
    [HideInInspector] public int pointsMultiplerAmount = 1;
    float pointsMultiplerTimer = 0f;
    bool pointsMultiplerAppled = false;

    private void Start()
    {
        shootType = GameManager.Get()?.GetPowerUpPerID(8);
    }

    void Update(){
        if (Input.GetKey(KeyCode.E))
        {
            if(shootType != null)
                Shoot_Type(shootType.specificType, ShootLayer.Player, damage);
        }
        PointsMultiplerInFunction();
        CalculateFireRateShoot();
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

    void ApplyConsumible(){
        switch(consumible.specificType){
            case SpecificPowerUp.Explosion:

                explosionAmount++;

            break;
            case SpecificPowerUp.IncrementDamage:

                if(DamageIncrementAmount > 0)
                    damage += 15f;
                DamageIncrementAmount--;

            break;
            case SpecificPowerUp.RestoreEnergy:

                energy = 100f;

            break;
        }
        consumible = null;
    }

    void ApplyPointsMultipler(){
        pointsMultiplerAppled = true;

        switch(pointsMultipler.specificType){
            case SpecificPowerUp.Points_X2:

                pointsMultiplerAmount = 2;

            break;
            case SpecificPowerUp.Points_X3:

                pointsMultiplerAmount = 3;

            break;
            case SpecificPowerUp.Points_X4:

                pointsMultiplerAmount = 4;

            break;
            case SpecificPowerUp.Points_X10:

                pointsMultiplerAmount = 10;

            break;
        }
    }

    void PointsMultiplerInFunction(){
        if(pointsMultiplerAppled){
            pointsMultiplerTimer += Time.deltaTime;

            if(pointsMultiplerTimer > 10f){
                pointsMultiplerAppled = false;
                pointsMultiplerTimer = 0f;
                pointsMultiplerAmount = 1;
                pointsMultipler = null;
            }
        }
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
                    ApplyConsumible();

                    break;
                case TypePowerUP.PointsMultiplers:

                    pointsMultipler = GameManager.Get()?.GetPowerUpPerID(powerUpIn.ID);
                    ApplyPointsMultipler();

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