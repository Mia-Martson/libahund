using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab here
    public GameObject laserPrefab;
    public GameObject laserIndicatorPrefab; // Reference your white line prefab

    public float indicatorDuration = 2f; // Time the indicator is shown before lasers appear


    public float shootInterval = 1f; // Time between each circle
    public int numberOfBulletsInStandardAttack = 20; // How many bullets in the circle
    public int numberOfBulletsInCircleAttack = 200;

    public float enragedShootInterval = 1f; // Time between each circle
    public int numberOfBulletsInEnragedStandardAttack = 20; // How many bullets in the circle
    public int numberOfBulletsInEnragedCircleAttack = 200;


    public float bulletSpeed; // Speed of the bullets
    public float waveOffsetAngle = 0f;
    public float waveAngleIncrement = 15f;

    public float laserRotationSpeed = 30f; // Degrees per second for the spinning lasers
    public List<GameObject> activeLasers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the lasers in an X pattern if they are active
        if (activeLasers.Count > 0)
        {
            foreach (GameObject laser in activeLasers)
            {
                laser.transform.RotateAround(transform.position, Vector3.forward, laserRotationSpeed * Time.deltaTime);
            }
        }
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

    public void LaserAttack()
    {
        Debug.Log("LASER ATTACK!!!");

        // Destroy existing lasers
        foreach (GameObject laser in activeLasers)
        {
            Destroy(laser);
        }
        activeLasers.Clear();

        // List to hold the indicators temporarily
        List<GameObject> indicators = new List<GameObject>();

        // Spawn indicators in an X pattern
        for (int i = 0; i < 4; i++)
        {
            float angle = (i * 90f); // 90 degrees apart for X pattern
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // Spawn the indicator
            GameObject indicator = Instantiate(laserIndicatorPrefab, transform.position, Quaternion.identity);
            indicator.transform.up = direction; // Align the indicator to the direction
            indicators.Add(indicator);
        }

        // Start the coroutine that replaces indicators with lasers after a delay
        StartCoroutine(ReplaceIndicatorsWithLasers(indicators));

    }

    public void EnragedStandardAttack()
    {
        //ründa mängijat

        float angleStep = 360f / numberOfBulletsInEnragedStandardAttack; // Angle between each bullet
        float angle = waveOffsetAngle;

        for (int i = 0; i < numberOfBulletsInEnragedStandardAttack; i++)
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

    public void EnragedCircularAttack()
    {
        Debug.Log("CIRCULAR ATTACK!!!!!!!!!");

        float angleStep = 360f / numberOfBulletsInEnragedCircleAttack; // Angle between each bullet
        float angle = 0f; // Starting angle

        for (int i = 0; i < numberOfBulletsInEnragedCircleAttack; i++)
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

    public IEnumerator SingularBulletCircleAttack(float fireRate, int numberOfBullets)
    {
        float angleStep = 360f / numberOfBullets; // Angle step between each bullet
        float angle = 0f; // Starting angle

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

            // Wait for the specified fire rate before shooting the next bullet
            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator ReplaceIndicatorsWithLasers(List<GameObject> indicators)
    {
        // Wait for the indicator duration
        yield return new WaitForSeconds(indicatorDuration);

        // Replace indicators with lasers
        foreach (GameObject indicator in indicators)
        {
            // Get the indicator's current rotation and position
            Vector3 position = indicator.transform.position;
            Quaternion rotation = indicator.transform.rotation;

            // Destroy the indicator
            Destroy(indicator);

            // Spawn the actual laser
            GameObject laser = Instantiate(laserPrefab, position, rotation);
            activeLasers.Add(laser);
        }
    }
}