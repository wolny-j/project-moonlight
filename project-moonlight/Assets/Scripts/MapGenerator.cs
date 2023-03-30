using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*public class MapGenerator : MonoBehaviour
{
    [SerializeField]static int row = 10;
    [SerializeField]static int col = 10;
    [SerializeField] int maxSegments = 6;
    [SerializeField] List<GameObject> segments = new List<GameObject>();
    [SerializeField] List<GameObject> endingSegments = new List<GameObject>();
    Queue<Vector2Int> segmentStack = new Queue<Vector2Int>();


    private GameObject[,] mapGrid = new GameObject[row, col];
    // Start is called before the first frame update
    void Start()
    {

        
        for (int segmentsCount = 0; segmentsCount < maxSegments; segmentsCount++)
        {
           
            if (segmentsCount == 0)
            {
                int x = row / 2;
                int y = col / 2;
                mapGrid[x, y] = segments[Random.Range(0, segments.Count - 1)];
                segmentStack.Enqueue(new Vector2Int(x, y));
            }
            else
            {
                Vector2Int lastPosition = segmentStack.Dequeue();
                Segment currentSegment = mapGrid[lastPosition.x, lastPosition.y].GetComponent<Segment>();
                if (currentSegment.top == true)
                {
                    GameObject segment;
                    do
                    {
                        segment = segments[Random.Range(0, segments.Count - 1)];
                    } while (segment.GetComponent<Segment>().bottom == true);
                    mapGrid[lastPosition.x + 1, lastPosition.y] = segment;
                    segmentStack.Enqueue(new Vector2Int(lastPosition.x + 1, lastPosition.y));
                }
                if (currentSegment.bottom == true)
                {
                    GameObject segment;
                    do
                    {
                        segment = segments[Random.Range(0, segments.Count - 1)];
                    } while (segment.GetComponent<Segment>().top == true);
                    mapGrid[lastPosition.x - 1, lastPosition.y] = segment;
                    segmentStack.Enqueue(new Vector2Int(lastPosition.x - 1, lastPosition.y));
                }
                if (currentSegment.right == true)
                {
                    GameObject segment;
                    do
                    {
                        segment = segments[Random.Range(0, segments.Count - 1)];
                    } while (segment.GetComponent<Segment>().left == true);
                    mapGrid[lastPosition.x, lastPosition.y + 1] = segment;
                    segmentStack.Enqueue(new Vector2Int(lastPosition.x, lastPosition.y + 1));
                }
                if (currentSegment.left == true)
                {
                    GameObject segment;
                    do
                    {
                        segment = segments[Random.Range(0, segments.Count - 1)];
                    } while (segment.GetComponent<Segment>().right == true);
                    mapGrid[lastPosition.x, lastPosition.y -1] = segment;
                    segmentStack.Enqueue(new Vector2Int(lastPosition.x, lastPosition.y - 1));
                }
            }
        }

        
    }*/

  /*  public class MapGenerator : MonoBehaviour
    {
        [SerializeField] int numRows = 10;
        [SerializeField] int numCols = 10;
        [SerializeField] int maxSegments = 6;
        [SerializeField] List<GameObject> segments = new List<GameObject>();
        [SerializeField] List<GameObject> endingSegments = new List<GameObject>();

        private GameObject[,] mapGrid;
        private Queue<Vector2Int> segmentQueue;

        private void Awake()
        {
            mapGrid = new GameObject[numRows, numCols];
            segmentQueue = new Queue<Vector2Int>();
        }

        private void Start()
        {
            int startRow = numRows / 2;
            int startCol = numCols / 2;
            CreateSegment(startRow, startCol);
            segmentQueue.Enqueue(new Vector2Int(startRow, startCol));

            for (int i = 1; i < maxSegments; i++)
            {
                Vector2Int currentPosition = segmentQueue.Dequeue();
                Segment currentSegment = mapGrid[currentPosition.x, currentPosition.y].GetComponent<Segment>();

            if (currentSegment.top == true)
            {
                CreateAdjacentSegment(currentPosition.x + 1, currentPosition.y, SegmentDirection.Bottom);
            }
            if (currentSegment.bottom == true)
            {
                CreateAdjacentSegment(currentPosition.x - 1, currentPosition.y, SegmentDirection.Top);
            }
            if (currentSegment.right == true)
            {
                CreateAdjacentSegment(currentPosition.x, currentPosition.y + 1, SegmentDirection.Left);
            }
            if (currentSegment.left == true)
            {
                CreateAdjacentSegment(currentPosition.x, currentPosition.y - 1, SegmentDirection.Right);
            }
        }
        }

        private void CreateSegment(int row, int col)
        {
            GameObject segment = Instantiate(segments[Random.Range(0, segments.Count)], transform);
            segment.transform.position = new Vector3(row, col, 0);
            Debug.Log($"{segment.name}");
            mapGrid[row, col] = segment;
        }

    private void CreateAdjacentSegment(int row, int col, SegmentDirection direction)
    {
        if (mapGrid[row, col] != null)
        {
            return; // don't create segment if one already exists in this position
        }

        GameObject segment;
        switch (direction)
        {
            case SegmentDirection.Top:
                do
                {
                    segment = segments[Random.Range(0, segments.Count - 1)];
                } while (segment.GetComponent<Segment>().top == true);
                Debug.Log($"{segment.name}");
               // Instantiate(segment, transform);
                mapGrid[row, col] = segment;
                break;
            case SegmentDirection.Bottom:
                do
                {
                    segment = segments[Random.Range(0, segments.Count - 1)];
                } while (segment.GetComponent<Segment>().bottom == true);
                Debug.Log($"{segment.name}");
                //Instantiate(segment, transform);
                mapGrid[row, col] = segment;
                break;
            case SegmentDirection.Left:
                do
                {
                    segment = segments[Random.Range(0, segments.Count - 1)];
                } while (segment.GetComponent<Segment>().left == true);
                Debug.Log($"{segment.name}");
                //Instantiate(segment, transform);
                mapGrid[row, col] = segment;
                break;
            case SegmentDirection.Right:
                do
                {
                    segment = segments[Random.Range(0, segments.Count - 1)];
                } while (segment.GetComponent<Segment>().right == true);
                Debug.Log($"{segment.name}");
                //Instantiate(segment, transform);
                mapGrid[row, col] = segment;
                break;
        }

        segmentQueue.Enqueue(new Vector2Int(row, col));

    }

    private enum SegmentDirection
        {
            Top,
            Bottom,
            Left,
            Right
        }
    }*/


