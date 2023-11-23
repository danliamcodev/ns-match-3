using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard
{
    #region Variables

    private int height = 0;
    public int Height { get { return height; } }

    private int width = 0;
    public int Width { get { return width; } }
  
    private SC_Gem[,] allGems;

    private List<SC_Gem> bombRewardMarked = new List<SC_Gem>();
    public List<Vector2Int> bombRewardSpawnLocs = new List<Vector2Int>();
    public List<SC_Gem> bombsToDetonate = new List<SC_Gem>();
    public List<SC_Gem> newBombsToDetonate = new List<SC_Gem>();

    public SC_Gem[,] gems { get { return allGems; } }
    //  public Gem[,] AllGems { get { return allGems; } }

    private int score = 0;
    public int Score 
    {
        get { return score; }
        set { score = value; }
    }
    #endregion

    public GameBoard(int _Width, int _Height)
    {
        height = _Height;
        width = _Width;
        allGems = new SC_Gem[width, height];
    }
    public bool MatchesAt(Vector2Int _PositionToCheck, SC_Gem _GemToCheck)
    {
        bool match = false;

        if (_PositionToCheck.x > 0 && _PositionToCheck.x < width - 1 && allGems[_PositionToCheck.x + 1, _PositionToCheck.y] != null)
        {
            if (allGems[_PositionToCheck.x - 1, _PositionToCheck.y].type == _GemToCheck.type &&
                allGems[_PositionToCheck.x + 1, _PositionToCheck.y].type == _GemToCheck.type)
                match = true;
        }

        if (_PositionToCheck.y > 0 && _PositionToCheck.y < height - 1 && allGems[_PositionToCheck.x, _PositionToCheck.y + 1] != null)
        {
            if (allGems[_PositionToCheck.x, _PositionToCheck.y - 1].type == _GemToCheck.type &&
                allGems[_PositionToCheck.x, _PositionToCheck.y + 1].type == _GemToCheck.type)
                match = true;
        }

        if (_PositionToCheck.x > 1)
        {
            if (allGems[_PositionToCheck.x - 1, _PositionToCheck.y].type == _GemToCheck.type &&
                allGems[_PositionToCheck.x - 2, _PositionToCheck.y].type == _GemToCheck.type)
                match = true;
        }

        if (_PositionToCheck.y > 1)
        {
            if (allGems[_PositionToCheck.x, _PositionToCheck.y - 1].type == _GemToCheck.type &&
                allGems[_PositionToCheck.x, _PositionToCheck.y - 2].type == _GemToCheck.type)
                match = true;
        }

        if (_PositionToCheck.x < width - 2 && _PositionToCheck.x >= 0)
        {
            if (allGems[_PositionToCheck.x + 1, _PositionToCheck.y] != null && allGems[_PositionToCheck.x + 2, _PositionToCheck.y] != null)
            {
                if (allGems[_PositionToCheck.x + 1, _PositionToCheck.y].type == _GemToCheck.type &&
                    allGems[_PositionToCheck.x + 2, _PositionToCheck.y].type == _GemToCheck.type)
                    match = true;
            }
        }

 

        if (_PositionToCheck.y >= 0 && _PositionToCheck.y < height - 2 && allGems[_PositionToCheck.x, _PositionToCheck.y + 1] != null && allGems[_PositionToCheck.x, _PositionToCheck.y + 2] != null)
        {
            if (allGems[_PositionToCheck.x, _PositionToCheck.y + 1].type == _GemToCheck.type &&
                allGems[_PositionToCheck.x, _PositionToCheck.y + 2].type == _GemToCheck.type)
                match = true;
        }
        return match;
    }

    public void SetGem(int _X, int _Y, SC_Gem _Gem)
    {
        allGems[_X, _Y] = _Gem;
    }
    public SC_Gem GetGem(int _X,int _Y)
    {
       return allGems[_X, _Y];
    }
}

