using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover)),
 RequireComponent(typeof(PlayerShooter))]
public class PlayerComposer : MonoBehaviour
{
    public event Action PlayerDestroyed;

    private PlayerMover _mover;
    private PlayerShooter _shooter;

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

    public void ResetPlayer()
    {
        _shooter.ResetShooter();
    }

    private void HandleDestroy()
    {
        PlayerDestroyed?.Invoke();
    }
}
