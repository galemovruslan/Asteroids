using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawner : MonoBehaviour
{
    public event Action<int> OnDestroyedByPlayer;

    [SerializeField] private Ufo _ufoPrefab;
    [SerializeField] private float _minSpawnDelay = 20f;
    [SerializeField] private float _maxSpawnDelay = 40f;
    [Range(0, 0.5f)]
    [SerializeField] private float _spawnMarginPercent = 0.2f;

    private float _respawnDelay;
    private DirectiontPicker _directiontPicker;
    private Timer _respawnTimer;
    private Ufo _ufo;

    private void Awake()
    {
        _directiontPicker = new DirectiontPicker();
        _respawnTimer = new Timer();
        _ufo = Instantiate<Ufo>(_ufoPrefab, Vector3.zero, Quaternion.identity);
    }

    private void Start()
    {
        _ufo.WaitForLaunch();
    }

    public void StarSpawn()
    {
        ResetSpawner();
        _ufo.Destroyed += OnDestruction;
        _respawnDelay = UnityEngine.Random.Range(_minSpawnDelay, _maxSpawnDelay);
        _respawnTimer.OnDone += LaunchRandom;
        _respawnTimer.Restart(_respawnDelay);
    }

    private void ResetSpawner()
    {
        _ufo.Destroyed -= OnDestruction;
        _respawnTimer.OnDone -= LaunchRandom;
        _ufo.ForceDestroy();
        _respawnTimer.Stop();
    }

    private void LaunchRandom()
    {
        bool isMoveRight = UnityEngine.Random.Range(0f, 1f) < 0.5f ? true : false;
        LaunchData launchData = _directiontPicker.GetHorizontalDirection(isMoveRight, _spawnMarginPercent);
        Launch(launchData.Start, launchData.Direction);
    }

    private void Launch(Vector2 start, Vector2 direction)
    {
        _ufo.Launch(start, direction);
    }

    private void OnDestruction(int points)
    {
        _respawnDelay = UnityEngine.Random.Range(_minSpawnDelay, _maxSpawnDelay);
        _respawnTimer.Restart(_respawnDelay);
        
        if(points != 0)
        {
            OnDestroyedByPlayer?.Invoke(points);
        }
    }
}
