using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisplacedGemsHandler : MonoBehaviour
{
    [SerializeField] GameBoardReference _gameBoardReference;
    public void CheckMisplacedGems()
    {
        List<SC_Gem> foundGems = new List<SC_Gem>();
        foundGems.AddRange(FindObjectsOfType<SC_Gem>());
        for (int x = 0; x < _gameBoardReference.gameBoard.Width; x++)
        {
            for (int y = 0; y < _gameBoardReference.gameBoard.Height; y++)
            {
                SC_Gem _curGem = _gameBoardReference.gameBoard.GetGem(x, y);
                if (foundGems.Contains(_curGem))
                    foundGems.Remove(_curGem);
            }
        }

        foreach (SC_Gem g in foundGems)
            g.objectPoolController.ReturnObject(g.gameObject);
    }
}
