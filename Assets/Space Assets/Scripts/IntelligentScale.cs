using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligentScale : MonoBehaviour
{
    // Position on Grid, Initialized to -1 -1
    public Vector2 gridPos = new Vector2(-1, -1);

    // Dimensions of Grid
    public Vector2 gridSize = new Vector2(-1, -1);

    // Cell Size
    public float cellSize;

    public Vector3 newScale;

    void Start()
    {
        // Get Grid Cell Size
        float cellSize = GridGen.cellSize;

        // Standardization (for safety when rescaling)
        Vector3 newScale = new Vector3(1, 1, 1);
        newScale.y = 0.37f;

        if (this.name.StartsWith("Head") || this.name.StartsWith("Foot"))
            newScale.x *= 3;

        // Rescale and Flag
        newScale *= cellSize;
        this.transform.localScale = newScale;
    }

    // Set the CoordinateLocation component of each code block to its coordinate location on the grid
    public void SetGridPosNums()
    {
        gridPos =
            this
                .transform
                .parent
                .gameObject
                .GetComponent<CoordinateLocation>()
                .coordinates;

        gridSize =
            new Vector2(this
                    .transform
                    .parent
                    .parent
                    .GetComponent<GridGen>()
                    .height,
                this.transform.parent.parent.GetComponent<GridGen>().width);
    }

    public void ResetGridPosNums()
    {
        gridPos = new Vector2(-1, -1);
    }
}
