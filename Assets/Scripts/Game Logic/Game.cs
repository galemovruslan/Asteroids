using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private IntBarUI _healthCounter;
    [SerializeField] private IntBarUI _pointsCounter;

    private LivesSystem _lives;
    private PointsSystem _points;
    private PlayerComposer _player;

    private void Awake()
    {
        _lives = new LivesSystem(_maxHealth);
        _lives.LiveTaken += OnLiveTaken;

        _points = new PointsSystem();
        _points.PointsAdded += OnPointsAdded;
    }

    private void OnPointsAdded(int amount)
    {

    }

    private void OnLiveTaken(int amount)
    {
        
    }
}
