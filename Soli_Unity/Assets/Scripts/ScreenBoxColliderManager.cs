using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniOSC
{
    public class ScreenBoxColliderManager : MonoBehaviour
    {
        public GameObject leftBoxCollider, centreBoxCollider, rightBoxCollider;

        public float aY, leftMin, rightMin;
        private int ratio;

        // Use this for initialization
        void OnEnable()
        {
            ratio = 100;
            leftMin = 7.5f;
            rightMin = 10.5f;
        }

        // Update is called once per frame
        void Update()
        {
            aY = UniOSCMuseMonitor.main.accY * ratio;

            if (aY <= leftMin)
            {
                leftBoxCollider.GetComponent<ActivateObjects>().SetActive(true);

                centreBoxCollider.GetComponent<ActivateObjects>().SetDeactive(true);
                rightBoxCollider.GetComponent<ActivateObjects>().SetDeactive(true);
            }
            else if (aY >= leftMin && aY <= rightMin)
            {
                centreBoxCollider.GetComponent<ActivateObjects>().SetActive(true);

                leftBoxCollider.GetComponent<ActivateObjects>().SetDeactive(true);
                rightBoxCollider.GetComponent<ActivateObjects>().SetDeactive(true);
            }
            else if (aY >= rightMin)
            {
                rightBoxCollider.GetComponent<ActivateObjects>().SetActive(true);

                leftBoxCollider.GetComponent<ActivateObjects>().SetDeactive(true);
                centreBoxCollider.GetComponent<ActivateObjects>().SetDeactive(true);
            }
        }
    }
}
