using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneNN : MonoBehaviour
{
    public float maxPower = 5;
    public float maxTurnSpeed = 40;

    private float turnParameter;
    private float powerParameter;

    public Rigidbody2D rb;
    public Transform target;

    //INPUTS: Angle towards target, Drone Angle, Velocity x, velocity y, angular velocity. Target position. Target Distance. Drone position.
    private float distance;
    private float xDistance;
    private float yDistance;
    private float xVelocity;
    private float yVelocity;
    private float angularVelocity;
    //OUTPUTS: Thruster Add Force, Adding Torque

    public float[,] inputs = new float[6,1];
    public float[,] weights = new float[3, 6];
    public float[,] outputs =new float[2,1];
        /*
         [i1]
         [i2]
         [i3]
         [i4]
         [i5]
         [i6]
         */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
        
    {
        distance = Vector2.Distance(transform.position, target.position);
        xDistance = Mathf.Abs(transform.position.x - target.position.x);
        yDistance = Mathf.Abs(transform.position.y - target.position.y);
        xVelocity = rb.velocity.x;
        yVelocity = rb.velocity.y;
        angularVelocity = rb.angularVelocity;





        if (Input.GetKey(KeyCode.W))
        {
            thrusters(maxPower);
        }      
        if (Input.GetKey(KeyCode.A))
        {
            turn(maxTurnSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turn(-maxTurnSpeed);
        }
        

        


        //interpret(turn, thrust);
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
