using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosshealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    private Animator bossAnimator;

    public List<string> DamageableStates;

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
        // Check if the boss is in a damageable state
        if (!IsDamageableState()) //if the boss is NOT in the damagable state
        {
            Debug.Log("Boss is invulnerable in the current state!");
            return;
        }

        currentHealth -= damage;
        
        Debug.Log(currentHealth);

        if (currentHealth < 25)
        {
            bossAnimator.SetTrigger("Enraged");
            bossAnimator.ResetTrigger("CircularAttack");
            bossAnimator.ResetTrigger("StandardAttack");
        }

        if(currentHealth < 0)
        {
            Debug.Log("Kurat sai surma");

        }
    }

    private bool IsDamageableState()
    {
        // Get the current state info from the Animator
        AnimatorStateInfo currentState = bossAnimator.GetCurrentAnimatorStateInfo(0); // Assuming layer 0

        // Check if the current state name is in the damageableStates list
        foreach (string stateName in DamageableStates)
        {
            if (currentState.IsName(stateName))
            {
                return true;
            }
        }

        return false;
    }
}
