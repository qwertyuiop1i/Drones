using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneAI : MonoBehaviour
{
    public drone droneController;
    public Vector2 targetPosition;

    [SerializeField]
    private float angleP = 2f;
    [SerializeField]
    private float angleD = 0.5f;
    [SerializeField]
    private float distanceP = 0.5f;

    private float lastAngleError = 0f;

    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 direction = targetPosition - currentPosition;
        float distance = direction.magnitude;


        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = transform.rotation.eulerAngles.z;
        float angleError = Mathf.DeltaAngle(currentAngle, targetAngle);

        float rotationAmount = angleP * angleError + angleD * (angleError - lastAngleError);
        lastAngleError = angleError;

     
        if (rotationAmount > 0)
            droneController.turnLeft(Mathf.Min(rotationAmount, droneController.maxTurnSpeed));
        else
            droneController.turnRight(Mathf.Min(-rotationAmount, droneController.maxTurnSpeed));


        float thrustAmount = Mathf.Min(distance * distanceP, droneController.maxPower);
        droneController.thrusters(thrustAmount);
    }

    public void SetTarget(Vector2 target)
    {
        targetPosition = target;
    }
}
