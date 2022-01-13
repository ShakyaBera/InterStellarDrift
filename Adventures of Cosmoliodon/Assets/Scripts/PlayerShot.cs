using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float shotSpeed = 7f;
    public GameObject impactEff;
    public GameObject explObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(shotSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collidents)
    {
        Instantiate(impactEff, transform.position, transform.rotation); 
        if(collidents.tag == "SpcObj")
        {
            Instantiate(explObj, collidents.transform.position, collidents.transform.rotation);
            Destroy(collidents.gameObject);

            GameManager.instance.AddScore(50);
        }
        if(collidents.tag == "Enemy")
        {
            collidents.GetComponent<EnemyController>().HurtEnemy();
        }
        if(collidents.tag == "Boss")
        {
            BossManager.instance.HurtBoss();
        }
        Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
