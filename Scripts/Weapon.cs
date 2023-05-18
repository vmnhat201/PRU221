using System.Collections;
using UnityEngine;

public enum WeaponStyle
{
    Pistol,
    FartGun,
    StrongGun,
    Bom,
}
public class Weapon : MonoBehaviour
{
    public WeaponStyle style;
    public float quantity;
    public GameObject bulletPrefab;
    public float bulletForce = 40f;
    public float ultimateBulletSpeed = 20f;
    public float missileSpeed = 10f;
    public float explosionRadius = 5f;

    public void SetUp()
    {
        switch (style)
        {
            case WeaponStyle.Pistol:
                quantity = 20;
                break;
            case WeaponStyle.FartGun:
                quantity = 20;
                break;
            case WeaponStyle.StrongGun:
                quantity = 20;
                break;
            case WeaponStyle.Bom:
                quantity = 20;
                break;
        }
    }

    public void Shoot()
    {
        switch (style)
        {
            case WeaponStyle.Pistol:
                ShootPistol();
                break;
            case WeaponStyle.FartGun:
                ShootFast();
                //UltimateSkillFast();
                break;
            case WeaponStyle.StrongGun:
                ShootStrong();
                //UltimateSkillStrong();
                break;
            case WeaponStyle.Bom:
                Bom();
                //UltimateSkillBom();
                break;
        }
    }

    public void UltiShoot()
    {
        switch (style)
        {
            case WeaponStyle.Pistol:
                ShootPistol();
                break;
            case WeaponStyle.FartGun:
                //ShootFast();
                UltimateSkillFast();
                break;
            case WeaponStyle.StrongGun:
                //ShootStrong();
                UltimateSkillStrong();
                break;
            case WeaponStyle.Bom:
                //Bom();
                UltimateSkillBom();
                break;
        }
    }


    public void ShootPistol()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }


    public void ShootFast()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }

    public void UltimateSkillFast()
    {
        StartCoroutine(FireBulletsInCone());
    }

    public void ShootStrong()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }

    public void UltimateSkillStrong()
    {
        GameObject missile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * missileSpeed, ForceMode2D.Impulse);
        // perform ultimate attack with high damage
        //StartCoroutine(Explode(missile));
    }


    public void Bom()
    {
        GameObject bombInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bombRigidbody = bombInstance.GetComponent<Rigidbody>();
        bombRigidbody.velocity = transform.forward * 10;
        Collider bombCollider = bombInstance.GetComponent<Collider>();
        Physics.IgnoreCollision(GetComponent<Collider>(), bombCollider);
        //Destroy(bombInstance, 5f);
    }

    public void UltimateSkillBom()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject bombInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody bombRigidbody = bombInstance.GetComponent<Rigidbody>();
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0;
            bombRigidbody.velocity = randomDirection.normalized * 10;
            Collider bombCollider = bombInstance.GetComponent<Collider>();
            Physics.IgnoreCollision(GetComponent<Collider>(), bombCollider);
            //Destroy(bombInstance, 3f);
        }
    }

    private IEnumerator Explode(GameObject missile)
    {
        yield return new WaitForSeconds(3f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(missile.transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            // apply damage to colliders within the explosion radius
        }
        // create explosion effect
        Destroy(missile);
    }

    private IEnumerator FireBulletsInCone()
    {
        float halfConeAngle = (6 - 1) * 6f / 2f;
        Vector2 direction = transform.right;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                float angle = j * 6f - halfConeAngle;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector2 rotatedDirection = rotation * direction;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = rotatedDirection * 25;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
<<<<<<< Updated upstream
    public void OnTriggerEnter2D(Collision2D collision)
=======
    public void OnTriggerEnter2D(Collider2D collision)
>>>>>>> Stashed changes
    {
        
        Player p = collision.gameObject.GetComponent<Player>();

        if (p != null)
        {
            Debug.Log("player");
            p.ChangeWeapon(this);
<<<<<<< Updated upstream
            Destroy(gameObject);
        }
=======
            Destroy(this.gameObject);
        }
       
       
>>>>>>> Stashed changes
    }
}