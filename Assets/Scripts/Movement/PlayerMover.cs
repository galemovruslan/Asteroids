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
    }

    private void Update()
    {
        float thrust = _input.GetThrust();
        float rotation = _input.GetRotation();
        HandleControl(thrust, rotation);
    }

    public void HandleControl(float thrustCommand, float rotationCommand)
    {
        var thrustVector = transform.right * thrustCommand * _thrustForce;
        var rotationSpeed = rotationCommand * _rotationSpeed;
        _mover.Move(thrustVector, rotationSpeed);
    }

    // Вектор направления движения обьекта
    private Vector2 MoveVector()
    {
        Vector2 direction = Vector2.right;
        float angleRadians = _mover.Angle * Mathf.Deg2Rad;
        // Применение матрицы вращения (на угол _object.Angle) на вектор с нулевым поворотом
        direction.x = direction.x * Mathf.Cos(angleRadians) - direction.y * Mathf.Sin(angleRadians);
        direction.y = direction.x * Mathf.Sin(angleRadians) + direction.y * Mathf.Cos(angleRadians);
        return direction;
    }



}
