using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniOSC
{
    public class MuseCursor : MonoBehaviour  {
        public Vector3 xyz;
        public int topEdge, bottomEdge, leftEdge, rightEdge, ratio;
        public float aX,aY, speed; 
	// Use this for initialization
	void Start () {
            topEdge = 341;
            bottomEdge = 311;
            leftEdge = 20;
            rightEdge = 62;
            ratio = 100;
            speed = 0.3f;
        }

        // Update is called once per frame
        void Update() {
            aX= -UniOSCMuseMonitor.main.accX*ratio;
            aY  = UniOSCMuseMonitor.main.accY*ratio;


            if (aY >= 13)
            {
                xyz.x = speed;
            }
            else if (aY <= 7.4)
            {
                xyz.x = -speed;
            }
            else
            {
                xyz.x = 0;
            } 
            
            if(aX >= 30)
            {
                xyz.y = speed;
            }
            else if (aX <= 18)
            {
                xyz.y = -speed;
            }
            else
            {
                xyz.y = 0.0f;
            } 


            if (transform.position.y >= topEdge )
            {
                xyz.y = 0.01f;

                transform.position -= xyz;
            }
            else if (transform.position.y <= bottomEdge)
            {
                xyz.y = 0.01f;

                transform.position += xyz;

            }
            else if (transform.position.x <= leftEdge)
            {
                xyz.x = 0.01f;

                transform.position += xyz;
            }
            else if (transform.position.x >= rightEdge)
            {
                xyz.x = 0.01f;

                transform.position -= xyz;
            }
            else
            {
                transform.position += xyz;
            }





        }
    }
}
