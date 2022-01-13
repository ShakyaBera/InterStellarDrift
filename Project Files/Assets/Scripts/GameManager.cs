using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLives = 3;
    public float RespawnGracePeriod = 2f;

    public int currentScore;
    private int hiScore = 500;

    public bool levelEnding;

    private int levelScore;

    public float waitForLvel = 5f;

    public string nxtLevel;

    private bool canPause;
    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        currentLives = PlayerPrefs.GetInt("CurrentLives");
        UIManager.instance.livesText.text = "x " + currentLives;
        
        hiScore = PlayerPrefs.GetInt("HighScore");
        UIManager.instance.hiScoreText.text = "Hi-Score: " + hiScore;

        currentScore = PlayerPrefs.GetInt("CurrentScore");
        UIManager.instance.ScoreText.text = "Score: " + PlayerPrefs.GetInt("CurrentScore");

        canPause = true;
    }

    public void Update()
    {
        if (levelEnding)
        {
            PlayerController.instance.transform.position += new Vector3(PlayerController.instance.boostSpd * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseUnpause();
        }
    }
    public void KillPlayer()
    {
        currentLives--;
        UIManager.instance.livesText.text = "x " + currentLives;
        if(currentLives >= 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            UIManager.instance.gameOverScreen.SetActive(true);
            WaveManager.instance.canSpawnWaves = false;
            canPause = false;
            MusicController.instance.PlayGameOver();
            PlayerPrefs.SetInt("HighScore", hiScore);
        }
    }
    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(RespawnGracePeriod);
        HealthManager.instance.respawn();

        WaveManager.instance.continueSpawning();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        levelScore += scoreToAdd;
        UIManager.instance.ScoreText.text = "Score: " + currentScore;

        if(currentScore>hiScore)
        {
            hiScore = currentScore;

            UIManager.instance.hiScoreText.text = "Hi-Score: " + hiScore;
        }
    }

    public IEnumerator EndOLevelCo()
    {
        UIManager.instance.levelEndScrn.SetActive(true);
        
        PlayerController.instance.stopMovement = true;
        levelEnding = true;
        MusicController.instance.PlayVictory();

        canPause = false;
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.endLevelScore.text = "Level Score: " + levelScore;
        UIManager.instance.endLevelScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        PlayerPrefs.SetInt("CurrentScore", currentScore);
        UIManager.instance.endCurrentScore.text = "Total Score: " + currentScore;
        UIManager.instance.endCurrentScore.gameObject.SetActive(true);

        
        if(currentScore == hiScore)
        {
            yield return new WaitForSeconds(0.5f);
            UIManager.instance.highscoreNotice.SetActive(true);
        }
        PlayerPrefs.SetInt("HighScore", hiScore);
        PlayerPrefs.SetInt("CurrentLives", currentLives);

        yield return new WaitForSeconds(waitForLvel);

        SceneManager.LoadScene(nxtLevel);
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScrn.activeInHierarchy)
        {
            UIManager.instance.pauseScrn.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.stopMovement = false;
            MusicController.instance.levelMusic.Play();
        }
        else
        {
            UIManager.instance.pauseScrn.SetActive(true);
            Time.timeScale = 0f;
            PlayerController.instance.stopMovement = true;
            MusicController.instance.levelMusic.Stop();
        }
    }
}
