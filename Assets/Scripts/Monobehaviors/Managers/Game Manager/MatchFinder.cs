using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int _minMatchThreshold = 3;
    [Header("References")]
    [SerializeField] GemSet _currentMatches;
    [SerializeField] GameBoardReference _gameBoardReference;
    [SerializeField] Vector2IntSet _bombSpawnPositions;

    List<SC_Gem> _matchingGems = new List<SC_Gem>();

    public void FindAllMatches()
    {
        GameBoard gameBoard = _gameBoardReference.gameBoard;
        _bombSpawnPositions.items.Clear();
        // Check horizontal matches
        for (int y = 0; y < gameBoard.Height; y++)
        {
            _matchingGems.Clear();
            for (int x = 0; x < gameBoard.Width - 1; x++)
            {
                SC_Gem currentGem = gameBoard.gems[x, y];
                SC_Gem nextGem = gameBoard.gems[x + 1, y];
                if (!_matchingGems.Contains(currentGem)) _matchingGems.Add(currentGem);
                if (AreMatchingGems(currentGem, nextGem))
                {
                    if (!_matchingGems.Contains(nextGem)) _matchingGems.Add(nextGem);
                }
                else
                {
                    CheckMatchThreshold();
                    _matchingGems.Clear();
                }
            }

            CheckMatchThreshold();
        }
        // Check vertical matches
        for (int x = 0; x < gameBoard.Width; x++)
        {
            _matchingGems.Clear();
            for (int y = 0; y < gameBoard.Height - 1; y++)
            {
                SC_Gem currentGem = gameBoard.gems[x, y];
                SC_Gem nextGem = gameBoard.gems[x, y + 1];
                if (!_matchingGems.Contains(currentGem)) _matchingGems.Add(currentGem);
                if (AreMatchingGems(currentGem, nextGem))
                {
                    if (!_matchingGems.Contains(nextGem)) _matchingGems.Add(nextGem);
                }
                else
                {
                    CheckMatchThreshold();
                    _matchingGems.Clear();
                }
            }
            CheckMatchThreshold();
        }
    }

    bool AreMatchingGems(SC_Gem gem1, SC_Gem gem2)
    {
        return (gem1.type == gem2.type);
    }

    private void CheckMatchThreshold()
    {
        if (_matchingGems.Count >= _minMatchThreshold)
        {
            foreach (SC_Gem gem in _matchingGems)
            {
                gem.isMatch = true;
            }
            _currentMatches.items.AddRange(_matchingGems);
        }

        if (_matchingGems.Count >= 4)
        {
            int midIndex = _matchingGems.Count / 2;
            _bombSpawnPositions.Add(_matchingGems[midIndex].posIndex);
        }
    }
}
