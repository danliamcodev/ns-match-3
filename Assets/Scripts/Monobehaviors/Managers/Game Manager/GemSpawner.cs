using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _boardRefilled;
    [SerializeField] VoidEvent _gemsCascaded;
    [Header("References")]
    [SerializeField] GameObject _gemsHolder;
    [SerializeField] SC_GameLogic _gameLogic;
    [SerializeField] GameBoardReference _gameBoardReference;
    [SerializeField] GemSet _currentMatches;
    [SerializeField] Vector2IntSet _bombSpawnPositions;
    [Header("Parameters")]
    [SerializeField] float _dropTime = 0.25f;
    List<List<SC_Gem>> _gemsToDropColumns = new List<List<SC_Gem>>();

    public void SpawnStartingGems()
    {
        _gemsToDropColumns.Clear();
        for (int x = 0; x < _gameBoardReference.gameBoard.Width; x++)
        {
            _gemsToDropColumns.Add(new List<SC_Gem>());

            for (int y = 0; y < _gameBoardReference.gameBoard.Height; y++)
            {
                Vector2 _pos = new Vector2(x, y);
                GameObject _bgTile = Instantiate(SC_GameVariables.Instance.bgTilePrefabs, _pos, Quaternion.identity);
                _bgTile.transform.SetParent(_gemsHolder.transform);
                _bgTile.name = "BG Tile - " + x + ", " + y;
                SpawnRandomGem(new Vector2Int(x, y));
            }
        }
        DropGems();
    }

    public void RefillBoard()
    {
        List<Vector2Int> gemSpawnPositions = new List<Vector2Int>();
        for (int x = 0; x < _gameBoardReference.gameBoard.Width; x++)
        {
            for (int y = 0; y < _gameBoardReference.gameBoard.Height; y++)
            {
                SC_Gem currentGem = _gameBoardReference.gameBoard.GetGem(x, y);
                if (currentGem == null)
                {
                    SpawnRandomGem(new Vector2Int(x, y));
                }
            }
        }
        DropGems();
    }

    private void SpawnRandomGem(Vector2Int p_position)
    {
        int _gemToUse = Random.Range(0, SC_GameVariables.Instance.gemObjectPools.Length);
        int iterations = 0;
        while (_gameBoardReference.gameBoard.MatchesAt(new Vector2Int(p_position.x, p_position.y), SC_GameVariables.Instance.gemObjectPools[_gemToUse].prefab.GetComponent<SC_Gem>()) && iterations < 100)
        {
            _gemToUse = Random.Range(0, SC_GameVariables.Instance.gemObjectPools.Length);
            iterations++;
        }
        SpawnGem(new Vector2Int(p_position.x, p_position.y), SC_GameVariables.Instance.gemObjectPools[_gemToUse]);
    }

    private void SpawnGem(Vector2Int _Position, ObjectPoolController _GemObjectPoolController)
    {
        if (Random.Range(0, 100f) < SC_GameVariables.Instance.bombChance)
            _GemObjectPoolController = SC_GameVariables.Instance.bombObjectPool;

        SC_Gem gem = _GemObjectPoolController.GetObject().GetComponent<SC_Gem>();

        gem.transform.position = new Vector3(_Position.x, _gameBoardReference.gameBoard.Height, 0f);
        gem.transform.rotation = Quaternion.identity;
        gem.transform.SetParent(_gemsHolder.transform);
        gem.name = "Gem - " + _Position.x + ", " + _Position.y;

        _gameBoardReference.gameBoard.SetGem(_Position.x, _Position.y, gem);
        gem.SetupGem(_gameLogic, _Position);
        gem.objectPoolController = _GemObjectPoolController;
        _gemsToDropColumns[_Position.x].Add(gem);
    }

    private void DropGems()
    {
        StartCoroutine(DropGemsTask());
    }

    private IEnumerator DropGemsTask()
    {
        for (int x = 0; x < _gameBoardReference.gameBoard.Width; x++)
        {
            StartCoroutine(DropGemsInColumnTask(x));
        }

        yield return new WaitForSeconds(_dropTime);
        _boardRefilled.Raise();
    }

    private IEnumerator DropGemsInColumnTask(int p_x)
    {
        List<SC_Gem> gemsToDrop = _gemsToDropColumns[p_x];
        float spawnDelay = _dropTime / (gemsToDrop.Count);
        for (int i = 0; i < gemsToDrop.Count; i++)
        {
            gemsToDrop[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
        _gemsToDropColumns[p_x].Clear();
    }

    private void SpawnBomb(Vector2Int p_position)
    {
        SC_Gem _gem = SC_GameVariables.Instance.bombObjectPool.GetObject().GetComponent<SC_Gem>();


        _gem.transform.position = new Vector3(p_position.x, p_position.y, 0f);
        _gem.transform.rotation = Quaternion.identity;
        _gem.transform.SetParent(_gemsHolder.transform);
        _gem.name = "Gem - " + p_position.x + ", " + p_position.y;

        _gameBoardReference.gameBoard.SetGem(p_position.x, p_position.y, _gem);
        _gem.SetupGem(_gameLogic, p_position);
        _gem.objectPoolController = SC_GameVariables.Instance.bombObjectPool;
        _gem.gameObject.SetActive(true);
    }

    public void OnSpawnBombs()
    {
        foreach (Vector2Int bombSpawnPosition in _bombSpawnPositions.items)
        {
            SpawnBomb(bombSpawnPosition);
        }
    }

    public void OnCascadeGems()
    {
        StartCoroutine(CascadeGemsTask());
    }

    private IEnumerator CascadeGemsTask()
    {
        GameBoard gameBoard = _gameBoardReference.gameBoard;
        yield return new WaitForSeconds(.2f);

        int nullCounter = 0;
        for (int x = 0; x < gameBoard.Width; x++)
        {
            for (int y = 0; y < gameBoard.Height; y++)
            {
                SC_Gem _curGem = gameBoard.GetGem(x, y);
                if (_curGem == null)
                {
                    nullCounter++;
                }
                else if (nullCounter > 0)
                {
                    _curGem.posIndex.y -= nullCounter;
                    gameBoard.SetGem(x, y - nullCounter, _curGem);
                    gameBoard.SetGem(x, y, null);
                }
            }
            nullCounter = 0;
        }
        _gemsCascaded.Raise();
    }
}
