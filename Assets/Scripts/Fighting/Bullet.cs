using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover))]
public class Bullet : PoolItem
{
    private ObjectMover _mover;
    private Timer _timer;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
        _timer = new Timer();
        _timer.OnDone += _timer_OnDone;
    }

    private void Update()
    {
        _mover.Move(Vector2.zero, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        _timer.Stop();
        ReturnToPool();
    }

    public void Launch(Vector2 position, Vector2 direction, float speed, float lifetime)
    {
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _mover.Initialize(position, direction.normalized * speed, angle);
        _timer.Restart(lifetime);
    }

    private void _timer_OnDone()
    {
        ReturnToPool();
    }


}
