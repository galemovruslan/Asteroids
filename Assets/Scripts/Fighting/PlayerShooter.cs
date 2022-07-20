using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class PlayerShooter : MonoBehaviour, IPauseable
{
    public event Action ShotMade;

    private Weapon _weapon;
    private IInputHandle _input;
    private bool _isPaused = false;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        if (_isPaused) { return; }

        HandleShoot();
    }

    public void SetInputScheme(IInputHandle inputHandle)
    {
        _input = inputHandle;
    }

    public void ResetShooter()
    {
        _weapon.ResetWeapon();
    }

    private void HandleShoot()
    {
        bool shootCommand = _input.GetAttack();
        if (!shootCommand)
        {
            return;
        }

        bool fired = _weapon.TryFire();
        if (fired)
        {
            ShotMade?.Invoke();
        }
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
    }
}
