using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth = 5;


    public GameObject bulletPrefab; // Assign your bullet prefab here
    public float shootInterval = 1f; // Time between each circle
    public int numberOfBulletsInStandardAttack = 20; // How many bullets in the circle
    public int numberOfBulletsInCircleAttack = 200;
    public float bulletSpeed; // Speed of the bullets
    public float waveOffsetAngle = 0f;
    public float waveAngleIncrement = 15f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StandardAttack()
    {
        //ründa mängijat
        
            float angleStep = 360f / numberOfBulletsInStandardAttack; // Angle between each bullet
            float angle = waveOffsetAngle;

        for (int i = 0; i < numberOfBulletsInStandardAttack; i++)
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

    public void CircularAttack()
    {
        Debug.Log("CIRCULAR ATTACK!!!!!!!!!");
        
        float angleStep = 360f / numberOfBulletsInCircleAttack; // Angle between each bullet
        float angle = 0f; // Starting angle

        for (int i = 0; i < numberOfBulletsInCircleAttack; i++)
        {
            // Calculate the direction of the bullet
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY);

            // Spawn the bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(bulletDirection); // Set the direction of the bullet
            bulletScript.speed = bulletSpeed;

            // Increment the angle for the next bullet
            angle += angleStep;
        }
    }
    
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
