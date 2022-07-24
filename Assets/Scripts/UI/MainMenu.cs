using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public event Action<ControlType> InputSchmeChanged;

    [SerializeField] private MenuButton _continue;
    [SerializeField] private MenuButton _newGame;
    [SerializeField] private FixedStateButton _inputSelector;
    [SerializeField] private MenuButton _quit;
    
    private Game _game;
    private bool _visible;

    private void Awake()
    {
        _continue.OnClick.AddListener(ContinueGame);
        _newGame.OnClick.AddListener(NewGame);
        _inputSelector.OnClick.AddListener(SelectInputScheme);
        _quit.OnClick.AddListener(Quit);
    }

    private void Start()
    {
        _continue.Interactable = false;
    }

    public void Initialize(Game game)
    {
        _game = game;
        _game.GameStarted += OnGameStarted;

    }

    public void SetVisible(bool value)
    {
        _visible = value;
        gameObject.SetActive(_visible);
    }

    private void NewGame()
    {
        _game.StartGame();
    }

    private void ContinueGame()
    {
        _game.ContinueGame();
    }

    private void SelectInputScheme(bool isCombined)
    {
        ControlType selectedType = isCombined ? ControlType.Combined : ControlType.Keyboard;
        _game.OnInputSchmeChanged(selectedType);
    }

    private void Quit()
    {
        Application.Quit();
    }


    private void OnGameStarted(bool isStarted)
    {
        _continue.Interactable = isStarted;
    }

}
