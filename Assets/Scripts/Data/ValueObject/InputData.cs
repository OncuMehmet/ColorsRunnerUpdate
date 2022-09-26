using UnityEngine;
using System;

[Serializable]
public class InputData 
{
    public float RunnerHorizontalInputSpeed = 3f;
    public float RunnerForwardInputSpeed = 7f;
    public Vector2 RunnerClampSides = new Vector2(-3, 3);
    public float RunnerClampSpeed = 0.007f;
    public float IdleInputSpeed = 1.15f;
}
