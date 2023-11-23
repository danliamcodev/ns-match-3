using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Board Reference", menuName = "Variables/Game Board Reference")]
public class GameBoardReference : ScriptableObject
{
    GameBoard _gameBoard;
    public GameBoard gameBoard { get { return _gameBoard; } }

    public void SetReference(GameBoard p_gameBoard)
    {
        _gameBoard = p_gameBoard;
    }
}
