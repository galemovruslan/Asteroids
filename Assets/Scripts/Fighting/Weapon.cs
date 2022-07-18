using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _fireCooldown = 1f;
    [SerializeField] private Pool _bulletPool;

    private Timer _timer;
    private bool _canFire = true;

    private void Awake()
    {
        _timer = new Timer();
        _timer.OnDone += OnCooldown;
    }

    public void Fire()
    {
        if (!_canFire)
        {
            return;
        }

        Bullet newBullet = _bulletPool.GetItem() as Bullet;
        newBullet.Launch(_firePoint.position, _firePoint.right, _bulletSpeed);

        _canFire = false;
        _timer.Restart(_fireCooldown);
    }

    public void ResetWeapon()
    {
        _bulletPool.ResetContents();
        _canFire = true;
    }

    private void OnCooldown()
    {
        _canFire = true;
    }


}
