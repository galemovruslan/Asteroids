using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    public event Action DoneFlashing;

    [SerializeField] private GameObject _visuals;
    [Min(0.1f)]
    [SerializeField] private float _halfPeriod = 0.5f;

    private Timer _modulator;
    private Timer _duration;
    private bool _isOn = true;
    private Renderer[] _renderers;
    private bool _isFlashing;

    private void Awake()
    {
        _modulator = new Timer();
        _modulator.OnDone += _modulator_OnDone;

        _duration = new Timer();
        _duration.OnDone += _duration_OnDone;

        _renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void Flash(float duration)
    {
        _isFlashing = true;
        _duration.Restart(duration);
        _modulator.Restart(_halfPeriod);
    }

    private void SetVisibility(bool isOn)
    {
        _isOn = isOn;
        foreach (var renderer in _renderers)
        {
            renderer.enabled = isOn;
        }
    }

    private void ToggleVisuals()
    {
        _isOn = !_isOn;
        SetVisibility(_isOn);
    }
    private void _duration_OnDone()
    {
        _isFlashing = false;
        DoneFlashing?.Invoke();
    }

    private void _modulator_OnDone()
    {
        if (_isFlashing)
        {
            ToggleVisuals();
            _modulator.Restart(_halfPeriod);
        }
        else
        {
            SetVisibility(true);
        }
    }

}
