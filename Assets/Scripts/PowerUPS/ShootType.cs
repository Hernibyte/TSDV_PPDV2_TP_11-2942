
public class ShootType : Item
{
    public enum TypeShoot
    {
        Simple,
        Burst,
        Cone,
        Beam
    }
    public TypeShoot typeShoot;
    public float damage;
    public float fireRate;
}
