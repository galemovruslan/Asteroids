
using UnityEngine;

public class KeyboardInput : IInputHandle
{
    public bool GetAttack()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetPause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
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
