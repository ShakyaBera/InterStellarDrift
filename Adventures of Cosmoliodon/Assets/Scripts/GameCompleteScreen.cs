using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompleteScreen : MonoBehaviour
{
    public float timeBetweenTexts;
    private bool canExit;
    public string mainMenuName = "MainMenuGame";
    public Text message, score, credit, pressKey;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showtextCo());
    }

    // Update is called once per frame
    void Update()
    {
        if(canExit && Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenuGame");
        }
    }
    public IEnumerator showtextCo()
    {
        yield return new WaitForSeconds(timeBetweenTexts);
        message.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        score.text = "Final Score: " + PlayerPrefs.GetInt("CurrentScore");
        score.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        credit.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        pressKey.gameObject.SetActive(true);
        canExit = true;

    }
}
