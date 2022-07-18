using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIndicator : IntegerIndicator
{
    [SerializeField] private Text _textField;
    [SerializeField] private string _prefix;
    public override void SetValue(int value)
    {
        _textField.text = $"{_prefix} {value}";
    }
}
