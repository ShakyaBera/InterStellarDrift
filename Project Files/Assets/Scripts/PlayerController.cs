using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public Rigidbody2D theRB;

    public Transform bottomLeftLimit, topRightLimit;

    public Transform shot1, shot2;
    public GameObject shot;

    public float tBS = 0.1f; //Time Between Shots
    private float sC; //Shot Counter

    private float normalSpd;
    public float boostSpd;
    public float boostPeriod;
    private float boostContr;

    public bool doubleshotactive;
    public float doubleshotoffset;

    public bool stopMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        normalSpd = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement)
        {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.position.x, topRightLimit.position.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.position.y, topRightLimit.position.y), transform.position.z);

            if (Input.GetButtonDown("Fire1"))
            {
                if (!doubleshotactive)
                {
                    Instantiate(shot, shot1.position, shot1.rotation);
                    Instantiate(shot, shot2.position, shot2.rotation);

                }
                else
                {
                    Instantiate(shot, shot1.position + new Vector3(0f, doubleshotoffset, 0f), shot1.rotation);
                    Instantiate(shot, shot1.position - new Vector3(0f, doubleshotoffset, 0f), shot2.rotation);
                    Instantiate(shot, shot2.position + new Vector3(0f, doubleshotoffset, 0f), shot2.rotation);
                    Instantiate(shot, shot2.position - new Vector3(0f, doubleshotoffset, 0f), shot2.rotation);
                }

                sC = tBS;
            }

            if (Input.GetButton("Fire1"))
            {
                sC -= Time.deltaTime;
                if (sC <= 0)
                {
                    if (!doubleshotactive)
                    {
                        Instantiate(shot, shot1.position, shot1.rotation);
                        Instantiate(shot, shot2.position, shot2.rotation);

                    }
                    else
                    {
                        Instantiate(shot, shot1.position + new Vector3(0f, doubleshotoffset, 0f), shot1.rotation);
                        Instantiate(shot, shot1.position - new Vector3(0f, doubleshotoffset, 0f), shot2.rotation);
                        Instantiate(shot, shot2.position + new Vector3(0f, doubleshotoffset, 0f), shot2.rotation);
                        Instantiate(shot, shot2.position - new Vector3(0f, doubleshotoffset, 0f), shot2.rotation);
                    }
                    sC = tBS;
                }
            }

            if (boostContr > 0)
            {
                boostContr -= Time.deltaTime;
                if (boostContr <= 0)
                {
                    moveSpeed = normalSpd;
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
    }

    public void ActivateSpdBoost()
    {
        boostContr = boostPeriod;
        moveSpeed = boostSpd;
    }

}
