using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMover))]
public class Ufo : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    private ObjectMover _mover;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
    }

    private void Update()
    {
        _mover.Move(Vector2.zero, 0f);
    }

    public void Initialize(Vector2 position)
    {
        _mover.Initialize(position, Vector2.zero, 0f);
    }
   
    public void Launch(Vector2 position, Vector2 direction)
    {
        _mover.Initialize(position, direction.normalized * _speed , 0f);
    }

    [ContextMenu("Launch")]
    public void TestLaunch()
    {
        Launch(Vector2.zero, Vector2.right);
    }
}
