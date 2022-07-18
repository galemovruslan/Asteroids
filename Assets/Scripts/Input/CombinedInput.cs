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
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0);
    }

    public float GetRotation()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 toMouseDirection = mouseWorldPosition - _player.position;
        toMouseDirection.z = 0;
        toMouseDirection = toMouseDirection.normalized;

        float playerAngle = LimitRotation(_player.rotation.eulerAngles.z);
        float lookError = Mathf.Atan2(toMouseDirection.y, toMouseDirection.x) * Mathf.Rad2Deg - playerAngle;
        lookError = LimitRotation(lookError);
        float rotationCommand = -(lookError) * _turnSharpness;

        rotationCommand = Mathf.Clamp(rotationCommand, -1f, 1f);
        return rotationCommand;
    }

    public float GetThrust()
    {
        return Mathf.Min(1, Input.GetAxis("Vertical2") + Mathf.Max(0f, Input.GetAxis("Vertical")));
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

    public bool GetPause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
