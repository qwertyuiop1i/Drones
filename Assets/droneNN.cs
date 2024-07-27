using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneNN : MonoBehaviour
{
    [Header("Should train?")]
    public bool shouldFly = true;

    public float maxPower = 5;
    public float maxTurnSpeed = 40;
    public float mutationAm = 1f;
    public float turnParameter;
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
    [Header("0 : xDistance, 1 : yDistance, 2 : xVelocity, 3 : yVelocity, 4 : angularVelocity, 5 : cosAngle, 6: sinAngle")]
    public float[] weights1 = new float[7];
    public float[] weights2 = new float[7];
    //public float[] biases1 = new float[7];
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
        shouldFly = true;
        rb = GetComponent<Rigidbody2D>();

        if (gameObject.name != "WINNER")
        {
            for (int i = 0; i < weights1.Length; i++)
            {
                weights1[i] += Random.Range(-mutationAm, mutationAm);
                weights2[i] += Random.Range(-mutationAm, mutationAm);
                //biases1[i] += Random.Range(-mutationAm, mutationAm);
                biases2[i] += Random.Range(-mutationAm, mutationAm);
            }
        }


        distance = 0f;
        
    }

    
    void Update()
        
    {

        if (shouldFly)
        {

            turnParameter = 0f;
            powerParameter = 0f;
            //distance = Vector2.Distance(transform.position, target.position);
            xDistance = (transform.position.x - target.position.x);
            yDistance = (transform.position.y - target.position.y);
            xVelocity = rb.velocity.x;
            yVelocity = rb.velocity.y;
            angularVelocity = rb.angularVelocity;
            cosAngle = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            sinAngle = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
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
                turnParameter += inputs[i] * weights1[i];//biases1[i] + 
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
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position = new Vector2(0, 0);
                transform.rotation = Quaternion.identity;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }




            interpret(turnParameter, powerParameter);

        }
    }

    public void interpret(float turning, float thrust)
    {
       // Debug.Log("Interprettting a thrust of " + thrust);
        if (thrust > 0)
        {
            Debug.Log(thrust>0); 
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
        rb.AddTorque(am,ForceMode2D.Force);
    }

    void thrusters(float am)
    {
        rb.AddForce(transform.up * am,ForceMode2D.Force);
    }
    
    
}
