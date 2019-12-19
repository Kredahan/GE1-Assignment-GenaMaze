using UnityEngine;
using System.Collections;

public class MazeCreator : MonoBehaviour
{
    public int mazeRows, mazeColumns; //Maze Rows and Columns are represented by numeric values
    public GameObject[] wall; // A Unity Asset (Which must be created manually) is used to actually create grid which will become the maze (After walls are destroyed to create a path)
    public float size = 2f;
    public Camera Bird;


    private MazeCell[,] mazeCells;

    // Use this for initialization
    void Start()
    {

        InitializeMaze(); // Function found in this class that creates the default grid used to later make the maze

       HuntAndKillMaze ma = new HuntAndKillMaze(mazeCells); 
       ma.CreateMaze();
       Bird.clearFlags = CameraClearFlags.Depth;
       Bird.rect = new Rect(0f, 0f, 0.3f, 0.3f);

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void InitializeMaze()
    {

        mazeCells = new MazeCell[mazeRows, mazeColumns];

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                mazeCells[r, c] = new MazeCell();
                //wall[Random.Range(0, wall.Length)]
                // mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor = Instantiate(wall[0], new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);

                if (c == 0)
                {
                    mazeCells[r, c].westWall = Instantiate(wall[Random.Range(1, wall.Length)], new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                }

                mazeCells[r, c].eastWall = Instantiate(wall[Random.Range(1, wall.Length)], new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall[Random.Range(1, wall.Length)], new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

                mazeCells[r, c].southWall = Instantiate(wall[Random.Range(1, wall.Length)], new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
            }
        }
    }
}