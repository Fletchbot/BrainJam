using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Artngame.SKYMASTER
{
    public class GrassGrower : MonoBehaviour {
         public GameObject grassGrower;
         WeatherManager weatherManager;

        public float growSpeed, ungrowSpeed;
        private float grow, ungrow;
        private Vector3 grown, ungrown;
        private bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2;

        // Use this for initialization
        void Start() {
            weatherManager = this.GetComponent<WeatherManager>();

            grow = -200;
            ungrow = -400;
            grown = new Vector3(28, 300, grow);
            ungrown = new Vector3(28, 300, ungrow);
            ungrowSpeed = 60.0f;
            growSpeed = 10.0f;

        }

        // Update is called once per frame
        void Update() {
            NoGesture = weatherManager.NoGesture;
            Mediate = weatherManager.Mediate;
            Happy = weatherManager.Happy;
            Sad = weatherManager.Sad;
            Instr1 = weatherManager.Instr1;
            Instr2 = weatherManager.Instr2;

            if (NoGesture || Sad)
            {
                if(grassGrower.transform.position.z <= grow && grassGrower.transform.position.z >= ungrow)
                {      
                    float step = ungrowSpeed * Time.deltaTime;
                    grassGrower.transform.position = Vector3.MoveTowards(grassGrower.transform.position, ungrown, step);
                }
               
            } else if (Mediate || Happy || Instr1 || Instr2)
            {
                if (grassGrower.transform.position.z <= grow && grassGrower.transform.position.z >= ungrow)
                {
                    float step = growSpeed * Time.deltaTime;
                    grassGrower.transform.position = Vector3.MoveTowards(grassGrower.transform.position, grown, step);
                }
            }

    
    }
    }
}
