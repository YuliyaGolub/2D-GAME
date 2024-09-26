using UnityEngine;

public interface IWeapon
{
    bool IsShooting { get; set ; }
    void Shoot();
}

public class Weapon : MonoBehaviour, IWeapon
{
    public bool IsShooting { get; set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private float timeOffset = 1f;

    [SerializeField] private float gunBarrelOfsfsetX = 1f;
    private Transform gunBarrelTransform;
    // Start is called before the first frame update
    void Start()
    {
        gunBarrelTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShooting && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + timeOffset / fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Player.Instance.AmmoCount > 0 && GameManager.Instance.CurrentGameEvent == GameEvent.Running)
        {
            Player.Instance.AmmoCount--;

            bool isFacingRight = Player.Instance.IsFacingRight;
            Vector2 bulletOffset = CalculateBarrelOffset(isFacingRight);
            gunBarrelTransform.localPosition = bulletOffset;

            GameObject bullet = Instantiate(bulletPrefab, gunBarrelTransform.position, gunBarrelTransform.rotation);
        }
        else
        {
            IsShooting = false;
        }
    }

    private Vector2 CalculateBarrelOffset(bool isFacingRight)
    {
        return isFacingRight ? new Vector2(gunBarrelOfsfsetX, gunBarrelTransform.localPosition.y) 
                             : new Vector2(-gunBarrelOfsfsetX, gunBarrelTransform.localPosition.y);
    }
}
