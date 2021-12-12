using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridGen : MonoBehaviour
{
    public GameObject gridSquare;

    public GameObject parentPlane;

    public int width = 1;

    public int height = 1;

    public static float cellSize = -1f;

    private static float trueCellSize = 0f;

    public List<List<GameObject>> tiles;

    void Awake()
    {
        //Time.timeScale = 1f;
        // Instantiate 2d List of Tiles
        tiles = new List<List<GameObject>>();

        // Parent Plane Mesh
        var planeMesh = parentPlane.GetComponent<MeshFilter>();

        // Parent Plane Mesh, localScale accounted for
        var m_Size =
            new Vector3(planeMesh.mesh.bounds.size.x *
                parentPlane.transform.localScale.x,
                planeMesh.mesh.bounds.size.y *
                parentPlane.transform.localScale.y,
                planeMesh.mesh.bounds.size.z *
                parentPlane.transform.localScale.z);

        // Counter for Naming Grid Cells in the Heirarchy
        int name = 0;

        // Cell Size
        cellSize = m_Size.z / width;

        trueCellSize = cellSize;

        // Height Curtailing
        if ((float)(m_Size.x / cellSize) != (float) height)
        {
            print("Too big~");
            if (height * cellSize > m_Size.x)
                height = Mathf.FloorToInt(m_Size.x / cellSize);
        }

        // Position of the Top Left Corner of the Plane
        var cornerPos =
            parentPlane.transform.position +
            parentPlane.transform.right * (m_Size.x / 2f) +
            -parentPlane.transform.right * cellSize / 2f +
            parentPlane.transform.forward * (m_Size.z / 2f) +
            -parentPlane.transform.forward * cellSize / 2f;

        // Create Grid
        for (int row = 0; row < height; ++row)
        {
            List<GameObject> rowCells = new List<GameObject>();

            for (int col = 0; col < width; ++col)
            {
                // For the plane in the scene, rows are -transform.right (red arrow)(top to bottom)
                // and columns are -transform.forward (z)(blue arrow)(left to right), depth is transform.up (green arrow)
                Vector3 pos =
                    cornerPos +
                    (col * cellSize) * -parentPlane.transform.forward +
                    (row * cellSize) * -parentPlane.transform.right +
                    parentPlane.transform.up * 0.001f;

                GameObject obj =
                    Instantiate(gridSquare,
                    pos,
                    parentPlane.transform.rotation);

                obj.transform.localScale *= cellSize;

                obj.name = "Square " + name++;

                obj.transform.parent = this.transform;

                //Cell cell = new Cell(obj, new Vector2(row, col));
                CoordinateLocation coordLoc =
                    obj.GetComponent<CoordinateLocation>();
                coordLoc.coordinates = new Vector2(row, col);

                rowCells.Add (obj);
            }
            tiles.Add (rowCells);
        }
    }

    void Update()
    {
    }

    public static float getCellSize()
    {
        return trueCellSize;
    }
}
