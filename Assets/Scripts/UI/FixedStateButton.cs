using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FixedStateButton : MonoBehaviour
{
    public UnityEvent<bool> OnClick;

    [SerializeField] private Text _label;
    [SerializeField] private string _offStateLabel;
    [SerializeField] private string _onStateLabel;
    [SerializeField] private Toggle _toggle;

    private void Awake()
    {
        _toggle.onValueChanged.AddListener(OnToggleValueChange);
    }

    private void OnToggleValueChange(bool value)
    {
        _label.text = value ? _onStateLabel : _offStateLabel;
        OnClick?.Invoke(value);
    }
}
