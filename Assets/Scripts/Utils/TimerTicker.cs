using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PauseableObject))]
public class TimerTicker : Singleton<TimerTicker>, IPauseable
{
    [SerializeField ] private List<Timer> _timers;

    private bool _isPaused;

    private void Awake()
    {
        _timers = new List<Timer>();
    }

    public void AddTimer(Timer newTimer)
    {
        _timers.Add(newTimer);
    }

    private void Update()
    {
        if (_isPaused) { return; }

        foreach (var timer in _timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
    }
}
