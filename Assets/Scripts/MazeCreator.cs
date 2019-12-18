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

    }

    // Update is called once per frame
    void Update()
    {
    }