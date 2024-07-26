using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneAI : MonoBehaviour
{
    public drone droneScript; // Reference to the drone script
    public Transform target; // Target position

    [SerializeField]
    public float pKp = 1f; // Proportional gain for position
    [SerializeField]
    public float pKi = 0.1f; // Integral gain for position
    [SerializeField]
    public float pKd = 0.01f; // Derivative gain position



    private float Perror, PpreviousError, Pintegral;

    [SerializeField]
    public float aKp = 1f; // Proportional gain for angle
    [SerializeField]
    public float aKi = 0.1f; // Integral gain for angle
    [SerializeField]
    public float aKd = 0.01f; // Derivative gain for angle

    private float Aerror, ApreviousError, Aintegral;


    void Start()
    {
        droneScript = GetComponent<drone>();
    }

    void Update()
    {
        
        // calc error
        Vector2 directionToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - transform.eulerAngles.z-90f;
        Debug.Log(angleToTarget);
        Aerror = angleToTarget;

        
        Aintegral += Aerror * Time.deltaTime;
        float derivative = (Aerror - ApreviousError) / Time.deltaTime;
        float output = aKp * Aerror + aKi * Aintegral + aKd * derivative;

        
        output = Mathf.Clamp(output, -droneScript.maxTurnSpeed, droneScript.maxTurnSpeed);

        // Apply control input for the angless
    //    if (output > 0)
   //     {
   //         droneScript.turnRight(output);
   //     }
   //     else
  //      {
  //          droneScript.turnLeft(-output);
   //     }

//        ApreviousError = Aerror;
    }
}
