using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab here
    public float shootInterval = 1f; // Time between each circle
    public int numberOfBullets = 20; // How many bullets in the circle
    public float bulletSpeed; // Speed of the bullets
    public float waveOffsetAngle = 0f;
    public float waveAngleIncrement = 15f;

    // Start is called before the first frame update
    void Start()
    {
     //   InvokeRepeating(nameof(Attack), 0f, shootInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        //ründa mängijat
        
            float angleStep = 360f / numberOfBullets; // Angle between each bullet
            float angle = waveOffsetAngle;

        for (int i = 0; i < numberOfBullets; i++)
            {
                // Calculate the direction of the bullet
                float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY);

                // Spawn the bullet
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetDirection(bulletDirection);
                bulletScript.speed = bulletSpeed;

                // Increment the angle for the next bullet
                angle += angleStep;

            waveOffsetAngle += waveAngleIncrement;

            // Ensure the angle stays within 0-360 degrees
            if (waveOffsetAngle >= 360f)
            {
                waveOffsetAngle -= 360f;
            }

        }
    }
}
