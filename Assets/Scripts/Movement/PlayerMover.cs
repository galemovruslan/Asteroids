using System;
using UnityEngine;

[RequireComponent(typeof(ObjectMover))]
public class PlayerMover : MonoBehaviour
{
    public event Action Thrusting;

    [SerializeField] private float _thrustForce;
    [SerializeField] private float _rotationSpeed;

    private IInputHandle _input;
    private ObjectMover _mover;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
    }

    private void Update()
    {
        float thrustCommand = _input.GetThrust();
        float rotationCommand = _input.GetRotation();
        HandleControl(thrustCommand, rotationCommand);
    }

    public void Initialize(IInputHandle inputHandle)
    {
        _input = inputHandle;
    }

    public void HandleControl(float thrustCommand, float rotationCommand)
    {
        Vector3 thrustVector = transform.right * thrustCommand * _thrustForce;
        float rotationSpeed = rotationCommand * _rotationSpeed;
        _mover.Move(thrustVector, rotationSpeed);

        if(thrustCommand > 0)
        {
            Thrusting?.Invoke();
        }
    }

    public void ReserMover(Vector2 resetPosition)
    {
        _mover.Initialize(resetPosition, Vector2.zero, 0);
    }

}
