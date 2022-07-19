using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWaveSpawner : MonoBehaviour
{
    public event Action<int> OnDestroyedByPlayer;

    [SerializeField] private Pool _bigAsteroidPool;
    [SerializeField] private Pool _mediumAsteroidPool;
    [SerializeField] private Pool _smallAsteroidPool;
    [Space]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private int _startWaveAmount = 2;
    [SerializeField] private int _spawnIncrement = 1;
    [SerializeField] private int _splitAmount = 2;
    [Range(0, 180f)]
    [SerializeField] private float _splitAngleDegree = 45;
    [Range(0, 0.5f)]
    [SerializeField] private float _borderMarginRatio = 0f;
    [Min(0)]
    [SerializeField] private float _spawnDelaySeconds = 2f;

    private int _spawnAmount;
    private HashSet<Asteroid> _currentWave = new HashSet<Asteroid>();
    private DirectiontPicker _directiontPicker;
    private Timer _spawnDelayTimer;

    private void Awake()
    {
        _spawnDelayTimer = new Timer();
        _spawnDelayTimer.OnDone += () => { SpawnWave(_spawnAmount); };
    }

    public void StartSpawn()
    {
        ResetWave();
        DelayedSpawn();
    }

    private void OnAsteroidDestroy(Asteroid asteroid, bool spawnNext, int points)
    {
        asteroid.Destroyed -= OnAsteroidDestroy;
        _currentWave.Remove(asteroid);

        if (spawnNext)
        {
            SpawnSubAsteroids(asteroid);
        }

        if (_currentWave.Count == 0)
        {
            _spawnAmount += _spawnIncrement;
            DelayedSpawn();
        }
        if (points != 0)
        {
            OnDestroyedByPlayer?.Invoke(points);
        }

    }

    private void SpawnSubAsteroids(Asteroid asteroid)
    {
        List<Asteroid> subAsteroids = new List<Asteroid>();
        for (int i = 0; i < _splitAmount; i++)
        {
            Asteroid newAsteroid = GetNextAsteroid(asteroid.Size);

            if (newAsteroid == null)
            {
                return;
            }
            subAsteroids.Add(newAsteroid);
            _currentWave.Add(newAsteroid);
            newAsteroid.Destroyed += OnAsteroidDestroy;
        }

        CalculateSubAsteroidsVelocity(asteroid, subAsteroids);
    }

    private Asteroid GetNextAsteroid(Asteroid.AsteroidSize size)
    {
        Asteroid newAsteroid = null;
        switch (size)
        {
            case Asteroid.AsteroidSize.Small:
                break;
            case Asteroid.AsteroidSize.Medium:
                newAsteroid = _smallAsteroidPool.GetItem() as Asteroid;
                break;
            case Asteroid.AsteroidSize.Big:
                newAsteroid = _mediumAsteroidPool.GetItem() as Asteroid;
                break;
        }
        return newAsteroid;
    }

    private void ResetWave()
    {
        foreach (var asteroid in _currentWave)
        {
            asteroid.Destroyed -= OnAsteroidDestroy;
        }
        _spawnAmount = _startWaveAmount;
        _currentWave.Clear();
        _directiontPicker = new DirectiontPicker();
        _bigAsteroidPool.ResetContents();
        _mediumAsteroidPool.ResetContents();
        _smallAsteroidPool.ResetContents();
    }

    private void SpawnWave(int size)
    {

        for (int i = 0; i < size; i++)
        {
            Asteroid newAsteroid = _bigAsteroidPool.GetItem() as Asteroid;

            newAsteroid.Destroyed += OnAsteroidDestroy;
            _currentWave.Add(newAsteroid);

            LaunchData launchData = GetRandomLaunchDirection();
            float speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
            newAsteroid.Launch(launchData.Start, launchData.Direction, speed, 1f);
        }
    }
    private void DelayedSpawn()
    {
        _spawnDelayTimer.Restart(_spawnDelaySeconds);
    }

    private LaunchData GetRandomLaunchDirection()
    {
        if (UnityEngine.Random.Range(0f, 1f) < 0.5)
        {
            bool isPositive = UnityEngine.Random.Range(0, 0.5f) < 0.5 ? true : false;
            return _directiontPicker.GetHorizontalDirection(isPositive, _borderMarginRatio);
        }
        else
        {
            bool isPositive = UnityEngine.Random.Range(0, 0.5f) < 0.5 ? true : false;
            return _directiontPicker.GetVerticalDirection(isPositive, _borderMarginRatio);
        }
    }

    private void CalculateSubAsteroidsVelocity(Asteroid parentAsteroid, List<Asteroid> subAsteroids)
    {
        float subRotation = UnityEngine.Random.Range(0f, 10f);
        float subSpeed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
        float angleOffset = 2 * _splitAngleDegree / _splitAmount;
        Vector2 parentVelocity = parentAsteroid.Velocity;

        for (int i = 0; i < subAsteroids.Count; i++)
        {
            Vector2 splitVelocity = Quaternion.Euler(0, 0, -_splitAngleDegree + i * angleOffset) * parentVelocity;
            subAsteroids[i].Launch(parentAsteroid.Position, splitVelocity, subSpeed, subRotation);
        }
    }

    

}