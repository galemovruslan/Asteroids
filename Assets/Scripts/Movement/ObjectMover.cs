using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float Angle => _object.Angle;

    [SerializeField] private float _maxSpeed;
    [SerializeField] private bool _screenBoundsMirroring = true;

    private SpaceObject _object;
    private Vector2 _currentVelocity;
    private BoundsMirrorer _boundsMirrorer;
    
    private void Awake()
    {
        Initialize(Vector2.zero, Vector2.zero, 0f);
        SetUpLimiter();
    }

    private void SetUpLimiter()
    {
        Vector3 bottomLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector2 gameFieldSize = new Vector2(
            topRightBoundary.x - bottomLeftBoundary.x,
            topRightBoundary.y - bottomLeftBoundary.y);

        Rect screenRect = new Rect(bottomLeftBoundary, gameFieldSize);
        _boundsMirrorer = new BoundsMirrorer(screenRect);
    }


    public void Move(Vector2 acceleration, float rotaionSpeed)
    {
        _currentVelocity += acceleration * Time.deltaTime*Time.deltaTime/2;
        LimitVelocity();
        UpdateMoveStates(_currentVelocity, rotaionSpeed);

        transform.position = _object.Position;
        transform.rotation = Quaternion.Euler(0, 0, -_object.Angle);
    }


    private void UpdateMoveStates(Vector2 moveVector, float rotaionSpeed)
    {
        float newAngle = _object.Angle + rotaionSpeed * Time.deltaTime;
        newAngle = LimitRotation(newAngle );
        _object.Rotate(newAngle);

        Vector2 newPosition = _object.Position + moveVector * Time.deltaTime;
        if (_screenBoundsMirroring)
        {
            newPosition = _boundsMirrorer.TryMirrorPosition(newPosition);
        }
        _object.Move(newPosition);
    }

    private void LimitVelocity()
    {
        if(_currentVelocity.sqrMagnitude > _maxSpeed * _maxSpeed)
        {
            _currentVelocity = _currentVelocity.normalized * _maxSpeed;
        }
    }

    private void Initialize(Vector2 startPosition, Vector2 startVelocity, float startAngle)
    {
        _object = new SpaceObject(startPosition, startAngle);
        _currentVelocity = startVelocity;
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
