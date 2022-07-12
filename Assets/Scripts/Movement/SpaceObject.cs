using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject
{
    public Vector2 Position { get; private set; }
    public float Angle { get; private set; }

    public SpaceObject(Vector2 position, float angle)
    {
        Position = position;
        Angle = angle;  
    }

    public void Move(Vector2 newPosition)
    {
        Position = newPosition;
    }

    public void Rotate(float newAngle)
    {
        Angle = newAngle;
    }

}
