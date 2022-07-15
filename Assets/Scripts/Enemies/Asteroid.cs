using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover))]
public class Asteroid : PoolItem
{
    public enum AsteroidSize
    {
        Small,
        Medium,
        Big
    }

    public event Action<Asteroid, bool> Destroyed;

    public Vector2 Velocity => _velocity;
    public Vector2 Position => transform.position;
    [field: SerializeField] public AsteroidSize Size { get; private set; }

    private ObjectMover _mover;
    private float _angleSpeed;
    private Vector2 _velocity;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
    }

    private void Update()
    {
        _mover.Move(Vector2.zero, _angleSpeed);
    }

    public void Launch(Vector2 position, Vector2 direction, float speed, float angleSpeed)
    {
        _angleSpeed = angleSpeed;
        _velocity = direction.normalized * speed;
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _mover.Initialize(position, _velocity, angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bullet>(out var bullet))
        {
            bool spawnNext = true;
            GetHit(spawnNext);
        }
        else
        {
            bool spawnNext = false;
            GetHit(spawnNext);
        }

    }

    private void GetHit(bool spawnNext)
    {
        Destroyed?.Invoke(this, spawnNext);
        ReturnToPool();
    }
}
