using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover))]
public class Bullet : PoolItem
{
    private ObjectMover _mover;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
    }

    private void Update()
    {
        _mover.Move(Vector2.zero, 0f);
    }

    public void Launch(Vector2 position, Vector2 direction, float speed)
    {
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _mover.Initialize(position, direction.normalized * speed, angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ScreenBoundary>(out var boundary))
        {
            ReturnToPool();
        }
    }

}
