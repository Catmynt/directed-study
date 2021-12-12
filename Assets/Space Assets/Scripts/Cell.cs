using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public GameObject square;

    public Vector2 gridPos = new Vector2();

    public Cell(GameObject square, Vector2 gridPos)
    {
        this.square = square;
        this.gridPos = gridPos;
        //Debug.Log(square.name + " Pos: " + this.gridPos.ToString());
    }
}
