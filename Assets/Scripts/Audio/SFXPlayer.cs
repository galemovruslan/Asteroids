using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour, IPauseable
{
    [SerializeField] AudioSource _audioSource;
    [Range(0f,1f)]
    [SerializeField] float _masterVolume = 0.5f;

    private SFXComposer[] _composers;
    private bool _isPaused;

    private void Start()
    {
        _composers = FindObjectsOfType<SFXComposer>(includeInactive: true);
        foreach (var playable in _composers)
        {
            playable.NeedPlay += OnNeedPlay;
        }
    }

    private void OnNeedPlay(AudioClip clip, float volume, bool ignorePause)
    {
        if (_isPaused && !ignorePause) { return; }

        _audioSource.PlayOneShot(clip, _masterVolume * volume);
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
    }
}
