using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXComposer : MonoBehaviour
{
    public event Action<AudioClip, float, bool> NeedPlay;

    [SerializeField] private List<SFXDefinition> _definitions;

    Dictionary<ClipType, PlayableSfx> _clipsMap;

    private void Awake()
    {
        _clipsMap = new Dictionary<ClipType, PlayableSfx>();

        foreach (var item in _definitions)
        {
            var newSfx = new PlayableSfx(item.Clip, item.Volume, item.IgnorePause);
            newSfx.NeedPlay += HandleNeedPlay;
            _clipsMap.Add(item.Type, newSfx);
        }
    }

    public void Play(ClipType type)
    {
        if (!_clipsMap.ContainsKey(type))
        {
            return;
        }
        _clipsMap[type].Play();
    }

    private void HandleNeedPlay(AudioClip clip, float volume, bool ignorePause)
    {
        NeedPlay?.Invoke(clip, volume, ignorePause);
    }

    public enum ClipType
    {
        Move,
        Shot,
        Destroy,
        Spawn
    }

    [System.Serializable]
    private class SFXDefinition
    {
        public AudioClip Clip;
        public ClipType Type;
        [Range(0, 1)]
        public float Volume;
        public bool IgnorePause;
    }
}
