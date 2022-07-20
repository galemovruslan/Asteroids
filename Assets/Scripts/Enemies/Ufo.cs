using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover)),
 RequireComponent(typeof(UfoShooter)), 
 RequireComponent(typeof(SFXComposer))]
public class Ufo : MonoBehaviour
{
    public event Action<int> Destroyed;

    [SerializeField] private float _onScreenTime = 10f;
    [SerializeField] private int _givesPoints;

    private float _speed = 5f;
    private ObjectMover _mover;
    private UfoShooter _shooter;
    private SFXComposer _sfxComposer;
    private Vector3 _respawnWaitPoint = new Vector3(100, 100, 0);
    private bool _isOnScreen;

    private void Awake()
    {
        _speed = Constants.ScreenRect.width / _onScreenTime;
        _mover = GetComponent<ObjectMover>();
        _shooter = GetComponent<UfoShooter>();
        _sfxComposer = GetComponent<SFXComposer>();
    }

    private void Update()
    {
        _mover.Move(Vector2.zero, 0f);
        bool fired = _shooter.TryFire();
        if (fired)
        {
            _sfxComposer.Play(SFXComposer.ClipType.Shot);
        }
        if (_isOnScreen)
        {
            _sfxComposer.Play(SFXComposer.ClipType.Move);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int points = 0;
        if(other.TryGetComponent<PlayerComposer>(out var composer)||
            other.TryGetComponent<Bullet>(out var bullet))
        {
            points = 50;
        }
        HandleDestruction(points);
        if(other.TryGetComponent<ScreenBoundary>(out var boundary))
        {
            return;
        }
        _sfxComposer.Play(SFXComposer.ClipType.Destroy);
    }

    public void Launch(Vector2 position, Vector2 direction)
    {
        _mover.Initialize(position, direction.normalized * _speed, 0f);
        _shooter.PrepareToFire();
        _isOnScreen = true;
    }

    public void WaitForLaunch()
    {
        transform.position = _respawnWaitPoint;
        _mover.Initialize(_respawnWaitPoint, Vector2.zero, 0);
        _shooter.StopFire();
    }

    public void ForceDestroy()
    {
        _shooter.ResetShooter();
        HandleDestruction(0);
    }

    private void HandleDestruction(int points)
    {
        Destroyed?.Invoke(points);
        WaitForLaunch();
        _isOnScreen = false;
    }

}
