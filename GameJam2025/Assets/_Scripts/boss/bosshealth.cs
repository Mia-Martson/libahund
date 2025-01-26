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

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    private float flashDuration = 0.1f;

    public AudioClip hurtSound;
    public AudioClip failedHurtSound;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        bossAnimator = GetComponent<Animator>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
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
            SoundManager.Instance.PlaySFX(failedHurtSound);
            return;
        }
        currentHealth -= damage;
        SoundManager.Instance.PlaySFX(hurtSound);
        StartCoroutine(FlashWhite());

        Debug.Log(currentHealth);

        if (currentHealth < (maxHealth/2))
        {
            bossAnimator.SetTrigger("Enraged");
            bossAnimator.ResetTrigger("CircularAttack");
            bossAnimator.ResetTrigger("StandardAttack");
            bossAnimator.ResetTrigger("SingularBulletAttack");
        }

        if(currentHealth < 0)
        {
            FindObjectOfType<WinLoseManager>().ShowWinScreen();
            Debug.Log("Kurat sai surma");
        }
    }

    private IEnumerator FlashWhite()
    {
        // Turn the sprite white
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = new Color(0.898039215f, 0.8549019607843137f, 0.854901960f, 1f);

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
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
