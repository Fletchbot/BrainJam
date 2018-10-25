using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

public class ProjectileEmitter : MonoBehaviour
{
    public GameObject projectileEmitter;
    public GameObject projectile1, projectile2;
    public GameObject emitterForce;
    public GameController gc;
    public FocusState fs;

    private GameObject Temp_Projectile_Handler;

    private float leftEdge, rightEdge, topEdge, bottomEdge, backEdge, frontEdge;

    private float moveX, moveY, moveZ, velocityMax, emitterbaseSpeed;
    private float forceY, forceZ, forceRot, forceRotVelocity, forceRotSpeed, f_x, f_y, f_z, f_w;
    private float emitTimer;
    public bool p1_sw, p2_sw, reset;
    public int emitHeld;

    private bool noGesture;

    // Use this for initialization
    void Start()
    {
        resetValues();
    }

    void resetValues()
    {
        leftEdge = 32.0f;
        rightEdge = 50.0f;
        topEdge = 357.0f;
        bottomEdge = 354.0f;
        frontEdge = 54.0f;
        backEdge = 70.0f;

        forceRot = 360.0f;

        velocityMax = 1.0f;
        forceRotVelocity = 10.0f;

        moveX = Random.Range(-velocityMax, velocityMax);
        moveY = Random.Range(-velocityMax, velocityMax);
        moveZ = Random.Range(-velocityMax, velocityMax);

        forceY = Random.Range(-forceRotVelocity, forceRotVelocity);
        forceZ = Random.Range(-forceRotVelocity, forceRotVelocity);
        emitTimer = 1.0f;

        p1_sw = false;
        p2_sw = true;
    }

    // Update is called once per frame
    void Update()
    {
        noGesture = gc.NoGesture;

        if (gc.state == -1 || gc.state >= 4)
        {
            randomMovement();
            P_Emitter();
            forceRotate();
        }

        if(gc.HeadsetOn == 0 && !reset)
        {
            resetValues();
            reset = true;
        }
        else if (gc.HeadsetOn == 1 && reset)
        {
            reset = false;
        }

    }

    void forceRotate()
    {
        forceRotSpeed += Time.deltaTime;

        if (emitterForce.transform.rotation.y <= -forceRot)
        {
            forceY = Random.Range(0.0f, forceRotVelocity);
            forceRotSpeed = 0.0f;
        }
        if (emitterForce.transform.rotation.y >= forceRot)
        {
            forceY = Random.Range(forceRotVelocity, 0.0f);
            forceRotSpeed = 0.0f;
        }
        if (emitterForce.transform.rotation.z <= -forceRot)
        {
            forceZ = Random.Range(0.0f, forceRotVelocity);
            forceRotSpeed = 0.0f;
        }
        if (emitterForce.transform.rotation.z >= forceRot)
        {
            forceZ = Random.Range(forceRotVelocity, 0.0f);
            forceRotSpeed = 0.0f;
        }

        if (forceRotSpeed > 1.0f)
        {
            forceY = Random.Range(-forceRotVelocity, forceRotVelocity);
            forceZ = Random.Range(-forceRotVelocity, forceRotVelocity);
            forceRotSpeed = 0.0f;
        }

        f_x = emitterForce.transform.rotation.x;
        f_y = emitterForce.transform.rotation.y;
        f_z = emitterForce.transform.rotation.z;
        f_w = emitterForce.transform.rotation.w;

        emitterForce.transform.rotation = new Quaternion(f_x, f_y + forceY, f_z + forceZ, f_w);

    }

    void randomMovement()
    {
        emitterbaseSpeed += Time.deltaTime;

        if (projectileEmitter.transform.position.x <= leftEdge)
        {
            moveX = Random.Range(0.0f, velocityMax);
            emitterbaseSpeed = 0.0f;
        }

        if (projectileEmitter.transform.position.x >= rightEdge)
        {
            moveX = Random.Range(-velocityMax, 0.0f);
            emitterbaseSpeed = 0.0f;
        }

        if (projectileEmitter.transform.position.y <= bottomEdge)
        {
            moveY = Random.Range(0.0f, velocityMax);
            emitterbaseSpeed = 0.0f;
        }
        if (projectileEmitter.transform.position.y >= topEdge)
        {
            moveY = Random.Range(-velocityMax, 0.0f);
            emitterbaseSpeed = 0.0f;
        }
        if (projectileEmitter.transform.position.z <= frontEdge)
        {
            moveZ = Random.Range(0.0f, velocityMax);
            emitterbaseSpeed = 0.0f;
        }
        if (projectileEmitter.transform.position.z >= backEdge)
        {
            moveZ = Random.Range(-velocityMax, 0.0f);
            emitterbaseSpeed = 0.0f;
        }

        if (emitterbaseSpeed > 0.5f)
        {
            moveX = Random.Range(-velocityMax, velocityMax);
            moveY = Random.Range(-velocityMax, velocityMax);
            moveZ = Random.Range(-velocityMax, velocityMax);
            emitterbaseSpeed = 0.0f;
        }

        projectileEmitter.transform.position = new Vector3(projectileEmitter.transform.position.x + moveX, projectileEmitter.transform.position.y + moveY, projectileEmitter.transform.position.z + moveZ);

    }

    void P_Emitter()
    {
      //  if (Input.GetButton("Vertical"))
    //    {
            if (fs.isFocus)
        {

            if(emitTimer <= 0.0f)
            {
                emitHeld++;

                if(p1_sw)
                {
                    Temp_Projectile_Handler = Instantiate(projectile1, projectileEmitter.transform.position, projectileEmitter.transform.rotation) as GameObject;
                }
                else if (p2_sw)
                {
                    Temp_Projectile_Handler = Instantiate(projectile2, projectileEmitter.transform.position, projectileEmitter.transform.rotation) as GameObject;
                }

                emitTimer = 1.0f;
            }
            else if (emitTimer == 1.0f)
            {
                if (emitHeld == 0)
                {
                    if (p1_sw)
                    {
                        p1_sw = false;
                    }
                    else if (!p1_sw)
                    {
                        p1_sw = true;
                    }

                    if (p2_sw)
                    {
                        p2_sw = false;
                    }
                    else if (!p2_sw)
                    {
                        p2_sw = true;
                    }
                }

                if (p1_sw)
                {
                    Temp_Projectile_Handler = Instantiate(projectile1, projectileEmitter.transform.position, projectileEmitter.transform.rotation) as GameObject;
                }
                else if (p2_sw)
                {
                    Temp_Projectile_Handler = Instantiate(projectile2, projectileEmitter.transform.position, projectileEmitter.transform.rotation) as GameObject;
                }

                emitTimer -= Time.deltaTime;
            }
            else
            {
                emitTimer -= Time.deltaTime;
            }
          
        }
        else
        {
            emitHeld = 0;
            emitTimer = 1.0f;
        }
            Destroy(Temp_Projectile_Handler, 7.0f);
    }
}
