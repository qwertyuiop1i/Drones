using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : MonoBehaviour
{
    public float scoring;

    public float maxPower = 5;
    public float maxTurnSpeed = 40;

    public Rigidbody2D rb;
    public Transform target;

    // PID parameters for linear velocity
    public float kpLinear = 1f;
    public float kiLinear = 0.1f;
    public float kdLinear = 0.01f;
    private float errorLinear, integralLinear, derivativeLinear;

    // PID parameters for angular velocity
    public float kpAngular = 1f;
    public float kiAngular = 0.1f;
    public float kdAngular = 0.01f;
    private float errorAngular, integralAngular, derivativeAngular;

    private float previousErrorLinear, previousErrorAngular;


    public float maxedOvershoot = 0f;
    public bool hasCrossedTarget = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        maxedOvershoot = 0f;
  
        hasCrossedTarget = false;
        
}

    void Update()
    {
        //errors
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = transform.rotation.eulerAngles.z;

        errorLinear = distance;
        integralLinear += errorLinear * Time.deltaTime;
        derivativeLinear = (errorLinear - previousErrorLinear) / Time.deltaTime;
        previousErrorLinear = errorLinear;

        errorAngular = Mathf.DeltaAngle(currentAngle, targetAngle);
        integralAngular += errorAngular * Time.deltaTime;
        derivativeAngular = (errorAngular - previousErrorAngular) / Time.deltaTime;
        previousErrorAngular = errorAngular;



        //outpouts calc
        float linearOutput = kpLinear * errorLinear + kiLinear * integralLinear + kdLinear * derivativeLinear;
        float angularOutput = kpAngular * errorAngular + kiAngular * integralAngular + kdAngular * derivativeAngular;

        // clamp
        linearOutput = Mathf.Clamp(linearOutput, 0, maxPower);
        angularOutput = Mathf.Clamp(angularOutput, -maxTurnSpeed, maxTurnSpeed);

        // apply
        thrusters(linearOutput);
        turn(angularOutput);

        if (Mathf.Sign(errorAngular)==-1)
        {
            hasCrossedTarget = true;
        }

        if (hasCrossedTarget)
        {
            if (Mathf.Abs(errorAngular) > maxedOvershoot)
            {
                maxedOvershoot = Mathf.Abs(errorAngular);
            }
        }
        




    }

    void turn(float am)
    {
        rb.AddTorque(am);
    }

    void thrusters(float am)
    {
        rb.AddForce(transform.up * am);
    }


}
