using UnityEngine;
using UnityEngine.UI;

public class EnemyRandomEvent : MonoBehaviour
{
    [SerializeField]private Image markImage;
    private float time;
    private float count =3f;

    private static int score = 0;

    [SerializeField]private Transform NPC;
    [SerializeField]private GameObject[] rubbish;

    private bool isHit = false;
    private bool isHappened = false;
    private bool isTimeOut = false;

    public void SetStopScore(int s)
    {
        score = s;
    }

    public static int GetStopScore()
    {
        return score;
    }

    void Start()
    {
        SetStopScore(0);

        time = Random.Range(10f, 20f);
        InvokeRepeating("RandomEvent", 5f, time); 
    }

    public void IsHit()
    {
        if (!isTimeOut && isHappened)
        {
            AudioManager.Instance.PlaySFX("Click", false);

            markImage.gameObject.SetActive(false);
            score += 20;
            SetStopScore(score);
        }

        Initialize();
    }

    void MarkShowUp()
    {
        markImage.gameObject.SetActive(true);

        //NoviceGuidePanel.instance.NextStep(MenuGuideConst.ClickBtnStep1);
    }

    void RandomEvent()
    {
        isHappened = true;

        AudioManager.Instance.PlaySFX("Hint", false);
        Invoke("MarkShowUp", 1.3f);
    }

    void Timer()
    {
        if(!isTimeOut)
        {
            count -= Time.deltaTime;
        }

        if (count <= 0)
        {
            isTimeOut = true;

            if (!isHit)
            {
                markImage.gameObject.SetActive(false);

                int rubbishIndex = Random.Range(0, rubbish.Length);
                GameObject _Instance = null;
                if (_Instance == null)
                {
                    _Instance = (GameObject)Instantiate(rubbish[rubbishIndex], NPC.position - Vector3.up * 2f, Quaternion.identity);
                }

                Initialize();
            }
           
        }
    }

    void Initialize()
    {
        count = 3f;

        isHappened = false;
        isHit = false;
        isTimeOut = false;
    }

    void Update()
    {
        if (isHappened)
        {
            Timer();
        }  
    }
}
