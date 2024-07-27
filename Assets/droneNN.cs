using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneNN : MonoBehaviour
{
    public float maxPower = 5;
    public float maxTurnSpeed = 40;

    private float turnParameter;
    public float powerParameter;

    public Rigidbody2D rb;
    public Transform target;
    public float distance;
    //INPUTS: Angle towards target, Drone Angle, Velocity x, velocity y, angular velocity. Target position. Target Distance. Drone position.
    //private float distance;
    private float xDistance;
    private float yDistance;
    private float xVelocity;
    private float yVelocity;
    private float angularVelocity;
    private float cosAngle;
    private float sinAngle;
    //OUTPUTS: Thruster Add Force, Adding Torque

    public float[] inputs = new float[7];
    public float[] weights1 = new float[7];
    public float[] weights2 = new float[7];
    public float[] biases1 = new float[7];
    public float[] biases2 = new float[7];
    public float[] outputs =new float[2];
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

        if (gameObject.name != "WINNER")
        {
            for (int i = 0; i < weights1.Length; i++)
            {
                weights1[i] += Random.Range(-1f, 1f);
                weights2[i] += Random.Range(-1f, 1f);
                biases1[i] += Random.Range(-1f, 1f);
                biases2[i] += Random.Range(-1f, 1f);
            }
        }


        distance = 0f;
        
    }

    
    void Update()
        
    {
        

        turnParameter = 0f;
        powerParameter = 0f;
        //distance = Vector2.Distance(transform.position, target.position);
        xDistance = Mathf.Abs(transform.position.x - target.position.x);
        yDistance = Mathf.Abs(transform.position.y - target.position.y);
        xVelocity = rb.velocity.x;
        yVelocity = rb.velocity.y;
        angularVelocity = rb.angularVelocity;
        cosAngle = Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad);
        sinAngle = Mathf.Sin(transform.eulerAngles.z*Mathf.Deg2Rad);
        distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

        inputs[0] = xDistance;
        inputs[1] = yDistance;
        inputs[2] = xVelocity;
        inputs[3] = yVelocity;
        inputs[4] = angularVelocity;
        inputs[5] = cosAngle;
        inputs[6] = sinAngle;


        for (int i = 0; i < inputs.Length; i++)
        {
            turnParameter += biases1[i] + inputs[i] * weights1[i];
        }
        outputs[0] = turnParameter;

        for (int i = 0; i < inputs.Length; i++)
        {
            powerParameter += biases2[i] + inputs[i] * weights2[i];
        }
        outputs[1] = powerParameter;


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
        

        


        interpret(turnParameter, powerParameter);
    }

    public void interpret(float turning, float thrust)
    {
        if (Mathf.Round(thrust) == 1)
        {
            Debug.Log("burst"); 
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
