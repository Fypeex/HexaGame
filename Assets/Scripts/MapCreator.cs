using System.Collections.Generic;
using Game;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [field: SerializeField] public int MapRadius { get; private set; } = 5;
    [field: SerializeField] public int HexSize { get; private set; } = 1;
    [field: SerializeField] public GameObject HexPrefab { get; private set; }

    private readonly List<int> _startTiles = new()
    {
        2, 2, 2, 2, 2, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 7, 7, 7, 7, 7
    };

    private int _prevMapSize;

    void Start()
    {
        int count = 0;
        for (int x = -MapRadius; x <= MapRadius; x++)
        {
            for (int y = -MapRadius; y <= MapRadius; y++)
            {
                if (IsInRadius(x, y))
                    count++;

                if (_startTiles.Count < count) _startTiles.Add(Random.Range(0, 2));
            }
        }

        CreateHexGrid();
    }

    private void OnDestroy()
    {
        DestroyHexGrid();
    }

    private void CreateHexGrid()
    {
        for (int i = 0; i <= 10; i++)
        {
            int count = _startTiles.FindAll((k) => k == i).Count;
        }

        for (int x = -MapRadius; x <= MapRadius; x++)
        {
            for (int y = -MapRadius; y <= MapRadius; y++)
            {
                if (IsInRadius(x, y))
                {
                    var center = HexMetrics.Center(x, y, HexSize / 2f);
                    HexTile tile = Instantiate(HexPrefab, center, Quaternion.identity, transform)
                        .GetComponent<HexTile>();

                    int rand = Random.Range(0, _startTiles.Count);

                    tile.SetType((Resourcetype)_startTiles[rand]);
                    _startTiles.RemoveAt(rand);

                    GameManager.Instance.AddTile(tile);
                }
            }
        }
    }


    private void DestroyHexGrid()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("Destroying " + child.name);
            Destroy(child.gameObject);
        }
    }

    private void AddTiles()
    {
        for (int x = -MapRadius; x <= MapRadius; x++)
        {
            for (int y = -MapRadius; y <= MapRadius; y++)
            {
                Debug.Log($"X:{x} Y:{y}");
                if (IsInRadius(x, y))
                {
                    var center = HexMetrics.Center(x, y, HexSize / 2f);
                    HexTile tile = Instantiate(HexPrefab, center, Quaternion.identity, transform)
                        .GetComponent<HexTile>();

                    GameManager.Instance.AddTile(tile);
                }
                //if (y == -_prevMapSize) y = _prevMapSize;
            }

            //if (x == -_prevMapSize) x = _prevMapSize;
        }
    }

    private void RemoveTiles()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("Destroying " + child.name + " " + child.position);
            Destroy(child.gameObject);
        }
    }

    private bool IsInRadius(int x, int y)
    {
        Vector3[] corners = HexMetrics.Corners(HexSize);

        //Check if any corner is in the radius
        foreach (var corner in corners)
        {
            if (Mathf.Abs(Vector3.Distance(HexMetrics.Center(x, y, HexSize), corner)) <= MapRadius * HexSize)
            {
                return true;
            }
        }

        return false;
    }
}