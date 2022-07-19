using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover)),
 RequireComponent(typeof(PlayerShooter)),
 RequireComponent(typeof(Flasher))]
public class PlayerComposer : MonoBehaviour
{
    public event Action PlayerDestroyed;

    private PlayerMover _mover;
    private PlayerShooter _shooter;
    private Flasher _flasher;
    private bool _isInvincible;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _shooter = GetComponent<PlayerShooter>();
    }

    private void OnTriggerEnter(Collider other)
    {
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
    }

    public void ActivateInvincibility()
    {

    }

    private void HandleDestroy()
    {
        PlayerDestroyed?.Invoke();
    }

    private void SetInvincible(float seconds)
    {

    }

}
