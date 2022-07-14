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
    [field: SerializeField] public AsteroidSize Size { get; private set; }

    private ObjectMover _mover;
    private float _angleSpeed;

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
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _mover.Initialize(position, direction.normalized * speed, angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerComposer>(out var player))
        {
            Destroyed(this,false);
            ReturnToPool();
        }
    }

}
