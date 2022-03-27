using UnityEngine;
using System.Collections.Generic;

public class CpuRandom : ICpuCore
{
    public Vector2Int GetNextMove(GameTable table){
        List<Vector2Int> emptyCell = table.GetEmptyCell();

        int nextMove = Random.Range(0, emptyCell.Count);

        Debug.Log(emptyCell[nextMove]);

        return emptyCell[nextMove];
    }
}
