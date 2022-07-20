using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableSfx
{
    public event Action<AudioClip, float, bool> NeedPlay;

    private AudioClip _clip;
    private float _volume;
    private bool _ignorePause;

    private float _startPlayTime;
    private float _endPlayTime;
    private bool _isPlaying = false;

    public PlayableSfx(AudioClip clip, float volume, bool ignorePause)
    {
        _clip = clip;
        _volume = volume;
        _ignorePause = ignorePause;
    }

    public void Play()
    {
        _isPlaying = Time.time < _endPlayTime;

        if (_isPlaying)
        {
            return;
        }
        _startPlayTime = Time.time;
        _endPlayTime = _startPlayTime + _clip.length;
        NeedPlay?.Invoke(_clip, _volume, _ignorePause);
    }


}
