using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject gameOverScreen;
    public Text livesText;

    public Slider healthbar, shieldBar;
    public Text ScoreText, hiScoreText;

    public GameObject levelEndScrn, pauseScrn;

    public Text endLevelScore, endCurrentScore;
    public GameObject highscoreNotice;

    public Slider bHSlider;
    public Text bossName;

    private void Awake()
    {
        instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenuGame");
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }
}
