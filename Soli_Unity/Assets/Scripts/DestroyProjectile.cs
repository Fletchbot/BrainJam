using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {

    public GameObject gc;

    //public GameObject exposion;

    public int damage = 4;
    private bool focusShot, noGC;


    public string gc_tag;

    // Use this for initialization
    void Start () {
        gc_tag = "GameController";
    }

    // Update is called once per frame
    void Update () {

        if (!noGC)
        {
            findGameObj();
            noGC = true;
        }

        focusShot = gc.GetComponent<GestureController>().isFocus;

  /*      if (damage <= 0)
        {
            string tag = gameObject.tag;

            gc.GetComponent<GestureController>().projectileDestroyed(tag);

            //GameObject exposionClone = Instantiate(exposion, transform.position, transform.rotation);

            Debug.Log("pTag" + tag);

            Destroy(gameObject);
        }
        else
        {
            gc.GetComponent<GestureController>().projectileDestroyed("");
        }*/
    }

    void findGameObj()
    {
        gc = GameObject.FindGameObjectWithTag(gc_tag);
    }

    void OnTriggerStay(Collider other)
    {
        string tag = gameObject.tag;

  //      gc.GetComponent<GestureController>().projectileDestroyed(tag);

        //GameObject exposionClone = Instantiate(exposion, transform.position, transform.rotation);

      //  Debug.Log("pTag" + tag);

        Destroy(gameObject);

        if (focusShot)
        {
            damage--;
        }

    }
}
