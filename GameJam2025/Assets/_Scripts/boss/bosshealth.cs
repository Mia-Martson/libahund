using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosshealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

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

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(currentHealth);

        if (currentHealth < 0)
        {
            //kurat saab surma
            Debug.Log("kurat sai surma");
            Destroy(gameObject);
        }
    }
}
