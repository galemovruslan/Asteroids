using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedInput : IInputHandle
{
    private Transform _player;
    private readonly float _turnSharpness = 0.1f;

    public CombinedInput(Transform player)
    {
        _player = player;
    }

    public bool GetAttack()
    {
        return false;
    }

    public float GetRotation()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 toLookDirection = mouseWorldPosition - _player.position;
        toLookDirection.z = 0;
        toLookDirection = toLookDirection.normalized;

        float playerAngle = LimitRotation(_player.rotation.eulerAngles.z);
        float lookAngle = Mathf.Atan2(toLookDirection.y, toLookDirection.x) * Mathf.Rad2Deg ;
        lookAngle = LimitRotation(lookAngle);
        float rotationCommand = -(lookAngle - playerAngle) * _turnSharpness;
        rotationCommand = Mathf.Clamp(rotationCommand, -1f, 1f);
        return rotationCommand;
    }

    public float GetThrust()
    {
        return 0;
    }

    public float LimitRotation(float rotation)
    {
        rotation %= 360f;
        if (Mathf.Abs(rotation) > 180f)
        {
            if (rotation < 0)
            {
                rotation += 360f;
            }
            else
            {
                rotation -= 360f;
            }
        }
        return rotation;
    }
}
