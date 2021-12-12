using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CodeReader : MonoBehaviour
{
    // This is the empty gameobject that holds the GridGen script.
    [SerializeField]
    GridGen gridGenerator;

    // Delay Between Instruction Execution
    public float instructionDelay = 0.5f;

    // Materials for Active/Inactive Code Blocks
    public Material inactiveIfMaterial;

    public Material activeIfMaterial;

    public Material inactiveWhileMaterial;

    public Material activeWhileMaterial;

    public Material inactiveTestMaterial;

    public Material activeTestMaterial;

    public Material inactiveBotMaterial;

    public Material activeBotMaterial;

    private int currentCol = 0;

    // Board Cells
    private static List<List<GameObject>> grid;

    // Active Sockets
    public static int[,] activeSockets;

    void Start()
    {
        // Get 2D List of Grid Cells
        grid = gridGenerator.tiles;

        // Create List of Active Sockets (First Column Only to Start)
        activeSockets = new int[grid.Count, grid[0].Count];

        for (int i = 0; i < grid.Count; i++)
        {
            activeSockets[i, 0] = 1;
        }

        // Update Sockets
        updateSockets (activeSockets);
    }

    // Updates Sockets
    public static void updateSockets(int[,] a)
    {
        print("Running updateSockets");

        // Rows
        for (int i = 0; i < a.GetLength(0); i++)
        {
            // Cols
            for (int j = 0; j < a.GetLength(1); j++)
            {
                grid[i][j]
                    .transform
                    .GetComponentInChildren<SocketWithTagCheck>()
                    .socketActive = a[i, j] == 1 ? true : false;
            }
        }
        activeSockets = a;
    }

    // Returns GameObject in Socket at Position position, or null if none
    public XRBaseInteractable getAttached(Vector2 position)
    {
        return grid[(int) position[0]][(int) position[1]]
            .transform
            .GetComponentInChildren<SocketWithTagCheck>()
            .selectTarget;
    }

    // Wrapper Function for Run Code
    public void runCode()
    {
        StartCoroutine(runCodeWithDelay());
    }

    IEnumerator runCodeWithDelay()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            // Get GameObject in Socket
            XRBaseInteractable cellContent =
                getAttached(new Vector2(i, currentCol));

            //Debug.Log("Checking row " + i.ToString());
            Debug.Log("Checking (" + i + ", " + currentCol + ")");

            // If CodeBlock found
            if (cellContent != null)
            {
                Debug.Log("Found a code block: " + cellContent.name);

                // Get CodeBlock mesh for material change
                MeshRenderer mesh = cellContent.GetComponent<MeshRenderer>();

                // Change Material to activeMaterial
                activateCodeBlock (cellContent, mesh);

                // Do Actual Code Execution
                if (cellContent.name.Contains("Bot"))
                {
                    if (cellContent.name.Contains("Left"))
                    {
                        cellContent
                            .transform
                            .parent
                            .parent
                            .GetComponent<IHoldARobot>()
                            .robot
                            .GetComponent<RobotBrain>()
                            .turnLeft();
                    }
                    else if (cellContent.name.Contains("Right"))
                    {
                        cellContent
                            .transform
                            .parent
                            .parent
                            .GetComponent<IHoldARobot>()
                            .robot
                            .GetComponent<RobotBrain>()
                            .turnRight();
                    }
                    else if (cellContent.name.Contains("Move"))
                    {
                        cellContent
                            .transform
                            .parent
                            .parent
                            .GetComponent<IHoldARobot>()
                            .robot
                            .GetComponent<RobotBrain>()
                            .moveForward();
                    }
                    else
                    {
                    }
                }

                // Wait
                yield return new WaitForSeconds(instructionDelay);

                // Change Material to inactiveMaterial
                deactivateCodeBlock (cellContent, mesh);

                // Wait Again (Looks better)
                yield return new WaitForSeconds(0.1f);

                // Move Reader to next col
                if (
                    cellContent.name.StartsWith("Head") &&
                    !cellContent.name.Contains("Example") &&
                    !cellContent.name.Contains("Bot")
                )
                {
                    currentCol++;
                }
            }
            else
            {
                yield return new WaitForSeconds(instructionDelay);
            }

            // Move Reader to previous col
            if (currentCol != 0)
            {
                XRBaseInteractable possiblePrevious =
                    getAttached(new Vector2(i, currentCol - 1));
                if (
                    possiblePrevious != null &&
                    possiblePrevious.name.StartsWith("Foot")
                )
                {
                    Debug.Log("found a footer");
                    deactivateCodeBlock(possiblePrevious,
                    possiblePrevious.GetComponent<MeshRenderer>());
                    currentCol--;
                }
            }
        }
    }

    // Activate CodeBlock Material Switch
    public void activateCodeBlock(XRBaseInteractable block, MeshRenderer mesh)
    {
        // Determines Correct Material Type
        Material tempMaterial = null;
        if (block.name.Contains("If"))
        {
            tempMaterial = activeIfMaterial;
        }
        else if (block.name.Contains("While"))
        {
            tempMaterial = activeWhileMaterial;
        }
        else if (block.name.Contains("Example"))
        {
            tempMaterial = activeTestMaterial;
        }
        else if (block.name.Contains("Bot"))
        {
            tempMaterial = activeBotMaterial;
        }

        // Sets Entire Enclosure Material at Once
        if (
            block.name.StartsWith("Head") &&
            !block.name.Contains("Example") &&
            !block.name.Contains("Bot")
        )
        {
            print("trying to make enclosure anyway");
            mesh.material = tempMaterial;
            foreach (GameObject
                item
                in
                block.GetComponent<CodeBarCreator>().enclosureBlocks
            )
            {
                item.GetComponent<MeshRenderer>().material = tempMaterial;
            }
        }
        else
        {
            mesh.material = tempMaterial;
        }
    }

    // Deactivate CodeBlock Material Switch
    public void deactivateCodeBlock(XRBaseInteractable block, MeshRenderer mesh)
    {
        // Determines Correct Material Type
        Material tempMaterial = null;
        if (block.name.Contains("If"))
        {
            tempMaterial = inactiveIfMaterial;
        }
        else if (block.name.Contains("While"))
        {
            tempMaterial = inactiveWhileMaterial;
        }
        else if (block.name.Contains("Example"))
        {
            tempMaterial = inactiveTestMaterial;
        }
        else if (block.name.Contains("Bot"))
        {
            tempMaterial = inactiveBotMaterial;
        }

        // Sets Entire Enclosure Material at Once
        if (block.name.StartsWith("Foot"))
        {
            Vector2 headerPos =
                new Vector2(block.GetComponent<IntelligentScale>().gridPos[0] -
                    3, //can't get barHeight in here, fix later
                    block.GetComponent<IntelligentScale>().gridPos[1]);

            List<GameObject> blocks =
                getAttached(headerPos)
                    .GetComponent<CodeBarCreator>()
                    .enclosureBlocks;

            foreach (GameObject item in blocks)
            {
                item.GetComponent<MeshRenderer>().material = tempMaterial;
            }
            getAttached(headerPos).GetComponent<MeshRenderer>().material =
                tempMaterial;
        }
        else if (
            !block.name.StartsWith("Head") ||
            block.name.Contains("Example") ||
            block.name.Contains("Bot")
        )
        {
            mesh.material = tempMaterial;
        }
    }

    // Returns Grid
    public static List<List<GameObject>> getGrid()
    {
        return grid;
    }

    // Returns 2D Array of Active Sockets
    public static int[,] getActiveSockets()
    {
        return activeSockets;
    }
}
