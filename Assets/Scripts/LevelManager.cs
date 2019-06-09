using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manager for game lever, sets and create every level attributes
/// </summary>
public class LevelManager : SingletonInstance<LevelManager>
{
    [SerializeField]
    private CameraMovement _cameraMovement;

    [SerializeField]
    private List<GameObject> _levelCollection;

    [SerializeField]
    private Transform _map;

    private string[] _currentLvl;

    private Point _simplePortPosition;

    private List<Vector3> _pathPoints = new List<Vector3>();

    [SerializeField]
    private GameObject _simplePortalPrefab;

    /// <summary>
    /// Gets created a path for monster
    /// </summary>
    public List<Vector3> Path { get; private set; }

    /// <summary>
    /// Gets a map tiles to create a map
    /// </summary>
    public Dictionary<Point, TileScript> Tiles { get; set; }

    /// <summary>
    /// Gets a spawn portal
    /// </summary>
    public PortalSpawn RatPortal { get; set; }

    public float TileSizeX => 
        _levelCollection[0].GetComponent<SpriteRenderer>().bounds.size.x;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        _currentLvl = ReadLevelData();
        var maxTile = Vector3.zero;
        var wrt = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < _currentLvl.Length; y++)
        {
            for (int x = 0; x < _currentLvl[y].Length; x++)
            {
                PlaceTitle(_currentLvl[y][x], x, y, wrt);
            }
        }
        maxTile = Tiles.Last().Value.transform.position;
        _cameraMovement.SetLimits(new Vector3(maxTile.x + TileSizeX, maxTile.y - TileSizeX));
        GenerateMonsterPath();
        PortalSpawn();
    }

    private void PlaceTitle(char levelInfo, int x, int y, Vector3 wrt)
    {
        var parsedTitle = int.Parse(levelInfo.ToString());
        TileScript nTile = Instantiate(_levelCollection[parsedTitle]).GetComponent<TileScript>();

        var p = new Point(x, y);

        var v = new Vector3(wrt.x + (TileSizeX * x), wrt.y - (TileSizeX * y), 0);
        nTile.Setup(_map, p, v);
        if (parsedTitle != 0)
        {
            if (_pathPoints.Count == 0)
                _simplePortPosition = p;

            _pathPoints.Add(v);
        }
    }

    private string[] ReadLevelData()
    {
        var levelData = Resources.Load("level") as TextAsset;
        return levelData.text.Replace(Environment.NewLine, string.Empty).Split('-');
    }

    private void PortalSpawn()
    {
        var fp = _pathPoints.FirstOrDefault();

        GameObject portal = Instantiate(_simplePortalPrefab, Tiles[_simplePortPosition]
            .GetComponent<TileScript>().WorldPos, Quaternion.identity);

        RatPortal = portal.GetComponent<PortalSpawn>();
        RatPortal.name = "RatPortal";
    }

    public void GenerateMonsterPath()
    {
        var tempPath = _pathPoints.ToList();
        var fixedPath = new List<Vector3>() { tempPath[0] };
        tempPath.RemoveAt(0);
        var directions = new List<Vector3>();

        do
        {
            float prevLen = float.MaxValue;
            int loopIdx = 0;

            for (int i = 0; i < tempPath.Count; i++)
            {
                var lastP = fixedPath.Last();
                var curP = tempPath[i];

                var len = GetLenght(lastP, curP);

                if (prevLen > len)
                {
                    prevLen = len;
                    loopIdx = i;
                }
            }

            fixedPath.Add(tempPath[loopIdx]);
            tempPath.Remove(tempPath[loopIdx]);
        } while (tempPath.Count != 0);

        for (int i = 1; i < fixedPath.Count; i++)
        {
            var v = fixedPath[i] - fixedPath[i - 1];
            v = new Vector3(v.x, v.y, v.z);
            directions.Add(v);
        }
        Path = directions;
    }

    private float GetLenght(Vector3 a, Vector3 b)
    {
        var xSub = (a.x - b.x);
        var ySub = (a.y - b.y);
        return (float)Math.Sqrt((xSub * xSub) + (ySub * ySub));
    }
}
