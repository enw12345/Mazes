using System.Collections;
using UnityEngine;

public class InitializeMaze : MonoBehaviour
{
    public float stepSpeed = .1f;
    public bool step;

    [Range(2, 10)]
    public int sizeX = 4;
    [Range(2, 10)]
    public int sizeY = 4;

    public Transform wallContainer;
    public GameObject wall;
    public GameObject plane;
    private Bounds wallBounds;
    private Vector3 planeCenter;

    public enum MazeTypes { Binary, Sidewinder };
    public MazeTypes mazeTypes;

    private IMazeAlgorithm mazeAlgorithm;

    // Start is called before the first frame update
    void Awake()
    {
        wallBounds = wall.GetComponent<MeshFilter>().sharedMesh.bounds;
        planeCenter = transform.TransformPoint(plane.GetComponent<MeshFilter>().sharedMesh.bounds.center);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grid.ClearGridLayout(wallContainer);

            GenerateMazeType();

            if (!step)
                InitializeMazeNormal();
            else
                StartCoroutine(InitalizeMazeStep(stepSpeed));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Grid.ClearGridLayout(wallContainer);
        }
    }

    IEnumerator InitalizeMazeStep(float stepSpeed)
    {
        yield return StartCoroutine(Grid.CreateGridStep(wallContainer, wall, wallBounds, planeCenter, stepSpeed, sizeX, sizeY));
        StartCoroutine(mazeAlgorithm.GenerateMazeStep(stepSpeed));
    }

    void InitializeMazeNormal()
    {
        Grid.CreateGrid(wallContainer, wall, wallBounds, planeCenter, sizeX, sizeY);
        mazeAlgorithm.GenerateMaze();
    }

    private void GenerateMazeType()
    {
        if (mazeTypes == MazeTypes.Binary)
        {
            mazeAlgorithm = new BinaryMazeAlgorithm();
        }
        else if(mazeTypes == MazeTypes.Sidewinder)
        {
            mazeAlgorithm = new SidewinderMazeAlgorithm();
        }
    }
}
