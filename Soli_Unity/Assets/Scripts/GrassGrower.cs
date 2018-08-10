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
        private bool NoGesture, Meditate, Happy, Sad, Unsure;
        private bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;
        private bool noG_sw, M_sw, H_sw, S_sw, U_sw;
        private bool StartGame;

        // Use this for initialization
        void OnEnable()
        {
            grow = -200;
            ungrow = -400;
            grown = new Vector3(28, 300, grow);
            ungrown = new Vector3(28, 300, ungrow);
            ungrowSpeed = 60.0f;
            growSpeed = 10.0f;
            transform.position = ungrown;
        }

        // Update is called once per frame
        void Update()
        {
            StartGame = gc.GetComponent<GestureController>().StartGame;

            NoGesture = gc.GetComponent<GestureController>().NoGesture;
            Meditate = gc.GetComponent<GestureController>().Meditate;
            Happy = gc.GetComponent<GestureController>().Happy;
            Sad = gc.GetComponent<GestureController>().Sad;
            Unsure = gc.GetComponent<GestureController>().Unsure;

            noGHeld_Reached = gc.GetComponent<GestureController>().noGHeld_Reached;
            mHeld_Reached = gc.GetComponent<GestureController>().mHeld_Reached;
            hHeld_Reached = gc.GetComponent<GestureController>().hHeld_Reached;
            sHeld_Reached = gc.GetComponent<GestureController>().sHeld_Reached;


            if (noGHeld_Reached)
            {
                noG_sw = true;
            }
            if (mHeld_Reached)
            {
                M_sw = true;
            }
            if (hHeld_Reached)
            {
                H_sw = true;
            }
            if (sHeld_Reached)
            {
                S_sw = true;
            }

            //UNGROW GRASS
            if (NoGesture && !StartGame || NoGesture && StartGame && noG_sw || Sad && !StartGame || Sad && StartGame && S_sw)
            {
                if (transform.position.z <= grow && transform.position.z >= ungrow)
                {
                    float step = ungrowSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, ungrown, step);
                }
                else
                {
                    S_sw = false;
                    noG_sw = false;
                }              
            }

            //GROW GRASS
            if (Happy && !StartGame || Happy && H_sw && StartGame || Meditate && StartGame && M_sw)
            {
                if (transform.position.z <= grow && transform.position.z >= ungrow)
                {
                    float step = growSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, grown, step);
                }
                else
                {
                    H_sw = false;
                    M_sw = false;
                }
            }
        }
    }
}
