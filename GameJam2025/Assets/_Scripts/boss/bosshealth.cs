using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosshealth : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;

    public GameObject deathEffect; // Optional particle effect or animation for death


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
     //   currentHealth -= damage;
    }
}
