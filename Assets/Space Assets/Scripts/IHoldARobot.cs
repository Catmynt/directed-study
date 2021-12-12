using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHoldARobot : MonoBehaviour
{
    public GameObject robot;

    public void left()
    {
        robot.GetComponent<RobotBrain>().turnLeft();
    }

    public void right()
    {
        robot.GetComponent<RobotBrain>().turnRight();
    }

    public void move()
    {
        robot.GetComponent<RobotBrain>().moveForward();
    }
}
