using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateScore : MonoBehaviour
{
    public Text textScore;
    private int score = 0;

    private static int points = 0;

    public void Calculating()
    {
        score = BinControl.GetRedScore() + BinControl.GetBlueScore() + BinControl.GetYellowScore() + BinControl.GetGreenScore() 
            + EnemyRandomEvent.GetStopScore();
        textScore.text = "Score:" + score;
        SetTotalScore(score);
    }

    public void SetTotalScore(int s)
    {
        points = s;
    }

    public static int GetTotalScore()
    {
        return points;
    }

    void Update()
    {
        Calculating();
        
    }

}
