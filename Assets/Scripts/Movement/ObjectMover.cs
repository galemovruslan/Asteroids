using UnityEngine;

[RequireComponent(typeof(PauseableObject))]
public class ObjectMover : MonoBehaviour, IPauseable
{
    public float Angle => _object.Angle;

    [SerializeField] private float _maxSpeed;
    [SerializeField] private bool _screenBoundsMirroring = true;

    private SpaceObject _object;
    private Vector2 _currentVelocity;
    private BoundsMirrorer _boundsMirrorer;
    private bool _isPaused = false;

    private void Awake()
    {
        Initialize(Vector2.zero, Vector2.zero, 0f);
        SetUpLimiter();
    }

    public void Initialize(Vector2 startPosition, Vector2 startVelocity, float startAngle)
    {
        if (_object == null)
        {
            _object = new SpaceObject(startPosition, startAngle);
        }
        else
        {
            _object.Move(startPosition);
            _object.Rotate(startAngle);
        }
        _currentVelocity = startVelocity;
    }

    public void Move(Vector2 acceleration, float rotaionSpeed)
    {
        if (_isPaused) { return; }

        _currentVelocity += acceleration * Time.deltaTime * Time.deltaTime / 2;
        LimitVelocity();
        UpdateMoveStates(_currentVelocity, rotaionSpeed);

        transform.position = _object.Position;
        transform.rotation = Quaternion.Euler(0, 0, -_object.Angle);
    }

    private void SetUpLimiter()
    {
        //Vector3 bottomLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        //Vector3 topRightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        //Vector2 gameFieldSize = new Vector2(
        //    topRightBoundary.x - bottomLeftBoundary.x,
        //    topRightBoundary.y - bottomLeftBoundary.y);

        //Rect screenRect = new Rect(bottomLeftBoundary, gameFieldSize);
        Rect screenRect = Constants.ScreenRect;
        _boundsMirrorer = new BoundsMirrorer(screenRect);
    }

    private void UpdateMoveStates(Vector2 moveVector, float rotaionSpeed)
    {
        float newAngle = _object.Angle + rotaionSpeed * Time.deltaTime;
        newAngle = LimitRotation(newAngle);
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
        if (_currentVelocity.sqrMagnitude > _maxSpeed * _maxSpeed)
        {
            _currentVelocity = _currentVelocity.normalized * _maxSpeed;
        }
    }

    private float LimitRotation(float rotation)
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

    public void SetPause(bool value)
    {
        _isPaused = value;
    }
}
