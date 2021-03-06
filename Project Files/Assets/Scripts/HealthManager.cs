using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public static HealthManager instance;

    public int currentHealth;
    public int maxHealth;

    public GameObject deathEff;

    public float invincibilityLength = 3f;
    public float invinceCounter;
    public SpriteRenderer theSR;

    public int shieldPwr;
    public int shieldMaxPwr;
    public GameObject shield;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        shield.SetActive(false);
        currentHealth = maxHealth;
        UIManager.instance.healthbar.maxValue = maxHealth;
        UIManager.instance.healthbar.value = currentHealth;
        UIManager.instance.shieldBar.maxValue = shieldMaxPwr;
        UIManager.instance.shieldBar.value = shieldPwr;
    }

    // Update is called once per frame
    void Update()
    {
        if(invinceCounter > 0)
        {
            invinceCounter -= Time.deltaTime;
            if(invinceCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
                WaveManager.instance.canSpawnWaves = true;
            }
        }
    }
    public void HurtPl()
    {
        if (invinceCounter <= 0)
        {
            if (shield.activeInHierarchy)
            {
                shieldPwr--;
                if(shieldPwr <= 0)
                {
                    shield.SetActive(false);
                }
                UIManager.instance.shieldBar.value = shieldPwr;

            }
            else
            {
                currentHealth--;
                UIManager.instance.healthbar.value = currentHealth;

                if (currentHealth <= 0)
                {
                    Instantiate(deathEff, transform.position, transform.rotation);
                    gameObject.SetActive(false);

                    GameManager.instance.KillPlayer();
                    WaveManager.instance.canSpawnWaves = false;
                }

                PlayerController.instance.doubleshotactive = false;
            }
        }

    }

    public void respawn()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        UIManager.instance.healthbar.value = currentHealth;

        invinceCounter = invincibilityLength;
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);
    }

    public void activateShield()
    {
        shield.SetActive(true);
        shieldPwr = shieldMaxPwr;
        UIManager.instance.shieldBar.value = shieldPwr;

    }
}
