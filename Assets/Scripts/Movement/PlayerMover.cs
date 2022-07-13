using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _thrustForce;
    [SerializeField] private float _rotationSpeed;

    private IInputHandle _input;
    private ObjectMover _mover;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
        _input = new CombinedInput(transform);
        //_input = new KeyboardInput();
    }

    private void Update()
    {
        float thrustCommand = _input.GetThrust();
        float rotationCommand = _input.GetRotation();
        bool attackCommand = _input.GetAttack();
        HandleControl(thrustCommand, rotationCommand);
        Debug.Log(attackCommand);
    }

    public void HandleControl(float thrustCommand, float rotationCommand)
    {
        var thrustVector = transform.right * thrustCommand * _thrustForce;
        var rotationSpeed = rotationCommand * _rotationSpeed;
        _mover.Move(thrustVector, rotationSpeed);
    }


}
