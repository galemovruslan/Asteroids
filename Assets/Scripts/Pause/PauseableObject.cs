using UnityEngine;

public class PauseableObject : MonoBehaviour
{
    IPauseable[] _pauseables;

    private void Awake()
    {
        _pauseables = GetComponents<IPauseable>();
    }

    public void SetPause(bool value)
    {
        if (_pauseables.Length == 0)
        {
            _pauseables = GetComponents<IPauseable>();
        }
        foreach (var item in _pauseables)
        {
            item.SetPause(value);
        }
    }

}
