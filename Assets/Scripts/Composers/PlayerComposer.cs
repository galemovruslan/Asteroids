using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover)),
 RequireComponent(typeof(PlayerShooter))]
public class PlayerComposer : MonoBehaviour
{
    public event Action PlayerDestroyed;

    private PlayerMover _mover;
    private PlayerShooter _shooter;

    private IInputHandle _keyboardInput;
    private IInputHandle _combinedInput;

    private void Awake()
    {
        _keyboardInput = new KeyboardInput();
        _combinedInput = new CombinedInput(transform);

        _mover = GetComponent<PlayerMover>();
        _shooter = GetComponent<PlayerShooter>();

        SetInputScheme(_keyboardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleDestroy();
    }

    public void SetInputScheme(IInputHandle input)
    {
        _mover.Initialize(input);
        _shooter.Initialize(input);
    }

    private void HandleDestroy()
    {
        PlayerDestroyed?.Invoke();
    }
}
