using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesSystem
{
    public event Action<int> LiveTaken;

    public int Current => _currentHealth;

    private int _maxHealth;
    private int _currentHealth;

    public LifesSystem(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        LiveTaken?.Invoke(_currentHealth);
    }

    public void TakeLive()
    {
        _currentHealth--;
        LiveTaken?.Invoke(_currentHealth);
    }

}
