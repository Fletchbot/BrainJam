using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

public class ScoreManager : MonoBehaviour {

    public GameController gc;


    public float m_HeldScore, h_HeldScore, s_HeldScore, u_HeldScore, noG_HeldScore, ScoresTotal;
    public float m_ScorePercent, h_ScorePercent, s_ScorePercent, u_ScorePercent, noG_ScorePercent;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HeldScorePercentages();
    }

    void HeldScorePercentages()
    {
        noG_HeldScore = gc.noG_HeldScore;
   //     m_HeldScore = gc.m_HeldScore;
        h_HeldScore = gc.h_HeldScore;
        s_HeldScore = gc.s_HeldScore;
        u_HeldScore = gc.u_HeldScore;

        ScoresTotal = noG_HeldScore + h_HeldScore + s_HeldScore + u_HeldScore;

        noG_ScorePercent = noG_HeldScore / ScoresTotal * 100;
 //       m_ScorePercent = m_HeldScore / HeldScoresTotal * 100;
        h_ScorePercent = h_HeldScore / ScoresTotal * 100;
        s_ScorePercent = s_HeldScore / ScoresTotal * 100;
        u_ScorePercent = u_HeldScore / ScoresTotal * 100;

    } 
}

