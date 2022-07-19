using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class UfoShooter : MonoBehaviour
{
    [SerializeField] float _minFireCooldown = 2f;
    [SerializeField] float _maxFireCooldown = 5f;
    [SerializeField] Transform _firePoint;

    private PlayerComposer _player;
    private Weapon _weapon;
    private Timer _fireCoolDown;
    private bool _canFire;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerComposer>();
        _weapon = GetComponent<Weapon>();
        _fireCoolDown = new Timer();
        _fireCoolDown.OnDone += _fireCoolDown_OnDone;
    }

    public void PrepareToFire()
    {
        _canFire = false;
        BeginCooldown();
    }

    public void StopFire()
    {
        if(_fireCoolDown == null) { return; }

        _canFire = false;
        _fireCoolDown.Stop();
    }

    public void Fire()
    {
        if (!_canFire) { return; }

        AimWeapon();
        _weapon.Fire();

        BeginCooldown();
        _canFire = false;
    }

    private void AimWeapon()
    {
        Vector2 toPlayer = _player.transform.position - transform.position;
        float aimAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        _firePoint.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
    private void BeginCooldown()
    {
        float cdTime = Random.Range(_minFireCooldown, _maxFireCooldown);
        _fireCoolDown.Restart(cdTime);
    }

    private void _fireCoolDown_OnDone()
    {
        _canFire = true;
    }


}
