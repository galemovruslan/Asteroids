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
    [SerializeField] private Toggle _inputSelector;
    [SerializeField] private Button _quit;
    [SerializeField] private Game _game;


    private void NewGame()
    {
        _game.StartGame();
    }

    private void ContinueGame()
    {

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

}
