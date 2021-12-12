using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScale : MonoBehaviour
{
    // Cell Size
    public float cellSize;

    private Vector3 newScale;

    void Start()
    {
        // Get Grid Cell Size
        cellSize = WindowGridGen.cellSize;

        // Standardization (for safety when rescaling)
        Vector3 newScale = new Vector3(1, 1, 1);

        // Rescale (robot will be 80% of window cell size)
        newScale *= cellSize * 0.8f;

        this.transform.localScale = newScale;
    }

    public float getCellSize()
    {
        return cellSize;
    }

    public Vector3 getNewScale()
    {
        return newScale;
    }
}
