using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBrain : MonoBehaviour
{
    public GameObject window;

    public float rotationSpeed;

    public float moveSpeed;

    public bool willTurnRight;

    public bool willTurnLeft;

    public bool move;

    private bool isTurningRight;

    private List<List<GameObject>> cells;

    void Start()
    {
        //xPos = transform.rotation.x;
        //cells = window.GetComponent<WindowGridGen>().cells;
    }

    public void Update()
    {
        if (move)
        {
            move = false;
            moveForward();
        }

        if (willTurnRight)
        {
            willTurnRight = false;
            turnRight();
        }

        if (willTurnLeft)
        {
            willTurnLeft = false;
            turnLeft();
        }
    }

    public void turnRight()
    {
        //yPos = transform.eulerAngles.x;
        // float value = 90f;
        // float newAngle =
        //     transform.rotation.x + value > 360f
        //         ? transform.rotation.x + value - 360f
        //         : transform.rotation.x + value;
        transform.Rotate(0f, 90f, 0f);

        //StartCoroutine(turnBotRight());
    }

    public void turnLeft()
    {
        //float value = 90f;
        // float newAngle =
        //     transform.rotation.x - value < 0
        //         ? transform.rotation.x - value + 360
        //         : transform.rotation.x - value;
        transform.Rotate(0f, -90f, 0f);
    }

    public void moveForward()
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos =
            this.transform.position +
            transform.right *
            gameObject.GetComponent<RobotScale>().getCellSize();
        print(gameObject.GetComponent<RobotScale>().getCellSize());

        // StartCoroutine(onward(startPos, endPos, moveSpeed));
        transform.position = endPos;
    }

    public Vector3 moveLerp(Vector3 a, Vector3 b, float t)
    {
        t = Mathf.Clamp(t, 0, 1);

        var distance = new Vector3(b.x - a.x, b.y - a.y, b.z - a.z);

        float x = a.x + distance.x * t;
        float y = a.y + distance.y * t;
        float z = a.z + distance.z * t;

        return new Vector3(x, y, z);
    }

    // IEnumerator onward(Vector3 startPos, Vector3 endPos, float moveSpeed)
    // {
    //     print("entered onward");
    //     while (transform.position != endPos)
    //     {
    //         transform.position =
    //             moveLerp(startPos, endPos, Time.deltaTime * moveSpeed);
    //         yield return null;
    //     }
    //     yield return null;
    // }

    //IEnumerator turnBotRight()
    //{
    //     Vector3 desiredRotation = new Vector3(xPos, 180f, 90f);
    //     while (transform.rotation != desiredRotation)
    //     {
    //         //print((float) Math.Abs(xPos - transform.rotation.x));
    //         if ((float) Math.Abs(xPos - transform.rotation.x) < 2f)
    //         {
    //             transform.rotation = new Quaternion(xPos, 180f, 90f, 0);
    //             break;
    //         }
    //         else
    //         {
    //             print("rotating");
    //             transform.rotation =
    //                 Quaternion
    //                     .Lerp(transform.rotation,
    //                     Quaternion.Euler(xPos, 180f, 90f),
    //                     Time.deltaTime);
    //         }
    //         yield return null;
    //     }
    //     print("done rotating");
    //     yield return null;
    // }
    //     Vector3 currentRotationVector = transform.rotation.eulerAngles;
    //     Vector3 desiredRotation = currentRotationVector;
    //     desiredRotation.x += 90f;

    //     while (currentRotationVector != desiredRotation)
    //     {
    //         print("currentRotationVector: " + currentRotationVector);
    //         print("desiredRotation: " + desiredRotation);
    //         transform.Rotate(Vector3.up * rotationSpeed);
    //         currentRotationVector = transform.rotation.eulerAngles;
    //         yield return null;
    //     }
    //     yield return null;
    // }
}
