using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public event Action<bool> GameStarted;

    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private IntegerIndicator _healthUI;
    [SerializeField] private IntegerIndicator _pointsUI;
    [SerializeField] private PlayerComposer _player;
    [SerializeField] private UfoSpawner _ufoSpawner;
    [SerializeField] private AsteroidWaveSpawner _asteroidSpawner;
    [SerializeField] private MainMenu _menu;
    [SerializeField] private Vector2 _playerSpawnPosition = Vector2.zero;
    [SerializeField] private float _invincibilityTime = 2f;

    private PlayerPlacer _playerPlacer;
    private LifesSystem _lifes;
    private PointsSystem _points;
    private IInputHandle _inputScheme;
    private Pause _gamePause;
    private bool _isStarted;

    private void Awake()
    {
        _lifes = new LifesSystem(_maxHealth);
        _lifes.LiveTaken += OnLiveTaken;

        _points = new PointsSystem(0);
        _points.PointsAdded += OnPointsAdded;

        //_playerPlacer = new PlayerPlacer(_player);
        _gamePause = new Pause();
        _gamePause.PauseSet += HandlePauseChange;

        _player.PlayerDestroyed += OnPlayerDestroyed;
        _inputScheme = new KeyboardInput();
       
        _menu.Initialize(this);

        OnLiveTaken(_lifes.Current);
        OnPointsAdded(_points.Current);

        _ufoSpawner.OnDestroyedByPlayer += _points.Add;
        _asteroidSpawner.OnDestroyedByPlayer += _points.Add;
    }


    private void Start()
    {
        _player.SetInputScheme(_inputScheme);
        PauseGame();
    }

    private void Update()
    {
        
        if (_inputScheme.GetPause())
        {
            TogglePause();
        }
    }

    public void ContinueGame()
    {
        TogglePause();
    }

    
    public void StartGame()
    {
        //_playerPlacer.PlaceAt(_playerSpawnPosition);
        _ufoSpawner.StarSpawn();
        _asteroidSpawner.StartSpawn();
        _lifes.ResetHealth();
        _points.Reset();
        _gamePause.SetPause(false);
        _player.ResetPlayer(_playerSpawnPosition);
        _player.ActivateInvincibility(_invincibilityTime);
        _isStarted = true;
        GameStarted?.Invoke(_isStarted);
    }

    public void OnInputSchmeChanged(ControlType type)
    {
        switch (type)
        {
            case ControlType.Keyboard:
                _inputScheme = new KeyboardInput();
                break;
            case ControlType.Combined:
                _inputScheme = new CombinedInput(_player.transform);
                break;
        }
        _player.SetInputScheme(_inputScheme);
    }

    private void TogglePause()
    {
        bool newState = !_gamePause.IsPaused;
        _gamePause.SetPause(newState);
    }

    private void PauseGame()
    {
        _gamePause.SetPause(true);
    }

    private void HandlePauseChange(bool isPaused)
    {
        _menu.SetVisible(isPaused);
    }

    private void OnPlayerDestroyed()
    {
        _lifes.TakeLive();
        _player.ResetPlayer(_playerSpawnPosition);
        _player.ActivateInvincibility(_invincibilityTime);
    }

    private void OnPointsAdded(int amount)
    {
        _pointsUI.SetValue(amount);
    }

    private void OnLiveTaken(int amount)
    {
        _healthUI.SetValue(amount);
        if (amount == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _isStarted = false;
        GameStarted?.Invoke(_isStarted);
        PauseGame();
    }
}
