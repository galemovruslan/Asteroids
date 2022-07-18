using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconRowIndicator : IntegerIndicator
{

    [SerializeField] private GameObject _icon;

    private int _level;
    private List<GameObject> _iconList = new List<GameObject>();

      public override void SetValue(int newLevel)
    {
        if (_level > newLevel)
        {
            ClearRow();
        }
        FillRow(newLevel);
        _level = newLevel;
    }

    private void FillRow(int newLevel)
    {
        int levelDiff = newLevel - _level;

        for (int i = 0; i < levelDiff; i++)
        {
            GameObject newIcon = Instantiate(_icon, transform);
            _iconList.Add(newIcon);
        }
    }

    private void ClearRow()
    {
        foreach (var icon in _iconList)
        {
            Destroy(icon);
        }
        _iconList.Clear();
        _level = 0;
    }
}
