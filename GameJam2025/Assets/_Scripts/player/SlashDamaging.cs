using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDamaging : MonoBehaviour
{

    [SerializeField] public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kurat"))
        {
            Debug.Log("Kurat hit with da melee attack!");
            bosshealth bossHealth = collision.GetComponent<bosshealth>();
            //kurat take damage
            bossHealth.takeDamage(damage);
        }
    }
}
