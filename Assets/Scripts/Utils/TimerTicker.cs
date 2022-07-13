using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTicker : Singleton<TimerTicker>
{
    
    [SerializeField ] private List<Timer> _timers;

    private void Awake()
    {
        _timers = new List<Timer>();
    }

    public void AddTimer(Timer newTimer)
    {
        _timers.Add(newTimer);
    }

    void Update()
    {
        foreach (var timer in _timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }
}
