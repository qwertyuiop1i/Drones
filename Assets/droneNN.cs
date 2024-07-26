using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneNN : MonoBehaviour
{
    public float maxPower = 5;
    public float maxTurnSpeed = 40;

    public Rigidbody2D rb;
    public Transform target;

    //INPUTS: Angle towards target, Drone Angle, Velocity x, velocity y, angular velocity. Target position. Target Distance. Drone position.
    private float distance;

    //OUTPUTS: Thruster Add Force, Adding Torque
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.position);
        
        
    }

    public void interpret(float turning, float thrust)
    {
        if (Mathf.Round(thrust) == 1)
        {
            thrusters(maxPower);
        }
        if (turning > 0.2f)
        {
            turn(maxTurnSpeed);
        }
        if (turning < -0.2f)
        {
            turn(-maxTurnSpeed);
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
