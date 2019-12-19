using UnityEngine;
using System.Collections;

public class HuntAndKillMaze
{
    protected MazeCell[,] mazeCells;
    protected int mazeRows, mazeColumns;
    private int currentRow = 0;
    private int currentColumn = 0;

    private bool courseComplete = false;
    private MonoBehaviour _mb;

    public HuntAndKillMaze(MazeCell[,] mazeCells)
    {
        this.mazeCells = mazeCells;
        mazeRows = mazeCells.GetLength(0);
        mazeColumns = mazeCells.GetLength(1);
    }

    public void CreateMaze()
    {
        HuntAndKill();
    }

    private void HuntAndKill()
    {
        mazeCells[currentRow, currentColumn].visited = true;

        while (!courseComplete)
        {
            Kill(); // Will run until it hits a dead end.
            Hunt(); // Finds the next unvisited cell with an adjacent visited cell and if it can't find any, it sets courseComplete to true
        }
    }

    private void Kill()
    {
        _mb = GameObject.FindObjectOfType<MonoBehaviour>();
        while (RouteStillAvailable(currentRow, currentColumn)) //If the RouteStillAvailable Bool returns true for the current cell then the kill process begins
        {
            _mb.StartCoroutine(CoKill());
        }
    }

    private IEnumerator CoKill()
    {

        // WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        //int direction = Random.Range (1, 5);
        int direction = ProceduralNumberGenerator.GetNextNumber();

        if (direction == 1 && CellIsAvailable(currentRow - 1, currentColumn))
        {
            // North
            // yield return new WaitForSecondsRealtime(3);
            DestroyWallIfItExists(mazeCells[currentRow, currentColumn].northWall);
            DestroyWallIfItExists(mazeCells[currentRow - 1, currentColumn].southWall);
            currentRow--;

        }


        else if (direction == 2 && CellIsAvailable(currentRow + 1, currentColumn))
        {
            // South
            //yield return new WaitForSecondsRealtime(3);
            DestroyWallIfItExists(mazeCells[currentRow, currentColumn].southWall);
            DestroyWallIfItExists(mazeCells[currentRow + 1, currentColumn].northWall);
            currentRow++;


        }
        else if (direction == 3 && CellIsAvailable(currentRow, currentColumn + 1))
        {
            // east
            //yield return new WaitForSecondsRealtime(3);
            DestroyWallIfItExists(mazeCells[currentRow, currentColumn].eastWall);
            DestroyWallIfItExists(mazeCells[currentRow, currentColumn + 1].westWall);
            currentColumn++;

        }
        else if (direction == 4 && CellIsAvailable(currentRow, currentColumn - 1))
        {
            // west
            //yield return new WaitForSecondsRealtime(3);
            DestroyWallIfItExists(mazeCells[currentRow, currentColumn].westWall);
            DestroyWallIfItExists(mazeCells[currentRow, currentColumn - 1].eastWall);
            currentColumn--;


        }
        mazeCells[currentRow, currentColumn].visited = true;
        // yield break;
        yield return new WaitForSecondsRealtime(3);

    } // End CoKill()

    /*private void Kill()
    {
        
        while (RouteStillAvailable(currentRow, currentColumn)) //If the RouteStillAvailable Bool returns true for the current cell then the kill process begins
        {
           // WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
            //int direction = Random.Range (1, 5);

            _mb = GameObject.FindObjectOfType<MonoBehaviour>();

            int direction = ProceduralNumberGenerator.GetNextNumber();

            if (direction == 1 && CellIsAvailable(currentRow - 1, currentColumn))
            {
                // North
                _mb.StartCoroutine(StepByStep());
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].northWall);
                DestroyWallIfItExists(mazeCells[currentRow - 1, currentColumn].southWall);
                currentRow--;
                _mb.StopAllCoroutines();
            }


            else if (direction == 2 && CellIsAvailable(currentRow + 1, currentColumn))
            {
                // South
                _mb.StartCoroutine(StepByStep());
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].southWall);
                DestroyWallIfItExists(mazeCells[currentRow + 1, currentColumn].northWall);
                currentRow++;
                _mb.StopAllCoroutines();

            }
            else if (direction == 3 && CellIsAvailable(currentRow, currentColumn + 1))
            {
                // east
                _mb.StartCoroutine(StepByStep());
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].eastWall);
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn + 1].westWall);
                currentColumn++;
                _mb.StopAllCoroutines();
            }
            else if (direction == 4 && CellIsAvailable(currentRow, currentColumn - 1))
            {
                // west
                _mb.StartCoroutine(StepByStep());
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].westWall);
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn - 1].eastWall);
                currentColumn--;
                _mb.StopAllCoroutines();

            }

           // yield return delay;
            mazeCells[currentRow, currentColumn].visited = true; 

        } // End While
    } // End Kill() */

    private void Hunt()
    {
        courseComplete = true; // Set it to this, and see if we can prove otherwise below

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                if (!mazeCells[r, c].visited && CellHasAnAdjacentVisitedCell(r, c))
                {
                    courseComplete = false; // Yep, we found something so definitely do another Kill cycle.
                    currentRow = r;
                    currentColumn = c;
                    DestroyAdjacentWall(currentRow, currentColumn);
                    mazeCells[currentRow, currentColumn].visited = true;
                    return; // Exit the function

                } //end if
            } // end for
        } // end for
    } // end void


    private bool RouteStillAvailable(int row, int column)
    {
        int availableRoutes = 0;

        //Sample 6X6 (0,5)
        //Cols
        //Rows        //A X X X X X  (A HERE IS 0,0)
        //X X X A X X  (A HERE IS 3,1)
        //X X X X X X
        //X X X X X X
        //X X X X X X
        //X X X X X X

        if (row > 0 && !mazeCells[row - 1, column].visited) //If the value is 0 or less then logically there is no available route, as it would be out of bounds
        {
            availableRoutes++; //If the cell North of the current cell has not been visited then the availableRoutes value increments by 1
        }

        if (row < mazeRows - 1 && !mazeCells[row + 1, column].visited) //Same as above, except it refers to the opposite direction being out of bounds
        {
            availableRoutes++; //If the cell South of the current cell has not been visited then the value increments by 1
        }

        if (column > 0 && !mazeCells[row, column - 1].visited) //Same concept as above but applied to columns
        {
            availableRoutes++; //if the cell West of the current cell has not been visited then the value increments by 1
        }

        if (column < mazeColumns - 1 && !mazeCells[row, column + 1].visited) //Same as above
        {
            availableRoutes++; //if the cell East of the current cell has not been visited then the value increments by 1
        }

        return availableRoutes > 0;
    }

    private bool CellIsAvailable(int row, int column)
    {
        if (row >= 0 && row < mazeRows && column >= 0 && column < mazeColumns && !mazeCells[row, column].visited)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DestroyWallIfItExists(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

    private bool CellHasAnAdjacentVisitedCell(int row, int column)
    {
        int visitedCells = 0;

        // Look 1 row up (north) if we're on row 1 or greater
        if (row > 0 && mazeCells[row - 1, column].visited)
        {
            visitedCells++;
        }

        // Look one row down (south) if we're the second-to-last row (or less)
        if (row < (mazeRows - 2) && mazeCells[row + 1, column].visited)
        {
            visitedCells++;
        }

        // Look one row left (west) if we're column 1 or greater
        if (column > 0 && mazeCells[row, column - 1].visited)
        {
            visitedCells++;
        }

        // Look one row right (east) if we're the second-to-last column (or less)
        if (column < (mazeColumns - 2) && mazeCells[row, column + 1].visited)
        {
            visitedCells++;
        }

        // return true if there are any adjacent visited cells to this one
        return visitedCells > 0;
    }

    private void DestroyAdjacentWall(int row, int column)
    {
        bool wallDestroyed = false;
        //_mb = GameObject.FindObjectOfType<MonoBehaviour>();

        while (!wallDestroyed)
        {
            int direction = Random.Range(1, 5);
            // int direction = ProceduralNumberGenerator.GetNextNumber();

            if (direction == 1 && row > 0 && mazeCells[row - 1, column].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].northWall);
                DestroyWallIfItExists(mazeCells[row - 1, column].southWall);
                wallDestroyed = true;
            }
            else if (direction == 2 && row < (mazeRows - 2) && mazeCells[row + 1, column].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].southWall);
                DestroyWallIfItExists(mazeCells[row + 1, column].northWall);
                wallDestroyed = true;
            }
            else if (direction == 3 && column > 0 && mazeCells[row, column - 1].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].westWall);
                DestroyWallIfItExists(mazeCells[row, column - 1].eastWall);
                wallDestroyed = true;
            }
            else if (direction == 4 && column < (mazeColumns - 2) && mazeCells[row, column + 1].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].eastWall);
                DestroyWallIfItExists(mazeCells[row, column + 1].westWall);
                wallDestroyed = true;
            }


        }
    }

    IEnumerator StepByStep()
    {
        yield return new WaitForSecondsRealtime(3);

    }

}