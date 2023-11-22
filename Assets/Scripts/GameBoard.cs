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
  //  public Gem[,] AllGems { get { return allGems; } }

    private int score = 0;
    public int Score 
    {
        get { return score; }
        set { score = value; }
    }

    private List<SC_Gem> currentMatches = new List<SC_Gem>();
    public List<SC_Gem> CurrentMatches { get { return currentMatches; } }
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
            //string gemTypes = string.Format("{0} {1} {2}", firstName, lastName, );
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

    public void FindAllMatches()
    {
        currentMatches.Clear();
        bombRewardMarked.Clear();
        bombRewardSpawnLocs.Clear();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                SC_Gem currentGem = allGems[x, y];
                if (currentGem != null)
                {
                    if (x > 0 && x < width - 1)
                    {
                        SC_Gem leftGem = allGems[x - 1, y];
                        SC_Gem rightGem = allGems[x + 1, y];
         
     
                        //checking no empty spots
                        if (leftGem != null && rightGem != null)
                        {
                            //Match
                            if (leftGem.type == currentGem.type && rightGem.type == currentGem.type)
                            {
                                currentGem.isMatch = true;
                                leftGem.isMatch = true;
                                rightGem.isMatch = true;
                                currentMatches.Add(currentGem);
                                currentMatches.Add(leftGem);
                                currentMatches.Add(rightGem);

                                if (x > 1)
                                {
                                    SC_Gem leftMostGem = allGems[x - 2, y];
                                    if (leftMostGem.type == currentGem.type && !(bombRewardMarked.Contains(leftMostGem) || bombRewardMarked.Contains(currentGem)))
                                    {
                                        bombRewardMarked.Add(currentGem);
                                        bombRewardMarked.Add(leftGem);
                                        bombRewardMarked.Add(rightGem);
                                        bombRewardMarked.Add(leftMostGem);
                                        bombRewardSpawnLocs.Add(new Vector2Int(x, y));
                                        Debug.Log("4 in a row");
                                    }
                                }

                                if (x < width - 2)
                                {
                                    SC_Gem rightMostGem = allGems[x + 2, y];
                                    if (rightMostGem.type == currentGem.type && !(bombRewardMarked.Contains(rightMostGem) || bombRewardMarked.Contains(currentGem)))
                                    {
                                        bombRewardMarked.Add(currentGem);
                                        bombRewardMarked.Add(leftGem);
                                        bombRewardMarked.Add(rightGem);
                                        bombRewardMarked.Add(rightMostGem);
                                        bombRewardSpawnLocs.Add(new Vector2Int(x, y));
                                        Debug.Log("4 in a row");
                                    }
                                }
                            }
                        }
                    }

                    if (y > 0 && y < height - 1)
                    {
                        SC_Gem aboveGem = allGems[x, y - 1];
                        SC_Gem bellowGem = allGems[x, y + 1];

         
                        //checking no empty spots
                        if (aboveGem != null && bellowGem != null)
                        {
                            //Match
                            if (aboveGem.type == currentGem.type && bellowGem.type == currentGem.type)
                            {
                                currentGem.isMatch = true;
                                aboveGem.isMatch = true;
                                bellowGem.isMatch = true;
                                currentMatches.Add(currentGem);
                                currentMatches.Add(aboveGem);
                                currentMatches.Add(bellowGem);


                                if (y > 1)
                                {
                                    SC_Gem aboveMostGem = allGems[x, y - 2];
                                    if (aboveMostGem.type == currentGem.type && !(bombRewardMarked.Contains(aboveMostGem) || bombRewardMarked.Contains(currentGem)))
                                    {
                                        bombRewardMarked.Add(currentGem);
                                        bombRewardMarked.Add(aboveGem);
                                        bombRewardMarked.Add(bellowGem);
                                        bombRewardMarked.Add(aboveMostGem);
                                        bombRewardSpawnLocs.Add(new Vector2Int(x, y));
                                        Debug.Log("4 in a row");
                                    }
                                }

                                if (y < height - 2)
                                {
                                    SC_Gem belowMostGem = allGems[x, y + 2];
                                    if (belowMostGem.type == currentGem.type && !(bombRewardMarked.Contains(belowMostGem) || bombRewardMarked.Contains(currentGem)))
                                    {
                                        bombRewardMarked.Add(currentGem);
                                        bombRewardMarked.Add(aboveGem);
                                        bombRewardMarked.Add(bellowGem);
                                        bombRewardMarked.Add(belowMostGem);
                                        bombRewardSpawnLocs.Add(new Vector2Int(x, y));
                                        Debug.Log("4 in a row");
                                    }
                                }
                            }
                        }
                    }
                }
            }

        if (currentMatches.Count > 0)
            currentMatches = currentMatches.Distinct().ToList();

        CheckForBombs();
    }

    public void CheckForBombs()
    {
        for (int i = 0; i < currentMatches.Count; i++)
        {
            SC_Gem gem = currentMatches[i];
            int x = gem.posIndex.x;
            int y = gem.posIndex.y;

            if (gem.posIndex.x > 0)
            {
                if (allGems[x - 1, y] != null && allGems[x - 1, y].type == GlobalEnums.GemType.bomb)
                    bombsToDetonate.Add(allGems[x - 1, y]);
            }

            if (gem.posIndex.x + 1 < width)
            {
                if (allGems[x + 1, y] != null && allGems[x + 1, y].type == GlobalEnums.GemType.bomb)
                    bombsToDetonate.Add(allGems[x + 1, y]);
            }

            if (gem.posIndex.y > 0)
            {
                if (allGems[x, y - 1] != null && allGems[x, y - 1].type == GlobalEnums.GemType.bomb)
                    bombsToDetonate.Add(allGems[x, y - 1]);
            }

            if (gem.posIndex.y + 1 < height)
            {
                if (allGems[x, y + 1] != null && allGems[x, y + 1].type == GlobalEnums.GemType.bomb)
                    bombsToDetonate.Add(allGems[x, y + 1]);
            }
        }
    }

    public void DetonateBombs()
    {
        for (int i = bombsToDetonate.Count - 1; i >= 0; i--)
        {
            DetonateBomb(bombsToDetonate[i]);
        }
        currentMatches = currentMatches.Distinct().ToList();
    }

    private void DetonateBomb(SC_Gem p_bomb)
    {
        Vector2Int bombPos = p_bomb.posIndex;
        for (int x = bombPos.x - p_bomb.blastSize; x <= bombPos.x + p_bomb.blastSize; x++)
        {
            for (int y = bombPos.y - p_bomb.blastSize; y <= bombPos.y + p_bomb.blastSize; y++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    if (allGems[x, y] != null)
                    {
                        if (allGems[x,y].type == GlobalEnums.GemType.bomb && allGems[x,y] != p_bomb)
                        {
                            Debug.Log("NEW BOMB");
                            bombsToDetonate.Add(allGems[x, y]);
                        } else
                        {
                            allGems[x, y].isMatch = true;
                            currentMatches.Add(allGems[x, y]);
                        }
                    }
                }
            }
        }
        bombsToDetonate.Remove(p_bomb);
    }
}

