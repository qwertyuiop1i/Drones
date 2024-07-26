using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : MonoBehaviour
{
    public float maxPower = 5;
    public float maxTurnSpeed = 40;

    public Rigidbody2D rb;

    public Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //target= Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = target.position - (Vector2)transform.position;


        float distance = direction.magnitude;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = transform.rotation.eulerAngles.z;

        if (targetAngle - currentAngle > 0)
        {
            turnRight(maxTurnSpeed);
        }
        if (targetAngle - currentAngle < 0)
        {
            turnLeft(maxTurnSpeed);
        }


        if (Input.GetKey(KeyCode.D))
        {
            turnRight(maxTurnSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            turnLeft(maxTurnSpeed);

        }
        if (Input.GetKey(KeyCode.W))
        {
            thrusters(maxPower);
        }

    }

    public void turnRight(float am)
    {
        rb.AddTorque(-1 * am);
    }
    public void turnLeft(float am)
    {
        rb.AddTorque(am);
    }
    public void thrusters(float am)
    {
        rb.AddForce(transform.up * maxPower);
    }
}
