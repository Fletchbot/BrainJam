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
    public GameObject screenBoxColliders;

    private GameObject Temp_Projectile_Handler;

    private float leftEdge, rightEdge, topEdge, bottomEdge, backEdge, frontEdge;

    private float moveX, moveY, moveZ, velocityMax, emitterbaseSpeed;
    private float emissionRate, emitMax, emitMin;
    private float forceY, forceZ, forceRot, forceRotVelocity, forceRotSpeed, f_x, f_y, f_z, f_w;

    private float randomProjectile;

    private bool enableCursor, noGesture;

    // Use this for initialization
    void Start()
    {

        leftEdge = 32.0f;
        rightEdge = 50.0f;
        topEdge = 357.0f;
        bottomEdge = 354.0f;
        frontEdge = 54.0f;
        backEdge = 70.0f;

        forceRot = 360.0f;

        velocityMax = 1.0f;
        emitMin = 0.5f;
        emitMax = 3.0f;
        forceRotVelocity = 10.0f;

        moveX = Random.Range(-velocityMax, velocityMax);
        moveY = Random.Range(-velocityMax, velocityMax);
        moveZ = Random.Range(-velocityMax, velocityMax);

        forceY = Random.Range(-forceRotVelocity, forceRotVelocity);
        forceZ = Random.Range(-forceRotVelocity, forceRotVelocity);

        emissionRate = Random.Range(emitMin, emitMax);

        randomProjectile = Random.Range(-1.0f, 1.0f);


    }

    // Update is called once per frame
    void Update()
    {
        noGesture = gc.NoGesture;

        if (gc.state == -1 && !noGesture)
        {
            if (!enableCursor)
            {
                screenBoxColliders.GetComponent<ActivateObjects>().SetActive(true);
                enableCursor = true;
            }

            randomMovement();
            randomEmitter();
            forceRotate();
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

    void randomEmitter()
    {
        emissionRate -= Time.deltaTime;

        if (emissionRate <= 0.0f)
        {
            if(randomProjectile >= 0.0f)
            {
                Temp_Projectile_Handler = Instantiate(projectile1, projectileEmitter.transform.position, projectileEmitter.transform.rotation) as GameObject;
                randomProjectile = Random.Range(-1, 1);
            }
            else
            {
                Temp_Projectile_Handler = Instantiate(projectile2, projectileEmitter.transform.position, projectileEmitter.transform.rotation) as GameObject;
                randomProjectile = Random.Range(-1, 1);
            }

            emissionRate = Random.Range(emitMin, emitMax);
        }
            Destroy(Temp_Projectile_Handler, 7.0f);
    }
}
