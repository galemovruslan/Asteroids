using System;

public class Timer
{
    public bool IsDone => _isDone;
    public bool IsRunning => _isRunning; 

    public event Action OnDone; 

    private float _time;
    private float _setTime;
    private bool _isRunning = false;
    private bool _isDone = false;

    public Timer()
    {
        TimerTicker.Instance.AddTimer(this);
    }


    public void Restart(float time)
    {
        _setTime = time;
        _time = 0;
        _isRunning = false;
        _isDone = false;
        _isRunning = true;
    }


    public void Tick(float deltaTime)
    {
        if (!_isRunning)
        {
            return;
        }

        _time += deltaTime;

        if(_time >= _setTime)
        {
            _isRunning = false;
            _isDone = true;
            OnDone?.Invoke();
        }
    }

}
