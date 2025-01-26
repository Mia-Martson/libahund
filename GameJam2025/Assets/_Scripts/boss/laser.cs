using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("inimenesaipihta");
            // Destroy the bullet
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);

        }
    }
}
