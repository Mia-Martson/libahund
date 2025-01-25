using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosshealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    private Animator bossAnimator;

    public GameObject deathEffect; // Optional particle effect or animation for death


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(currentHealth);

        if (currentHealth < 25)
        {
            bossAnimator.SetTrigger("Enraged");
            bossAnimator.ResetTrigger("CircularAttack");
            bossAnimator.ResetTrigger("StandardAttack");
        }

        if(currentHealth > 0)
        {
            Debug.Log("Kurat sai surma");

        }
    }
}
