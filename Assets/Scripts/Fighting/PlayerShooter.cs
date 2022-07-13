using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class PlayerShooter : MonoBehaviour
{

    private Weapon _weapon;
    private IInputHandle _input;

    private void Awake()
    {
        _input = new KeyboardInput();
        _weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        bool shootCommand = _input.GetAttack();
        if (shootCommand)
        {
            _weapon.Fire();
        }
    }
}
