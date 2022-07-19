using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover)),
 RequireComponent(typeof(UfoShooter))]
public class Ufo : MonoBehaviour
{
    public event Action Destroyed;

    [SerializeField] private float _onScreenTime = 10f;

    private float _speed = 5f;
    private ObjectMover _mover;
    private UfoShooter _shooter;
    private Vector3 _respawnWaitPoint = new Vector3(100, 100, 0);

    private void Awake()
    {
        _speed = Constants.ScreenRect.width / _onScreenTime;
        _mover = GetComponent<ObjectMover>();
        _shooter = GetComponent<UfoShooter>();
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        _mover.Move(Vector2.zero, 0f);
        _shooter.Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleDestruction();
    }

    public void Launch(Vector2 position, Vector2 direction)
    {
        gameObject.SetActive(true);
        _mover.Initialize(position, direction.normalized * _speed, 0f);
        _shooter.PrepareToFire();
    }

    public void WaitForLaunch()
    {
        //gameObject.SetActive(false);
        transform.position = _respawnWaitPoint;
        _mover.Initialize(_respawnWaitPoint, Vector2.zero, 0);
        _shooter.StopFire();
    }

    public void ForceDestroy()
    {
        HandleDestruction();
    }

    private void HandleDestruction()
    {
        Destroyed?.Invoke();
        WaitForLaunch();
    }

}
