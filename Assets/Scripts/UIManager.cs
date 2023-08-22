using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float timer = 45f;

    public Text timerText;
    public Text scoreText;

    private bool isTimeOut = false;

    public GameObject startPanel;
    public GameObject winPanel;
    public NoviceGuidePanel guidingPanel;

    public GameObject[] stars;

    private int score;
    private bool canCilck = false;
    private bool isStart = false;

    private bool isStep1 = true;
    private bool isStep2 = false;
    private bool isStep3 = false;
    private bool isStep4 = false;
    private static bool isStep5 = false;

    [SerializeField] private int taskPoint1;
    [SerializeField] private int taskPoint2;
    [SerializeField] private int taskPoint3;

    public static void SetIsStep5(bool flag)
    {
        isStep5 = flag;
    }

    public static bool GetIsStep5()
    {
        return isStep5;
    }

    void Start()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlayMusic("BGM");

        if(guidingPanel != null)
        {
            guidingPanel.gameObject.SetActive(true);
            guidingPanel.ExcuteStep(0);
        }

        if(guidingPanel == null)
        {
            StartGame();

            Invoke("Hidden", 3f);
        }
    }

    void StartGame()
    {
        startPanel.SetActive(true);
    }

    void Hidden()
    {
        startPanel.SetActive(false);

        isStart = true;
    }

    void Update()
    {
        if (isStart)
        {
            Timer();
        }

        if(guidingPanel != null)
        {
            if (Input.GetMouseButtonDown(0) && isStep1)
            {
                NoviceGuidePanel.instance.NextStep(MenuGuideConst.ClickBtnStep1);
                StartGame();

                isStep1 = false;
                isStep2 = true;
            }

            else if (Input.GetMouseButtonDown(0) && isStep2)
            {
                NoviceGuidePanel.instance.NextStep(MenuGuideConst.ClickBtnStep2);
                Hidden();

                isStep2 = false;
                isStep3 = true;
            }

            else if (Input.GetKey(GameManager.GM.forward) || Input.GetKey(GameManager.GM.backward)
                || Input.GetKey(GameManager.GM.left) || Input.GetKey(GameManager.GM.right) && isStep3)
            {
                NoviceGuidePanel.instance.NextStep(MenuGuideConst.ClickBtnStep3);

                isStep3 = false;
                isStep4 = true;
            }

            else if (isStep4 && RubbishCollectable.isCollect)
            {
                NoviceGuidePanel.instance.NextStep(MenuGuideConst.ClickBtnStep4);

                isStep4 = false;
                SetIsStep5(true);
            }

            else if (Input.GetMouseButtonDown(0) && !isStep5)
            {
                guidingPanel.gameObject.SetActive(false);
            }
        }
    }

    void Timer()
    {
        if (!isTimeOut)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F0") + "s";

            if (timer <= 10.5f)
            {
                AudioManager.Instance.PlaySFX("CountDown", true);
            }
        }
        
        if(timer <= 0)
        {
            isTimeOut = true;
            timerText.text = "0s";

            score = CalculateScore.GetTotalScore();
            StartCoroutine("ShowStars");
        }
    }

    IEnumerator ShowStars()
    {
        winPanel.SetActive(true);
        scoreText.text = "Score:" + score;

        AudioManager.Instance.musicSource.Stop();

        if (score < taskPoint1)
        {
            AudioManager.Instance.PlaySFX("Lose", true);
            canCilck = true;
        }

        else if (score < taskPoint2)
        {
            AudioManager.Instance.PlaySFX("Win", true);

            yield return new WaitForSeconds(0.5f);
            stars[0].SetActive(true);
            canCilck = true;
        }

        else if (score < taskPoint3)
        {
            AudioManager.Instance.PlaySFX("Win", true);

            yield return new WaitForSeconds(0.5f);
            stars[0].SetActive(true);

            yield return new WaitForSeconds(0.5f);
            stars[1].SetActive(true);
            canCilck = true;
        }

        else
        {
            AudioManager.Instance.PlaySFX("Win", true);

            yield return new WaitForSeconds(0.6f);
            stars[0].SetActive(true);

            yield return new WaitForSeconds(0.6f);
            stars[1].SetActive(true);

            yield return new WaitForSeconds(0.6f);
            stars[2].SetActive(true);
            canCilck = true;
        }
    }

    public void RestartGame()
    {
        if (canCilck)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ReturnToMainMenu()
    {
        if (canCilck)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void NextLevel()
    {
        if (canCilck)
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
        }
    }
}
