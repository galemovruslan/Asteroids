using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWaveSpawner : MonoBehaviour
{
    [SerializeField] private Pool _bigAsteroidPool;
    [SerializeField] private Pool _mediumAsteroidPool;
    [SerializeField] private Pool _smallAsteroidPool;
    [Space]
    [SerializeField] private int _startWaveAmount = 2;
    [SerializeField] private int _spawnIncrement = 1;
    [SerializeField] private int _splitAmount = 2;

    private int _spawnAmount;
    private HashSet<Asteroid> _currentWave;
    private void Awake()
    {
        _spawnAmount = _startWaveAmount;
        _currentWave = new HashSet<Asteroid>();
    }

    private void Start()
    {
        SpawnWave(_splitAmount);
    }

    private void OnAsteroidDestroy(Asteroid asteroid, bool spawnNext)
    {
        asteroid.Destroyed -= OnAsteroidDestroy;
        _currentWave.Remove(asteroid);

        if (spawnNext)
        {
            for (int i = 0; i < _splitAmount; i++)
            {
                var newAsteroid = GetNextAsteroid(asteroid.Size);
                if (newAsteroid == null)
                {
                    return;
                }
                _currentWave.Add(newAsteroid);
                newAsteroid.Destroyed += OnAsteroidDestroy;
            }
        }

        if (_currentWave.Count == 0)
        {
            _spawnAmount += _spawnIncrement;
            SpawnWave(_spawnAmount);
        }
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

    private void SpawnWave(int size)
    {
        for (int i = 0; i < size; i++)
        {
            Asteroid newAsteroid = _bigAsteroidPool.GetItem() as Asteroid;
            newAsteroid.Destroyed += OnAsteroidDestroy;
            _currentWave.Add(newAsteroid);
        }
    }

}