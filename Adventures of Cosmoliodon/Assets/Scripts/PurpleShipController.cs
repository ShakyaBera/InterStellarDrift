using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleShipController : MonoBehaviour
{
    public float moveSpeed;

    public Vector2 startDir;

    public bool shouldChngeDir;
    public float changeDirPoint;
    public Vector2 changeDirPos;

    public GameObject shotFire;
    public Transform firePos1;
    public float timeInshots;
    private float shotCounter;

    public bool canShoot;
    private bool allowShooting;

    public int currentHealth;
    public GameObject destrEff;
    public GameObject deathParts;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = timeInshots;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        if (!shouldChngeDir)
        {
            transform.position += new Vector3(startDir.x * moveSpeed * Time.deltaTime, startDir.y * moveSpeed * Time.deltaTime, 0f);
        }
        else
        {
            if (transform.position.x > changeDirPoint)
            {
                transform.position += new Vector3(startDir.x * moveSpeed * Time.deltaTime, startDir.y * moveSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.position += new Vector3(changeDirPos.x * moveSpeed * Time.deltaTime, changeDirPos.y * moveSpeed * Time.deltaTime, 0f);
            }
        }



        if (allowShooting)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeInshots;
                Instantiate(shotFire, firePos1.position, firePos1.rotation);
            }
        }

    }

    public void HurtEnemy()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(destrEff, transform.position, transform.rotation);
            Instantiate(deathParts, transform.position, transform.rotation);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnBecameVisible()
    {
        if (canShoot)
        {
            allowShooting = true;
        }
    }
}