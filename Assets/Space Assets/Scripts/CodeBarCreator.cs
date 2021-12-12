using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBarCreator : MonoBehaviour
{
    // Ya like public variables? me too
    private GameObject block;

    public GameObject ifSquarePrefab;

    public GameObject whileSquarePrefab;

    public GameObject ifFootPrefab;

    public GameObject whileFootPrefab;

    public int barHeight = 3;

    public List<GameObject> enclosureBlocks;

    private IntelligentScale intelligentScale;

    private float cellSize;

    // aaaaaaaaaaa
    public List<List<GameObject>> grid;

    public int[,] activeSockets;

    void Start()
    {
        block = this.transform.gameObject;
        intelligentScale = block.GetComponent<IntelligentScale>();
        cellSize = GridGen.getCellSize();
    }

    // Creates Vertical and Footer Bars
    public void createEnclosure()
    {
        // If the enclosure will fit on the board (vertically)
        if (
            intelligentScale.gridSize[0] >
            intelligentScale.gridPos[0] + barHeight
        )
        {
            activeSockets = CodeReader.getActiveSockets();
            grid = CodeReader.getGrid();

            // If the sockets for the vertical and footer bars aren't active or are unavailable
            for (int i = 0; i < barHeight; i++)
            {
                if (
                    activeSockets[(int) intelligentScale.gridPos[0] + i + 1,
                    (int) intelligentScale.gridPos[1]] !=
                    1 ||
                    grid[(int) intelligentScale.gridPos[0] + i + 1][(int)
                    intelligentScale.gridPos[1]]
                        .GetComponentInChildren<SocketWithTagCheck>()
                        .selectTarget !=
                    null
                )
                {
                    return;
                }
            }

            GameObject codeSquare = null;
            GameObject codeFoot = null;

            // If they are active
            if (block.name.StartsWith("Head If"))
            {
                codeSquare = ifSquarePrefab;
                codeFoot = ifFootPrefab;
            }
            else if (block.name.StartsWith("Head While"))
            {
                codeSquare = whileSquarePrefab;
                codeFoot = whileFootPrefab;
            }
            else
            {
            }

            // Place Appropriate Code Blocks in Below sockets, then update the activeSockets
            // Vertical Bar
            for (int i = 0; i < barHeight - 1; i++)
            {
                GameObject sideBlock =
                    Instantiate(codeSquare,
                    grid[(int) intelligentScale.gridPos[0] + i + 1][(int)
                    intelligentScale.gridPos[1]].transform.position +
                    grid[0][0].transform.up * 1 / 32f,
                    this.transform.rotation);

                // Add block to enclosureBlocks
                enclosureBlocks.Add (sideBlock);

                // Add right socket to activeSockets
                if (
                    intelligentScale.gridSize[1] >
                    intelligentScale.gridPos[1] + 1
                )
                {
                    activeSockets[(int) intelligentScale.gridPos[0] + i + 1,
                    (int) intelligentScale.gridPos[1] + 1] = 1;
                }
            }

            // Footer Bar
            GameObject footBlock =
                Instantiate(codeFoot,
                grid[(int) intelligentScale.gridPos[0] + barHeight][(int)
                intelligentScale.gridPos[1] +
                1].transform.position +
                grid[0][0].transform.up * 1 / 32f,
                this.transform.rotation);

            //grid[(int) intelligentScale.gridPos[0] + barHeight][(int) cintelligentScale.gridPos[1] +1]
            // Add block to enclosureBlocks
            enclosureBlocks.Add (footBlock);
        }

        // Update activeSockets
        CodeReader.updateSockets (activeSockets);
    }

    public void removeEnclosure()
    {
        List<GameObject> blocks = new List<GameObject>();
        foreach (GameObject b in enclosureBlocks)
        {
            blocks.Add (b);
        }
        foreach (GameObject b in blocks)
        {
            enclosureBlocks.Remove (b);
            GameObject.Destroy (b);
        }

        if (intelligentScale.gridPos[0] != -1)
        {
            // Disable Inner Sockets
            for (int i = 0; i < barHeight - 1; i++)
            {
                activeSockets[(int) intelligentScale.gridPos[0] + i + 1,
                (int) intelligentScale.gridPos[1] + 1] = 0;
            }

            // Disable Enclosure Sockets
            for (int i = 0; i < barHeight; i++)
            {
                if (intelligentScale.gridPos[1] != 0)
                {
                    activeSockets[(int) intelligentScale.gridPos[0] + i + 1,
                    (int) intelligentScale.gridPos[1]] = 0;
                }
            }

            // Update activeSockets
            CodeReader.updateSockets (activeSockets);

            // Delete Enclosure Blocks
            blocks.ForEach(child => Destroy(child));
        }
    }
}
