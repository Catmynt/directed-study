using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowGridGen : MonoBehaviour
{
    public int width = 1;

    public int height = 1;

    public static float cellSize = -1f;

    public List<List<GameObject>> cells;

    private GameObject window;

    void Awake()
    {
        window = gameObject;

        // Instantiate 2d List of Empty Gameobjects
        cells = new List<List<GameObject>>();

        // Parent Plane Mesh
        var windowMesh = window.GetComponent<MeshFilter>();

        // Parent Plane Mesh, localScale accounted for
        var m_Size =
            new Vector3(windowMesh.mesh.bounds.size.x *
                window.transform.localScale.x,
                windowMesh.mesh.bounds.size.y * window.transform.localScale.y,
                windowMesh.mesh.bounds.size.z * window.transform.localScale.z);

        // Counter for Naming Grid Nodes in the Heirarchy
        int name = 0;

        // Cell Size
        cellSize = m_Size.z / width;

        // Height Curtailing
        if ((float)(m_Size.x / cellSize) != (float) height)
        {
            print("Too big for the window~");
            if (height * cellSize > m_Size.x)
                height = Mathf.FloorToInt(m_Size.x / cellSize);
        }

        // Position of the Top Left Corner of the Plane
        var cornerPos =
            window.transform.position +
            window.transform.right * (m_Size.x / 2f) +
            -window.transform.right * cellSize / 2f +
            window.transform.forward * (m_Size.z / 2f) +
            -window.transform.forward * cellSize / 2f;

        // Create Grid
        for (int row = 0; row < height; ++row)
        {
            List<GameObject> rowCells = new List<GameObject>();

            for (int col = 0; col < width; ++col)
            {
                // transform.right (red arrow)(this is distance from window, shouldn't change)
                // width is -transform.forward (z)(blue arrow)(left to right), height is transform.up (green arrow)
                Vector3 pos =
                    cornerPos +
                    (col * cellSize) * -window.transform.forward +
                    (row * cellSize) * -window.transform.right +
                    window.transform.up * 0.001f;

                GameObject obj = new GameObject("Cell " + name++);
                obj.transform.position = pos;
                obj.transform.rotation = window.transform.rotation;
                obj.transform.parent = this.transform;

                //Cell cell = new Cell(obj, new Vector2(row, col));
                // CoordinateLocation coordLoc =
                //     obj.GetComponent<CoordinateLocation>();
                // coordLoc.coordinates = new Vector2(row, col);
                rowCells.Add (obj);
            }
            cells.Add (rowCells);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
