using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnds : MonoBehaviour
{
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
            StartCoroutine(GameManager.instance.EndOLevelCo());
        }
    }
}
