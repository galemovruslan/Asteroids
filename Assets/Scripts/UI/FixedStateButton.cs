using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SFXComposer))]
public class FixedStateButton : MonoBehaviour
{
    public UnityEvent<bool> OnClick;

    [SerializeField] private Text _label;
    [SerializeField] private string _offStateLabel;
    [SerializeField] private string _onStateLabel;
    [SerializeField] private Toggle _toggle;

    private SFXComposer _sfxComposer;

    private void Awake()
    {
        _toggle.onValueChanged.AddListener(OnToggleValueChange);
        _sfxComposer = GetComponent<SFXComposer>();
    }

    private void OnToggleValueChange(bool value)
    {
        _label.text = value ? _onStateLabel : _offStateLabel;
        OnClick?.Invoke(value);
        _sfxComposer.Play(SFXComposer.ClipType.Shot);
    }
}
