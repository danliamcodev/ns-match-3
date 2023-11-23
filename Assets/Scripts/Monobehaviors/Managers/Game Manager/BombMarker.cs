using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMarker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GemSet _currentMatches;
    [SerializeField] GemSet _bombsToDetonate;
    [SerializeField] GemSet _gemsToExplode;
    [SerializeField] GameBoardReference _currentGameBoardReference;

    private List<SC_Gem> _detonatedBombs = new List<SC_Gem>();
    public void MarkBombsToDetonate()
    {
        _bombsToDetonate.items.Clear();

        for (int i = 0; i < _currentMatches.items.Count; i++)
        {
            SC_Gem[,] allGems = _currentGameBoardReference.gameBoard.gems;
            SC_Gem gem = _currentMatches.items[i];
            int x = gem.posIndex.x;
            int y = gem.posIndex.y;

            if (gem.posIndex.x > 0)
            {
                if (allGems[x - 1, y] != null && allGems[x - 1, y].type == GlobalEnums.GemType.bomb)
                    _bombsToDetonate.Add(allGems[x - 1, y]);
            }

            if (gem.posIndex.x + 1 < _currentGameBoardReference.gameBoard.Width)
            {
                if (allGems[x + 1, y] != null && allGems[x + 1, y].type == GlobalEnums.GemType.bomb)
                    _bombsToDetonate.Add(allGems[x + 1, y]);
            }

            if (gem.posIndex.y > 0)
            {
                if (allGems[x, y - 1] != null && allGems[x, y - 1].type == GlobalEnums.GemType.bomb)
                    _bombsToDetonate.Add(allGems[x, y - 1]);
            }

            if (gem.posIndex.y + 1 < _currentGameBoardReference.gameBoard.Height)
            {
                if (allGems[x, y + 1] != null && allGems[x, y + 1].type == GlobalEnums.GemType.bomb)
                    _bombsToDetonate.Add(allGems[x, y + 1]);
            }
        }
    }

    public void DetonateBombs()
    {
        foreach (SC_Gem bomb in _bombsToDetonate.items)
        {
            MarkGemsToExplode(bomb);
        }
        _bombsToDetonate.items.Clear();
        _detonatedBombs.Clear();
    }

    private void MarkGemsToExplode(SC_Gem p_bomb)
    {
        _detonatedBombs.Add(p_bomb);
        SC_Gem[,] allGems = _currentGameBoardReference.gameBoard.gems;
        Vector2Int bombPos = p_bomb.posIndex;
        for (int x = bombPos.x - p_bomb.blastSize; x <= bombPos.x + p_bomb.blastSize; x++)
        {
            for (int y = bombPos.y - p_bomb.blastSize; y <= bombPos.y + p_bomb.blastSize; y++)
            {
                if (x >= 0 && x < _currentGameBoardReference.gameBoard.Width && y >= 0 && y < _currentGameBoardReference.gameBoard.Height)
                {
                    if (allGems[x, y] != null)
                    {
                        if (allGems[x, y].type == GlobalEnums.GemType.bomb && allGems[x, y] != p_bomb && !_detonatedBombs.Contains(allGems[x,y]))
                        {
                            MarkGemsToExplode(allGems[x, y]);
                        }
                        _gemsToExplode.Add(allGems[x, y]);
                        allGems[x, y].isMatch = true;
                    }
                }
            }
        }
    }
}
