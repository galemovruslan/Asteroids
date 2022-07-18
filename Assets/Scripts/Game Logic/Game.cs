using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private IntegerIndicator _healthCounter;
    [SerializeField] private IntegerIndicator _pointsCounter;
    [SerializeField] private PlayerComposer _player;
    [SerializeField] private UfoSpawner _ufoSpawner;
    [SerializeField] private AsteroidWaveSpawner _asteroidSpawner;

    private PlayerPlacer _playerPlacer;
    private LifesSystem _lifes;
    private PointsSystem _points;
    private IInputHandle _inputScheme;

    private void Awake()
    {
        _lifes = new LifesSystem(_maxHealth);
        _lifes.LiveTaken += OnLiveTaken;

        _points = new PointsSystem();
        _points.PointsAdded += OnPointsAdded;

        _inputScheme = new KeyboardInput();

        _playerPlacer = new PlayerPlacer(_player);
    }

    private void Start()
    {
        //StartGame();
    }

    public void StartGame()
    {
        _playerPlacer.PlaceAt(Vector2.zero);
        _player.SetInputScheme(_inputScheme);
        _ufoSpawner.StarSpawn();
        _asteroidSpawner.StartSpawn();
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

    private void OnPointsAdded(int amount)
    {

    }

    private void OnLiveTaken(int amount)
    {

    }
}
