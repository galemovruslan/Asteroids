using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem 
{
    public event Action<int> PointsAdded;

    public int Current => _curentPoints;

    private int _curentPoints;

    public PointsSystem(int startValue)
    {
        _curentPoints = startValue;
    }

    public void Reset()
    {
        _curentPoints = 0;
        PointsAdded?.Invoke(_curentPoints);
    }

    public void Add(int amount)
    {
        _curentPoints += amount;
        PointsAdded?.Invoke(_curentPoints);
    }
}
