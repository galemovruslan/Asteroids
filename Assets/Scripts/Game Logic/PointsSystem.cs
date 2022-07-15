using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem 
{
    public event Action<int> PointsAdded;

    private int _curentPoints;

    public void Add(int amount)
    {
        _curentPoints += amount;
        PointsAdded?.Invoke(_curentPoints);
    }
}
