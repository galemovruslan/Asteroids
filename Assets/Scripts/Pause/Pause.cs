using System;
using UnityEngine;

public class Pause
{
    public event Action<bool> PauseSet;
    public bool IsPaused { get; private set; }

    private PauseableObject[] _pauseables;

    public void SetPause(bool value)
    {
        if (value == IsPaused) { return; }

        IsPaused = value;
        _pauseables = GameObject.FindObjectsOfType<PauseableObject>();


        foreach (var item in _pauseables)
        {
            item.SetPause(IsPaused);
        }
        PauseSet?.Invoke(IsPaused);
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        SetPause(IsPaused);
    }
}
