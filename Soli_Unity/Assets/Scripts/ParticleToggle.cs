using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Artngame.SKYMASTER;

public class ParticleToggle : MonoBehaviour
{
    public GameObject gc;
    public ParticleSystem Eruption, Lava, BubblingLava, Smoke, PeekABooLava;
    public bool NoGesture, Mediate, Happy, Sad;
    public bool G_sw, M_sw, H_sw, S_sw;
    private Color32 orange;
    private bool IntroTest;
    // Use this for initialization
    void Start()
    {
        orange = new Color32(255,123,0,255);
        ParticleEmissionController();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleEmissionController();
    }

    void ParticleEmissionController()
    {
        NoGesture = gc.GetComponent<GestureController>().NoGesture;
        Mediate = gc.GetComponent<GestureController>().Mediate;
        Happy = gc.GetComponent<GestureController>().Happy;
        Sad = gc.GetComponent<GestureController>().Sad;
        IntroTest = gc.GetComponent<GestureController>().IntroTest;

        var E_em = Eruption.GetComponent<ParticleSystem>().emission.enabled;
        var L_em = Lava.GetComponent<ParticleSystem>().emission.enabled;
        var B_em = BubblingLava.GetComponent<ParticleSystem>().emission.enabled;

        var S_em = Smoke.GetComponent<ParticleSystem>().emission.enabled;
        var P_em = PeekABooLava.GetComponent<ParticleSystem>().emission.enabled;


        if (!IntroTest && Happy && H_sw == false) //Midday Sun
        {
            E_em = false;
            L_em = false;
            B_em = false;

            S_em = true;
            P_em = false;

            Eruption.GetComponent<ParticleSystem>().Stop();
            Lava.GetComponent<ParticleSystem>().Stop();
            BubblingLava.GetComponent<ParticleSystem>().Stop();

            Smoke.GetComponent<ParticleSystem>().Play();
            PeekABooLava.GetComponent<ParticleSystem>().Stop();

            S_sw = false;
            G_sw = false;
            H_sw = true;
            M_sw = false;
        }
        else if (!IntroTest && Sad && S_sw == false) //Winter Night
        {
            E_em = false;
            L_em = false;
            B_em = false;

            S_em = false;
            P_em = false;

            Eruption.GetComponent<ParticleSystem>().Stop();
            Lava.GetComponent<ParticleSystem>().Stop();
            BubblingLava.GetComponent<ParticleSystem>().Stop();

            Smoke.GetComponent<ParticleSystem>().Stop();
            PeekABooLava.GetComponent<ParticleSystem>().Stop();

            S_sw = true;
            G_sw = false;
            H_sw = false;
            M_sw = false;
        } 

        else if (NoGesture && G_sw == false) //Volcano Erupt
        {
            E_em = true;
            L_em = true;
            B_em = true;

            S_em = false;
            P_em = false;

            Eruption.GetComponent<ParticleSystem>().Play();
            Lava.GetComponent<ParticleSystem>().Play();
            BubblingLava.GetComponent<ParticleSystem>().Play();

            Smoke.GetComponent<ParticleSystem>().Stop();
            PeekABooLava.GetComponent<ParticleSystem>().Stop();

            S_sw = false;
            G_sw = true;
            H_sw = false;
            M_sw = false;
        }
        else if (Mediate && M_sw == false) //sunset
        { 
            E_em = false;
            L_em = false;
            B_em = false;

            S_em = false;
            P_em = true;

            ParticleSystem.MainModule PeekABooColor = PeekABooLava.GetComponent<ParticleSystem>().main;
            PeekABooColor.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, orange);

            Eruption.GetComponent<ParticleSystem>().Stop();
            Lava.GetComponent<ParticleSystem>().Stop();
            BubblingLava.GetComponent<ParticleSystem>().Stop();

            Smoke.GetComponent<ParticleSystem>().Stop();
            PeekABooLava.GetComponent<ParticleSystem>().Play();

            S_sw = false;
            G_sw = false;
            H_sw = false;
            M_sw = true;
        }
    }
}
