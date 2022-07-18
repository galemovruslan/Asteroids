using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class PlayerShooter : MonoBehaviour
{

    private Weapon _weapon;
    private IInputHandle _input;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
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
        if (shootCommand)
        {
            _weapon.Fire();
        }
    }
}
