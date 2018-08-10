using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Artngame.SKYMASTER
{
    public class GrassGrower : MonoBehaviour
    {
        public GameObject gc;

        public float growSpeed, ungrowSpeed;
        private float grow, ungrow;
        private Vector3 grown, ungrown;
        private bool NoGesture, Happy, Sad;
        private bool noGHeld_Reached, hHeld_Reached, sHeld_Reached;
        private bool noGHeldOff, noGHeld_Rsw, hHeldOff, hHeld_Rsw, sHeldOff, sHeld_Rsw;
        private bool StartGame;

        // Use this for initialization
        void OnEnable()
        {
            grow = -200;
            ungrow = -400;
            grown = new Vector3(28, 300, grow);
            ungrown = new Vector3(28, 300, ungrow);
            ungrowSpeed = 60.0f;
            growSpeed = 12.0f;
            transform.position = ungrown;
        }

        // Update is called once per frame
        void Update()
        {
            StartGame = gc.GetComponent<GestureController>().StartGame;

            NoGesture = gc.GetComponent<GestureController>().NoGesture;
            Happy = gc.GetComponent<GestureController>().Happy;
            Sad = gc.GetComponent<GestureController>().Sad;

            HeldReachedSW();
            GrassTrigger();
        }

        void GrassTrigger()
        {
            //UNGROW GRASS when nogesture or sad test or sad and sadheld
            if (NoGesture && !StartGame || NoGesture && StartGame && noGHeld_Rsw || Sad && !StartGame || Sad && StartGame && sHeld_Rsw)
            {
                if (transform.position.z <= grow && transform.position.z >= ungrow)
                {
                    float step = ungrowSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, ungrown, step);
                }
                else
                {
                    sHeld_Rsw = false;
                    noGHeld_Rsw = false;
                }
            }

            //GROW GRASS when happy test or happy and happyheld
            if (Happy && !StartGame || Happy && hHeld_Rsw && StartGame)
            {
                if (transform.position.z <= grow && transform.position.z >= ungrow)
                {
                    float step = growSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, grown, step);
                }
                else
                {
                    hHeld_Rsw = false;
                }
            }
        }

        void HeldReachedSW()
        {
            noGHeld_Reached = gc.GetComponent<GestureController>().noGHeld_Reached;
            hHeld_Reached = gc.GetComponent<GestureController>().hHeld_Reached;
            sHeld_Reached = gc.GetComponent<GestureController>().sHeld_Reached;

            if (noGHeld_Reached && !noGHeldOff)
            {
                noGHeld_Rsw = true;

                noGHeldOff = true;
            }
            if (hHeld_Reached && !hHeldOff)
            {
                hHeld_Rsw = true;

                hHeldOff = true;
            }
            if (sHeld_Reached && !sHeldOff)
            {
                sHeld_Rsw = true;

                sHeldOff = true;
            }

        }

    }
}
