
using UnityEngine;

public class KeyboardInput : IInputHandle
{
    public bool GetAttack()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public float GetRotation()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetThrust()
    {
        return Mathf.Max(0f, Input.GetAxis("Vertical"));
    }
}
