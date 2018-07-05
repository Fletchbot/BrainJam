using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Artngame.SKYMASTER;

public class ParticleToggle : MonoBehaviour
{
    public GameObject gc;
    public ParticleSystem Eruption, Lava, BubblingLava, Smoke, PeekABooLava;
    public bool gameStart, NoGesture, Mediate, Happy, Sad, Instr1, Instr2;
    public bool G_sw, M_sw, H_sw, S_sw, I1_sw, I2_sw;

    // Use this for initialization
    void Start()
    {
   //     gameStart = true;
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
        Instr1 = gc.GetComponent<GestureController>().Instr1;
        Instr2 = gc.GetComponent<GestureController>().Instr2;

        var E_em = Eruption.GetComponent<ParticleSystem>().emission.enabled;
        var L_em = Lava.GetComponent<ParticleSystem>().emission.enabled;
        var B_em = BubblingLava.GetComponent<ParticleSystem>().emission.enabled;

        var S_em = Smoke.GetComponent<ParticleSystem>().emission.enabled;
        var P_em = PeekABooLava.GetComponent<ParticleSystem>().emission.enabled;

        if (gameStart)
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

            gameStart = false;
        }
        else if (Happy && H_sw == false)
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
            I1_sw = false;
            I2_sw = false;
        }
        else if (Sad && S_sw == false)
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

            S_sw = true;
            G_sw = false;
            H_sw = false;
            M_sw = false;
            I1_sw = false;
            I2_sw = false; 
        } 
        else if (Instr1 && I1_sw == false)
        {
            E_em = false;
            L_em = false;
            B_em = false;

            S_em = true;
            P_em = true;

            Eruption.GetComponent<ParticleSystem>().Stop();
            Lava.GetComponent<ParticleSystem>().Stop();
            BubblingLava.GetComponent<ParticleSystem>().Stop();

            Smoke.GetComponent<ParticleSystem>().Play();
            PeekABooLava.GetComponent<ParticleSystem>().Play();

            S_sw = false;
            G_sw = false;
            H_sw = false;
            M_sw = false;
            I1_sw = true;
            I2_sw = false;
        } else if (Instr2 && I2_sw == false)
        {
            E_em = false;
            L_em = false;
            B_em = false;

            S_em = true;
            P_em = true;

            Eruption.GetComponent<ParticleSystem>().Stop();
            Lava.GetComponent<ParticleSystem>().Stop();
            BubblingLava.GetComponent<ParticleSystem>().Stop();

            Smoke.GetComponent<ParticleSystem>().Play();
            PeekABooLava.GetComponent<ParticleSystem>().Play();

            S_sw = false;
            G_sw = false;
            H_sw = false;
            M_sw = false;
            I1_sw = false;
            I2_sw = true;
        }
        else if (NoGesture && G_sw == false)
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

            S_sw = false;
            G_sw = true;
            H_sw = false;
            M_sw = false;
            I1_sw = false;
            I2_sw = false;
        }
        else if (Mediate && M_sw == false)
        { 
            E_em = false;
            L_em = false;
            B_em = false;

            S_em = false;
            P_em = true;

            Eruption.GetComponent<ParticleSystem>().Stop();
            Lava.GetComponent<ParticleSystem>().Stop();
            BubblingLava.GetComponent<ParticleSystem>().Stop();

            Smoke.GetComponent<ParticleSystem>().Stop();
            PeekABooLava.GetComponent<ParticleSystem>().Play();

            S_sw = false;
            G_sw = false;
            H_sw = false;
            M_sw = true;
            I1_sw = false;
            I2_sw = false;
        }
    }
}
