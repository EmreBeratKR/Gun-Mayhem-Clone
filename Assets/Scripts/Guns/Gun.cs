using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private gun_type type;
    [SerializeField] private float knockback;
    [SerializeField] private float fire_rate;
    [SerializeField] private int ammo_capacity;
    [SerializeField] private float weight;
    [SerializeField] private float recoil;
    [SerializeField] private float reload_time;
    private int current_ammo;
    private float last_shot;
    private bool canReload;
    private bool isFiring;
    private enum gun_type
    {
        SMG,
        SNIPER,
        SHOTGUN,
        LMG,
        PISTOL,
        SPECIAL
    }


    private void Start()
    {
        current_ammo = ammo_capacity;
        last_shot = 0f;
        canReload = true; // just for test (true)
    }

    public void fire(bool triggerRelease = false)
    {
        if (current_ammo > 0)
        {
            isFiring = true;
            if (Time.time - last_shot >= (1f / fire_rate))
            {
                Transform player = transform.parent.parent.parent;
                // fires a bullet
                GameObject bullet = Instantiate(bulletPrefab, transform.position, player.rotation);
                bullet.GetComponent<Bullet>().set_bullet(knockback);
                last_shot = Time.time;
                Destroy(bullet, 2f);
                current_ammo--;
                // tries to reload gun
                if (current_ammo == 0)
                {
                    if (canReload)
                    {
                        StartCoroutine(reload());
                    }
                    else
                    {
                        // drop the gun
                    }
                }
                // gun recoil
                player.GetComponent<Rigidbody2D>().velocity += (player.rotation.y == 0 ? 1 : -1) * Vector2.right * recoil;
            }
        }
        else
        {
            isFiring = false;
        }
    }

    private IEnumerator reload()
    {
        yield return new WaitForSeconds(reload_time);
        current_ammo = ammo_capacity;
    }

    public void set_firing(bool state)
    {
        isFiring = state;
    }

    public bool get_firing()
    {
        return isFiring;
    }

    public float get_weight()
    {
        return weight;
    }
}
