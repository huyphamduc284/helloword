using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Speed at which the turret rotates
    public GameObject bulletPrefab; // Prefab of the bullet
    //public float fireRate = 1f; // Number of bullets fired per second
    //private float fireCooldown = 0f; // Countdown to next bullet
    public static Turret t;

    private void Awake()
    {
        t = this;
    }

    private void Update()
    {
        if (TypingManager.tm.activeWord != null)
        {
            Vector3 direction = TypingManager.tm.activeWord.transform.position - transform.position;
            direction.z = 0;  // Ensure the direction is only in the x-y plane
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;  // Subtract 90 to align with y-axis
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


            //// If it's time to fire a bullet, fire a bullet
            //if (fireCooldown <= 0f)
            //{
            //    FireBullet();
            //    fireCooldown = 1f / fireRate;
            //}

            //fireCooldown -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        // Instantiate a new bullet
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        // If the bullet was successfully created, seek the target
        if (bullet != null)
        {
            bullet.Seek(TypingManager.tm.activeWord.transform);
        }
    }
}
