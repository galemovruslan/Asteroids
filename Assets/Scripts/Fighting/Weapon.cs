using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _fireCooldown = 1f;

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

        Bullet newBullet = Instantiate<Bullet>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        newBullet.Launch(_firePoint.position, _firePoint.right, _bulletSpeed);

        _canFire = false;
        _timer.Restart(_fireCooldown);
    }
    private void OnCooldown()
    {
        _canFire = true;
        Debug.Log("Can fire");
    }


}