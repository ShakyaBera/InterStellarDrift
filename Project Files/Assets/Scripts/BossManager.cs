using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public string bossName;
    public int currentHealthBoss = 100;

    //public BattleShot[] shotsToFire;
    public BattlePhase[] phases;
    public int currentPhase;
    public Animator bossAnim;

    public GameObject endExplosion;
    public bool battleEnding;
    public float timeToExplosionEnd, waitToEndLevel;

    public Transform bossPos;

    public int scoreValue = 5000;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.bossName.text = bossName;
        UIManager.instance.bHSlider.maxValue = currentHealthBoss;
        UIManager.instance.bHSlider.value = currentHealthBoss;
        UIManager.instance.bHSlider.gameObject.SetActive(true);

        MusicController.instance.PlayBoss();
    }

    // Update is called once per frame
    void Update()
    {
        /* for(int i = 0; i < shotsToFire.Length; i++)
         {
             shotsToFire[i].shotCounter -= Time.deltaTime;

             if(shotsToFire[i].shotCounter <= 0)
             {
                 shotsToFire[i].shotCounter = shotsToFire[i].timeBetwenShots;
                 Instantiate(shotsToFire[i].theShot, shotsToFire[i].firePoint.position, shotsToFire[i].firePoint.rotation);
             }
         }*/
        if (!battleEnding)
        {
            if (currentHealthBoss <= phases[currentPhase].healthToEndPhs)
            {
                phases[currentPhase].removeAtPhaseEnd.SetActive(false);
                Instantiate(phases[currentPhase].addAtPhaseEnd, phases[currentPhase].newAbilityPoint.position, phases[currentPhase].newAbilityPoint.rotation);


                bossAnim.SetInteger("Phase", 2);
            }
            else
            {
                for (int i = 0; i < phases[currentPhase].phaseShots.Length; i++)
                {
                    phases[currentPhase].phaseShots[i].shotCounter -= Time.deltaTime;

                    if (phases[currentPhase].phaseShots[i].shotCounter <= 0)
                    {
                        phases[currentPhase].phaseShots[i].shotCounter = phases[currentPhase].phaseShots[i].timeBetwenShots;
                        Instantiate(phases[currentPhase].phaseShots[i].theShot, phases[currentPhase].phaseShots[i].firePoint.position, phases[currentPhase].phaseShots[i].firePoint.rotation);
                    }
                }
            }
        }
    }

    public void HurtBoss()
    {
        currentHealthBoss--;
        UIManager.instance.bHSlider.value = currentHealthBoss;
        if (currentHealthBoss == 0 && !battleEnding)
        {
            /*UIManager.instance.bHSlider.gameObject.SetActive(false);
            Destroy(gameObject);*/
            battleEnding = true;
            StartCoroutine(EndBattleCo());
        }
    }

    public IEnumerator EndBattleCo()
    {
        UIManager.instance.bHSlider.gameObject.SetActive(false);
        Instantiate(endExplosion, bossPos.position, bossPos.rotation);
        bossAnim.enabled = false;
        GameManager.instance.AddScore(scoreValue);
        yield return new WaitForSeconds(timeToExplosionEnd);

        bossPos.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToEndLevel);

        StartCoroutine(GameManager.instance.EndOLevelCo());

    }
}

[System.Serializable]
public class BattleShot
{
    public GameObject theShot;
    public float timeBetwenShots;
    public float shotCounter;
    public Transform firePoint;
}

[System.Serializable]
public class BattlePhase
{
    public BattleShot[] phaseShots;
    public int healthToEndPhs;
    public GameObject removeAtPhaseEnd;
    public GameObject addAtPhaseEnd;
    public Transform newAbilityPoint;

}
