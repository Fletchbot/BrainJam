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
        private bool NoGesture, Mediate, Happy, Sad;

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
            NoGesture = gc.GetComponent<GestureController>().NoGesture;
            Mediate = gc.GetComponent<GestureController>().Mediate;
            Happy = gc.GetComponent<GestureController>().Happy;
            Sad = gc.GetComponent<GestureController>().Sad;

            if (NoGesture || Sad)
            {
                if (transform.position.z <= grow && transform.position.z >= ungrow)
                {
                    float step = ungrowSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, ungrown, step);
                }

            }
            else if (Happy)
            {
                if (transform.position.z <= grow && transform.position.z >= ungrow)
                {
                    float step = growSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, grown, step);
                }
            }


        }
    }
}
