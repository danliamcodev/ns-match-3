using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDestroyer : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _markBombsToDetonate;
    [SerializeField] VoidEvent _matchesDestroyed;
    [SerializeField] VoidEvent _spawnBombs;
    [SerializeField] SC_GemEvent _scoreGem;
    [Header("References")]
    [SerializeField] GemSet _currentMatches;
    [SerializeField] GemSet _gemsToExplode;
    [SerializeField] GameBoardReference _gameBoardReference;
    [SerializeField] Vector2IntSet _bombSpawnPositions;
    public void DestroyAllMatches()
    {
        _markBombsToDetonate.Raise();
        for (int i = 0; i < _currentMatches.items.Count; i++)
            if (_currentMatches.items[i] != null)
            {
                _scoreGem.Raise(_currentMatches.items[i]);
                DestroyMatchedGemsAt(_currentMatches.items[i].posIndex);
            }
        
        if (_bombSpawnPositions.items.Count > 0)
        {
            _spawnBombs.Raise();
        }
        _currentMatches.items.Clear();
        _matchesDestroyed.Raise();
    }

    public void ExplodeBombedGems()
    {
        for (int i = 0; i < _gemsToExplode.items.Count; i++)
            if (_gemsToExplode.items[i] != null)
            {
                _scoreGem.Raise(_gemsToExplode.items[i]);
                DestroyMatchedGemsAt(_gemsToExplode.items[i].posIndex);
            }
        _gemsToExplode.items.Clear();
        _matchesDestroyed.Raise();
    }

    private void DestroyMatchedGemsAt(Vector2Int _Pos)
    {
        SC_Gem currentGem = _gameBoardReference.gameBoard.GetGem(_Pos.x, _Pos.y);
        if (currentGem != null)
        {
            Instantiate(currentGem.destroyEffect, new Vector2(_Pos.x, _Pos.y), Quaternion.identity);
            currentGem.objectPoolController.ReturnObject(currentGem.gameObject);
            _gameBoardReference.gameBoard.SetGem(_Pos.x, _Pos.y, null);
        }
    }
}
