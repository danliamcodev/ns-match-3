using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_GameLogic : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _setupBoard;
    [SerializeField] VoidEvent _findAllMatches;
    [SerializeField] VoidEvent _destroyAllMatches;
    [SerializeField] VoidEvent _cascadeGems;
    [SerializeField] VoidEvent _markGemsToExplode;
    [SerializeField] VoidEvent _explodeGemsMarkedByBombs;
    [SerializeField] VoidEvent _checkMisplacedGems;
    [SerializeField] VoidEvent _refillBoard;

    [Header("References")]
    [SerializeField] GemSet _currentMatches;
    [SerializeField] GemSet _bombsToDetonate;
    [SerializeField] GameBoardReference _gameBoardReference;

    private Dictionary<string, GameObject> unityObjects;
    private int score = 0;
    private float displayScore = 0;
    private GameBoard gameBoard;
    private GlobalEnums.GameState currentState = GlobalEnums.GameState.move;

    public GlobalEnums.GameState CurrentState { get { return currentState; } }

    #region MonoBehaviour
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _setupBoard.Raise();
        StartGame();
    }

    private void Update()
    {
        displayScore = Mathf.Lerp(displayScore, gameBoard.Score, SC_GameVariables.Instance.scoreSpeed * Time.deltaTime);
        unityObjects["Txt_Score"].GetComponent<TMPro.TextMeshProUGUI>().text = displayScore.ToString("0");
    }
    #endregion

    #region Logic
    private void Init()
    {
        unityObjects = new Dictionary<string, GameObject>();
        GameObject[] _obj = GameObject.FindGameObjectsWithTag("UnityObject");
        foreach (GameObject g in _obj)
            unityObjects.Add(g.name,g);

        gameBoard = new GameBoard(7, 7);
        _gameBoardReference.SetReference(gameBoard);
    }

    public void StartGame()
    {
        unityObjects["Txt_Score"].GetComponent<TextMeshProUGUI>().text = score.ToString("0");
    }

    public void SetGem(int _X,int _Y, SC_Gem _Gem)
    {
        gameBoard.SetGem(_X,_Y, _Gem);
    }
    public SC_Gem GetGem(int _X, int _Y)
    {
        return gameBoard.GetGem(_X, _Y);
    }
    public void SetState(GlobalEnums.GameState _CurrentState)
    {
        currentState = _CurrentState;
    }

    public void ScoreCheck(SC_Gem gemToCheck)
    {
        gameBoard.Score += gemToCheck.scoreValue;
    }

    public void OnBoardRefilled()
    {
        _checkMisplacedGems.Raise();
    }

    public void OnMatchesDestroyed()
    {
        _cascadeGems.Raise();
    }

    public void OnGemsCascaded()
    {
        if (_bombsToDetonate.items.Count > 0)
        {
            StartCoroutine(DetonateBombsTask());
        } else
        {
            StartCoroutine(RefillBoardTask());
        }

    }

    private IEnumerator DetonateBombsTask()
    {
        _markGemsToExplode.Raise();

        yield return new WaitForSeconds(0.8f);

        _explodeGemsMarkedByBombs.Raise();
    }

    private IEnumerator RefillBoardTask()
    {
        yield return new WaitForSeconds(0.5f);
        _refillBoard.Raise();
        yield return new WaitForSeconds(0.5f);
        _findAllMatches.Raise();
        if (_currentMatches.items.Count > 0)
        {
            yield return new WaitForSeconds(0.5f);
            _destroyAllMatches.Raise();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            currentState = GlobalEnums.GameState.move;
        }
    }
    #endregion
}
