using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Artngame.SKYMASTER
{
    public class GrassGrower : MonoBehaviour {
         public GameObject grassGrower;
         WeatherManager weatherManager;
        
        public float grow, ungrow;
        public Vector3 grown, ungrown;
        public float speed;

        public bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2;

        // Use this for initialization
        void Start() {
            weatherManager = this.GetComponent<WeatherManager>();

            grow = -200;
            ungrow = -400;
            grown = new Vector3(28, 300, grow);
            ungrown = new Vector3(28, 300, ungrow);

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
                    speed = 60.0f;
                    float step = speed * Time.deltaTime;
                    grassGrower.transform.position = Vector3.MoveTowards(grassGrower.transform.position, ungrown, step);
                }
               
            } else if (Mediate || Happy || Instr1 || Instr2)
            {
                if (grassGrower.transform.position.z <= grow && grassGrower.transform.position.z >= ungrow)
                {
                    speed = 10.0f;
                    float step = speed * Time.deltaTime;
                    grassGrower.transform.position = Vector3.MoveTowards(grassGrower.transform.position, grown, step);
                }
            }

    
    }
    }
}
