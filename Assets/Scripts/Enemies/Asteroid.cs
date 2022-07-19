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

    public event Action<Asteroid, bool, int> Destroyed;

    public Vector2 Velocity => _velocity;
    public Vector2 Position => transform.position;

    [field: SerializeField] public AsteroidSize Size { get; private set; }
    [SerializeField] private int _givesPoint;

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
        bool spawnNext = other.TryGetComponent<Bullet>(out var bullet);
        int points = 0;

        if( (spawnNext && bullet.gameObject.layer == LayerMask.NameToLayer("Player Bullet")) ||
            (other.TryGetComponent<PlayerComposer>(out var player)))
        {
            points = _givesPoint;
        }

        GetHit(spawnNext, points);
    }

    private void GetHit(bool spawnNext, int points)
    {
        Destroyed?.Invoke(this, spawnNext, points);
        ReturnToPool();
    }
}
