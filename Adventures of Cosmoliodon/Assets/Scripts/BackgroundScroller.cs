using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform BG1, BG2;
    public float scrollSpeed;

    private float bgwidth;
    // Start is called before the first frame update
    void Start()
    {
        bgwidth = BG1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

    }

    // Update is called once per frame
    void Update()
    {
        BG1.position = new Vector3(BG1.position.x - (scrollSpeed * Time.deltaTime), BG1.position.y, BG1.position.z);
        BG2.position -= new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f);

        if (BG1.position.x < -bgwidth - 1) 
        {
            BG1.position += new Vector3(bgwidth * 2f, 0f, 0f);
        }
        if (BG2.position.x < -bgwidth - 1)
        {
            BG2.position += new Vector3(bgwidth * 2f, 0f, 0f);
        }
    }
}
