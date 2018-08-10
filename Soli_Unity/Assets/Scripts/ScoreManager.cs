using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public GameObject gestureController;
    public bool p1Destroyed, p2Destroyed, pPoint, sCombo, sdCombo, cStack;
    public int p1combo, p2combo, superCombo, superduperCombo, comboStackTotal;
    public int p1Total, p2Total, superTotal, superduperTotal;
    public float projectileCountdown, comboCountdown;

    public float m_HeldScore, h_HeldScore, s_HeldScore, u_HeldScore, noG_HeldScore, HeldScoresTotal;
    public float m_ScorePercent, h_ScorePercent, s_ScorePercent, u_ScorePercent, noG_ScorePercent;

    public float sixtysecCounter, thirtysecCounter, tensecCounter, fivesecCounter, threesecCounter, twosecCounter, secCounter;

    // Use this for initialization
    void Start()
    {
        sixtysecCounter = 60.0f;
        thirtysecCounter = 30.0f;
        tensecCounter = 10.0f;
        fivesecCounter = 5.0f;
        threesecCounter = 3.0f;
        twosecCounter = 2.0f;
        secCounter = 1.0f;

        projectileCountdown = twosecCounter;
        comboCountdown = tensecCounter;
    }

    // Update is called once per frame
    void Update()
    {
        HeldScorePercentages();
    }

    void HeldScorePercentages()
    {
        noG_HeldScore = gestureController.GetComponent<GestureController>().noG_HeldScore;
        m_HeldScore = gestureController.GetComponent<GestureController>().m_HeldScore;
        h_HeldScore = gestureController.GetComponent<GestureController>().h_HeldScore;
        s_HeldScore = gestureController.GetComponent<GestureController>().s_HeldScore;
        u_HeldScore = gestureController.GetComponent<GestureController>().u_HeldScore;

        HeldScoresTotal = noG_HeldScore + m_HeldScore + h_HeldScore + s_HeldScore + u_HeldScore;

        noG_ScorePercent = noG_HeldScore / HeldScoresTotal * 100;
        m_ScorePercent = m_HeldScore / HeldScoresTotal * 100;
        h_ScorePercent = h_HeldScore / HeldScoresTotal * 100;
        s_ScorePercent = s_HeldScore / HeldScoresTotal * 100;
        u_ScorePercent = u_HeldScore / HeldScoresTotal * 100;

    } 

    public void projectileDestroyed(string p)
    {
        if (p == "Projectile1")
        {
            p1Destroyed = true;
            p1Total++;
            p1combo++;
            p1Destroyed = false;
        }
        else if (p == "Projectile2")
        {
            p2Destroyed = true;
            p2Total++;
            p2combo++;
        }
        else
        {
            p1Destroyed = false;
            p2Destroyed = false;
        }
    }
    void ScoreBoard()
    {
        projectileCountdown -= Time.deltaTime;
        comboCountdown -= Time.deltaTime;

        if (projectileCountdown <= 0.0f)
        {
            if (p1combo >= 2 && p2combo >= 2)
            {
                pPoint = false;
                sCombo = false;
                sdCombo = true;

                superduperCombo++;
                superduperTotal++;
            }
            else if (p1combo == 1 && p2combo == 1)
            {
                pPoint = false;
                sCombo = true;
                sdCombo = false;

                superCombo++;
                superTotal++;
            }
            else if (p1combo >= 1 && p2combo == 0 || p1combo == 0 && p2combo >= 1)
            {
                pPoint = true;
                sCombo = false;
                sdCombo = false;
            }

            p1combo = 0;
            p2combo = 0;
            projectileCountdown = threesecCounter;
        }

        if (comboCountdown <= 0.0f)
        {
            if (superCombo >= 2 || superduperCombo >= 2)
            {
                cStack = true;
                comboStackTotal++;
            }
            else
            {
                cStack = false;
            }
            superduperCombo = 0;
            superCombo = 0;
            comboCountdown = tensecCounter;
        }

    }
}

