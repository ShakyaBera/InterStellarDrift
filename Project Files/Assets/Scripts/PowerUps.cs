using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool isShield;
    public bool isBoost;
    public bool isDoubleShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collidents)
    {
        if(collidents.tag == "Player")
        {
            Destroy(gameObject);

            if (isShield)
            {
                HealthManager.instance.activateShield();
            }
            if(isBoost)
            {
                PlayerController.instance.ActivateSpdBoost();
            }
            if (isDoubleShot)
            {
                PlayerController.instance.doubleshotactive = true;
            }
        }

    }
}
