using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover))]
public class Ufo : MonoBehaviour
{
    public event Action Destroyed;

    [SerializeField] float _speed = 5f;

    private ObjectMover _mover;
    private Vector3 _respawnWaitPoint = new Vector3(100, 100, 0);

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        _mover.Move(Vector2.zero, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleDestruction();
    }

    public void Launch(Vector2 position, Vector2 direction)
    {
        gameObject.SetActive(true);
        _mover.Initialize(position, direction.normalized * _speed, 0f);
    }

    private void HandleDestruction()
    {
        Destroyed();
        gameObject.SetActive(false);
        transform.position = _respawnWaitPoint;
    }

}
