using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Artngame.SKYMASTER;
using SoliGameController;

public class ParticleToggle : MonoBehaviour
{
    public GameController gc;
    public ParticleSystem Eruption, Lava, BubblingLava, Smoke, PeekABooLava;
    public bool NoGesture, Meditate, Happy, Sad, Unsure;
    public bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached;
    public bool G_sw, M_sw, H_sw, S_sw, U_sw;
    private Color32 orange;

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
        NoGesture = gc.NoGesture;
        Meditate = gc.Meditate;
        Happy = gc.Happy;
        Sad = gc.Sad;
        Unsure = gc.Unsure;

        noGHeld_Reached = gc.noGHeld_Reached;
        mHeld_Reached = gc.mHeld_Reached;
        hHeld_Reached = gc.hHeld_Reached;
        sHeld_Reached = gc.sHeld_Reached;

        var E_em = Eruption.GetComponent<ParticleSystem>().emission.enabled;
        var L_em = Lava.GetComponent<ParticleSystem>().emission.enabled;
        var B_em = BubblingLava.GetComponent<ParticleSystem>().emission.enabled;

        var S_em = Smoke.GetComponent<ParticleSystem>().emission.enabled;
        var P_em = PeekABooLava.GetComponent<ParticleSystem>().emission.enabled;

        if (NoGesture)
        {
            if (!G_sw)
            {
                S_em = false;
                P_em = false;

                Smoke.GetComponent<ParticleSystem>().Stop();
                PeekABooLava.GetComponent<ParticleSystem>().Stop();

                if (gc.state == -1 && !noGHeld_Reached) //ALL OFF
                {
                    E_em = false;
                    L_em = false;
                    B_em = false;

                    Eruption.GetComponent<ParticleSystem>().Stop();
                    Lava.GetComponent<ParticleSystem>().Stop();
                    BubblingLava.GetComponent<ParticleSystem>().Stop();
                }
                else if (gc.state == -1 && noGHeld_Reached) //ERUPTION
                {
                    E_em = true;
                    L_em = true;
                    B_em = true;

                    Eruption.GetComponent<ParticleSystem>().Play();
                    Lava.GetComponent<ParticleSystem>().Play();
                    BubblingLava.GetComponent<ParticleSystem>().Play();

                    G_sw = true;
                }
                else if (gc.state >= 0) //Test Eruption
                {
                    E_em = true;
                    L_em = true;
                    B_em = true;

                    Eruption.GetComponent<ParticleSystem>().Play();
                    Lava.GetComponent<ParticleSystem>().Play();
                    BubblingLava.GetComponent<ParticleSystem>().Play();

                    G_sw = true;
                }

                S_sw = false;
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

                if (gc.state == -1 && !mHeld_Reached) // orangy yellow peekaboo
                {
                    ParticleSystem.MainModule PeekABooColor1 = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor1.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, orange);
                    PeekABooLava.GetComponent<ParticleSystem>().Play();
                }
                else if (gc.state == -1 && mHeld_Reached || gc.state >= 0) //redish peekaboo
                {
                    ParticleSystem.MainModule PeekABooColor2 = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor2.startColor = new ParticleSystem.MinMaxGradient(orange, Color.red);

                    PeekABooLava.GetComponent<ParticleSystem>().Play();

                    M_sw = true;
                }

                S_sw = false;
                G_sw = false;
                H_sw = false;
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

                if (gc.state == -1 && !hHeld_Reached) //Smoke
                {
                    S_em = true;
                    P_em = false;

                    Smoke.GetComponent<ParticleSystem>().Play();
                    PeekABooLava.GetComponent<ParticleSystem>().Stop();
                }
                else if (gc.state == -1 && hHeld_Reached || gc.state >= 0) //stop smoke blueish greenish peekaboo
                {
                    P_em = true;
                    S_em = false;

                    ParticleSystem.MainModule PeekABooColor = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor.startColor = new ParticleSystem.MinMaxGradient(Color.blue, Color.green);

                    Smoke.GetComponent<ParticleSystem>().Stop();
                    PeekABooLava.GetComponent<ParticleSystem>().Play();

                    H_sw = true;
                }
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

                if (gc.state == -1 && !sHeld_Reached) //Smoke
                {
                    S_em = true;
                    P_em = false;

                    Smoke.GetComponent<ParticleSystem>().Play();
                    PeekABooLava.GetComponent<ParticleSystem>().Stop();
                }
                else if (gc.state == -1 && sHeld_Reached || gc.state >= 0) // stop smoke purple blueish peekaboo
                {
                    S_em = false;
                    P_em = true;

                    ParticleSystem.MainModule PeekABooColor = PeekABooLava.GetComponent<ParticleSystem>().main;
                    PeekABooColor.startColor = new ParticleSystem.MinMaxGradient(Color.magenta, Color.blue);

                    Smoke.GetComponent<ParticleSystem>().Stop();
                    PeekABooLava.GetComponent<ParticleSystem>().Play();

                    S_sw = true;
                }

                G_sw = false;
                H_sw = false;
                M_sw = false;
            }     
        }
    }
}
