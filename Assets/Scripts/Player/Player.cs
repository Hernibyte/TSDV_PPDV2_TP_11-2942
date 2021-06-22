using UnityEngine;

public class Player : Character, IHittable
{
    [SerializeField] public float energy = 100f;
    [SerializeField] public float damage = 10f;
    int DamageIncrementAmount = 1;
    [SerializeField] public int explosionAmount = 0;
    [SerializeField] LayerMask powerUp;
    [SerializeField] public PowerUp shootType = null;
    [SerializeField] PowerUp consumible = null;
    [SerializeField] PowerUp pointsMultipler = null;
    [HideInInspector] public int pointsMultiplerAmount = 1;
    float pointsMultiplerTimer = 0f;
    bool pointsMultiplerAppled = false;
    [HideInInspector] public float restoreEnergy;

    private bool restoringEnergy = false;
    [SerializeField] ParticleSystem particle;

    public delegate void UpdateUIPlayer();
    public UpdateUIPlayer updateDataUI;
    private void Start()
    {
        updateDataUI?.Invoke();
    }

    private void Awake()
    {
        restoreEnergy = energy;
        shootType = GameManager.Get()?.GetPowerUpPerID(7);
    }

    void Update(){
        if (GameManager.Get() != null)
        {
            if (!GameManager.Get().IsGamePaused())
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (shootType != null)
                        Shoot_Type(shootType.specificType, ShootLayer.Player, damage);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (explosionAmount > 0)
                    {
                        ExplosionAttack();
                        particle.Play();
                        explosionAmount--;
                    }
                }
                PointsMultiplerInFunction();
                CalculateFireRateShoot();
            }
        }
    }

    void LateUpdate()
    {
        if (GameManager.Get() != null)
        {
            if (!GameManager.Get().IsGamePaused())
            {
                Movement_Clamped(speed);
                RestoreEnergy();
            }
        }
    }

    public void TakeDamage(float damage){
        if(energy > 0)
            energy -= damage;

        updateDataUI?.Invoke();
        AudioManager.Get()?.Play("hit");


        if (energy <= 0)
        {
            energy = 0;
            If_Die();
        }
    }

    void If_Die(){
        GameManager.Get()?.GameOver(true);
    }

    void RestoreEnergy()
    {
        if(restoringEnergy)
        {
            if (energy < restoreEnergy)
            {
                energy += (energy*0.25f) * Time.deltaTime;
                updateDataUI?.Invoke();
            }
            else
            {
                energy = restoreEnergy;
                restoringEnergy = false;
            }
        }
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
                restoringEnergy = true;

            break;
            case SpecificPowerUp.IncreamentFireRate:

                if(fireRate > 0.2f)
                    fireRate -= 0.1f;

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

            AudioManager.Get()?.Play("power1");
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
            updateDataUI?.Invoke();
            Destroy(collision.gameObject);
        }
    }
    public bool Contains(LayerMask mask, int layer) {
        return mask == (mask | (1 << layer));
    }
}