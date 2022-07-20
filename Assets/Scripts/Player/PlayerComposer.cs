using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover)),
 RequireComponent(typeof(PlayerShooter)),
 RequireComponent(typeof(Flasher)),
 RequireComponent(typeof(SFXComposer))]
public class PlayerComposer : MonoBehaviour
{
    public event Action PlayerDestroyed;

    private PlayerMover _mover;
    private PlayerShooter _shooter;
    private Flasher _flasher;
    private SFXComposer _sfxComposer;
    private bool _isInvincible;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _mover.Thrusting += OnThrusting;
        
        _shooter = GetComponent<PlayerShooter>();
        _shooter.ShotMade += OnShotMade;
        
        _flasher = GetComponent<Flasher>();
        _flasher.DoneFlashing += EndInvincibility;
        
        _sfxComposer = GetComponent<SFXComposer>();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (_isInvincible) { return; }

        HandleDestroy();
    }

    public void SetInputScheme(IInputHandle input)
    {
        _mover.Initialize(input);
        _shooter.SetInputScheme(input);
    }

    public void ResetPlayer(Vector2 position)
    {
        _mover.ReserMover(position);
        _shooter.ResetShooter();
        _sfxComposer.Play(SFXComposer.ClipType.Spawn);
    }

    public void ActivateInvincibility(float duration)
    {
        _isInvincible = true;
        _flasher.Flash(duration);

    }
    private void EndInvincibility()
    {
        _isInvincible = false;
    }

    private void HandleDestroy()
    {
        PlayerDestroyed?.Invoke();
        _sfxComposer.Play(SFXComposer.ClipType.Destroy);
    }

    private void OnThrusting()
    {
        _sfxComposer.Play(SFXComposer.ClipType.Move);
    }
    private void OnShotMade()
    {
        _sfxComposer.Play(SFXComposer.ClipType.Shot);
    }
}
