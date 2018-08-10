using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Artngame.SKYMASTER;

public class ParticleToggle : MonoBehaviour
{
    public GameObject gc;
    public ParticleSystem Eruption, Lava, BubblingLava, Smoke, PeekABooLava;
    public bool NoGesture, Meditate, Happy, Sad, Unsure;
    public bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached;
    public bool G_sw, M_sw, H_sw, S_sw, U_sw;
    private Color32 orange;
    private bool StartGame;

    // Use this for initialization
    void OnEnable()
    {
        orange = new Color32(255,123,0,255);
        NoGesture = true;
        Smoke.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleEmissionController();
    }

    void ParticleEmissionController()
    {
        NoGesture = gc.GetComponent<GestureController>().NoGesture;
        Meditate = gc.GetComponent<GestureController>().Meditate;
        Happy = gc.GetComponent<GestureController>().Happy;
        Sad = gc.GetComponent<GestureController>().Sad;
        Unsure = gc.GetComponent<GestureController>().Unsure;

        noGHeld_Reached = gc.GetComponent<GestureController>().noGHeld_Reached;
        mHeld_Reached = gc.GetComponent<GestureController>().mHeld_Reached;
        hHeld_Reached = gc.GetComponent<GestureController>().hHeld_Reached;
        sHeld_Reached = gc.GetComponent<GestureController>().sHeld_Reached;

        StartGame = gc.GetComponent<GestureController>().StartGame;

        var E_em = Eruption.GetComponent<ParticleSystem>().emission.enabled;
        var L_em = Lava.GetComponent<ParticleSystem>().emission.enabled;
        var B_em = BubblingLava.GetComponent<ParticleSystem>().emission.enabled;

        var S_em = Smoke.GetComponent<ParticleSystem>().emission.enabled;
        var P_em = PeekABooLava.GetComponent<ParticleSystem>().emission.enabled;

        if (NoGesture || Unsure)
        {
            if (!G_sw)
            {
                S_em = false;
                P_em = false;

                Smoke.GetComponent<ParticleSystem>().Stop();
                PeekABooLava.GetComponent<ParticleSystem>().Stop();

                if (StartGame && !noGHeld_Reached) //ALL OFF
                {
                    E_em = false;
                    L_em = false;
                    B_em = false;

                    Eruption.GetComponent<ParticleSystem>().Stop();
                    Lava.GetComponent<ParticleSystem>().Stop();
                    BubblingLava.GetComponent<ParticleSystem>().Stop();
                }
                else if (StartGame && noGHeld_Reached) //ERUPTION
                {
                    E_em = true;
                    L_em = true;
                    B_em = true;

                    Eruption.GetComponent<ParticleSystem>().Play();
                    Lava.GetComponent<ParticleSystem>().Play();
                    BubblingLava.GetComponent<ParticleSystem>().Play();
                }
                else if (!StartGame) //Test Eruption
                {
                    E_em = true;
                    L_em = true;
                    B_em = true;

                    Eruption.GetComponent<ParticleSystem>().Play();
                    Lava.GetComponent<ParticleSystem>().Play();
                    BubblingLava.GetComponent<ParticleSystem>().Play();
                }

                S_sw = false;
                G_sw = true;
                H_sw = false;
                M_sw = false;
            }        
        }

        if (Meditate)
        {
            if (!M_sw)
            {
                E_em = false;
                L_em = false;
                B_em = false;

                S_em = false;
                P_em = true;

                //Stop all fx except peekaboo
                Eruption.GetComponent<ParticleSystem>().Stop();
                Lava.GetComponent<ParticleSystem>().Stop();
                BubblingLava.GetComponent<ParticleSystem>().Stop();
                Smoke.GetComponent<ParticleSystem>().Stop();

                if (StartGame && !mHeld_Reached) // orangy yellow peekaboo
                {
                    ParticleSystem.MainModule PeekABooColor1 = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor1.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, orange);
                    PeekABooLava.GetComponent<ParticleSystem>().Play();
                }
                else if (StartGame && mHeld_Reached || !StartGame) //redish peekaboo
                {
                    ParticleSystem.MainModule PeekABooColor2 = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor2.startColor = new ParticleSystem.MinMaxGradient(orange, Color.red);

                    PeekABooLava.GetComponent<ParticleSystem>().Play();
                }

                S_sw = false;
                G_sw = false;
                H_sw = false;
                M_sw = true;
            }
        }

        if (Happy && !Meditate)
        {
            if (!H_sw)
            {
                E_em = false;
                L_em = false;
                B_em = false;

                Eruption.GetComponent<ParticleSystem>().Stop();
                Lava.GetComponent<ParticleSystem>().Stop();
                BubblingLava.GetComponent<ParticleSystem>().Stop();

                if (StartGame && !hHeld_Reached) //Smoke
                {
                    S_em = true;
                    P_em = false;

                    Smoke.GetComponent<ParticleSystem>().Play();
                    PeekABooLava.GetComponent<ParticleSystem>().Stop();
                }
                else if (StartGame && hHeld_Reached || !StartGame) //stop smoke blueish greenish peekaboo
                {
                    P_em = true;
                    S_em = false;

                    ParticleSystem.MainModule PeekABooColor = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor.startColor = new ParticleSystem.MinMaxGradient(Color.blue, Color.green);

                    Smoke.GetComponent<ParticleSystem>().Stop();
                    PeekABooLava.GetComponent<ParticleSystem>().Play();
                }

                H_sw = true;
                S_sw = false;
                G_sw = false;
                M_sw = false;
            }
        }

        if (Sad && !Meditate) 
        {
            if (!S_sw)
            {
                E_em = false;
                L_em = false;
                B_em = false;

                Eruption.GetComponent<ParticleSystem>().Stop();
                Lava.GetComponent<ParticleSystem>().Stop();
                BubblingLava.GetComponent<ParticleSystem>().Stop();

                if (StartGame && !sHeld_Reached) //Smoke
                {
                    S_em = true;
                    P_em = false;

                    Smoke.GetComponent<ParticleSystem>().Play();
                    PeekABooLava.GetComponent<ParticleSystem>().Stop();
                }
                else if (StartGame && sHeld_Reached || !StartGame) // stop smoke purple blueish peekaboo
                {
                    S_em = false;
                    P_em = true;

                    ParticleSystem.MainModule PeekABooColor = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor.startColor = new ParticleSystem.MinMaxGradient(Color.magenta, Color.blue);

                    Smoke.GetComponent<ParticleSystem>().Stop();
                    PeekABooLava.GetComponent<ParticleSystem>().Play();
                }

                S_sw = true;
                G_sw = false;
                H_sw = false;
                M_sw = false;
            }     
        }
    }
}
