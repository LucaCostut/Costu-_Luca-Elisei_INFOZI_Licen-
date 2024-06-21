using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    private GameObject m_Menu;
    private GameObject inGameMenu;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemySpawner;

    [SerializeField]
    private GameObject pauseResumeButton;
    [SerializeField]
    private Sprite[] pauseResumeButtonImage;

    [SerializeField]
    private GameObject hpBar;
    [SerializeField]
    private GameObject xpBar;


    [SerializeField]
    private TextMeshProUGUI timeText;
    private int timeInGame;

    [SerializeField]
    private TextMeshProUGUI highScoreText;
    private int highScore;

    private void Start()
    {
        m_Menu     = transform.GetChild(0).gameObject;
        inGameMenu = transform.GetChild(1).gameObject;

        highScore = PlayerPrefs.GetInt("HIGHSCORE");
        
        SetHighScore();
    }

    public void Play()
    {
        timeInGame = 0;
        Time.timeScale = 1.0f;

        m_Menu.SetActive(false);
        inGameMenu.SetActive(true);

        player.SetActive(true);
        enemySpawner.SetActive(true);

        StartCoroutine(ShowTime());
    }
    public void Pause_Resume()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1.0f;
            pauseResumeButton.GetComponent<Image>().sprite = pauseResumeButtonImage[0];
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseResumeButton.GetComponent<Image>().sprite = pauseResumeButtonImage[1];
        }
    }

    public void ReloadScene()
    {
        SetHighScore();
        StartCoroutine(Reload());

    }

    IEnumerator Reload()
    {
        transform.GetChild(transform.childCount - 1).GetComponent<Animator>().SetBool("final", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetHighScore()
    {
        if (timeInGame > highScore)
        {
            highScore = timeInGame - 1;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
        highScoreText.text = "Highscore: " + (highScore / 60).ToString() + ":" + (highScore % 60).ToString();
    }

    IEnumerator ShowTime()
    {
        timeText.text = "Time\n" + (timeInGame/60).ToString() + ":"+ (timeInGame % 60).ToString();
        yield return new WaitForSeconds(1);
        timeInGame++;
        StartCoroutine(ShowTime());
    }

    public void ShowHpBar(float hp,float maxHp)
    {
            hpBar.transform.GetChild(0).localScale = new Vector2(hp / maxHp, 1);
    }

    public void ShowXpBar(float xp,float xpForNextLVl)
    {
        xpBar.transform.GetChild(0).localScale = new Vector2(xp / xpForNextLVl, 1);
    }

}
