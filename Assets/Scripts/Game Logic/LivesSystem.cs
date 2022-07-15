using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem
{
    public event Action<int> LiveTaken;

    private int _maxHealth;
    private int _currentHealth;

    public LivesSystem(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeLive()
    {
        _currentHealth--;
        LiveTaken?.Invoke(_currentHealth);
    }

}
