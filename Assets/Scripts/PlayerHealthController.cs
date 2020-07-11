using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invicibleLength;
    private float invicibleCounter;

    private SpriteRenderer theSR;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime;

            if(invicibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.r, theSR.color.r, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if(invicibleCounter <= 0)
        {
            // currentHealth -= 1;
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                // gameObject.SetActive(false);

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invicibleCounter = invicibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.r, theSR.color.r, 0.5f);

                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(9);
            }

            UIController.instance.updateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.updateHealthDisplay();
    }
}
