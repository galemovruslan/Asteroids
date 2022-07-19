using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public event Action<ControlType> InputSchmeChanged;

    [SerializeField] private Button _continue;
    [SerializeField] private Button _newGame;
    [SerializeField] private FixedStateButton _inputSelector;
    [SerializeField] private Button _quit;
    
    private Game _game;
    private bool _visible;

    private void Awake()
    {
        _continue.onClick.AddListener(ContinueGame);
        _continue.interactable = false;
        _newGame.onClick.AddListener(NewGame);
        _inputSelector.OnClick.AddListener(SelectInputScheme);
        _quit.onClick.AddListener(Quit);
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
        _continue.interactable = isStarted;
    }

}
