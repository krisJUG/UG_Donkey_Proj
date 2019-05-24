using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Provides attributes and information of single map tile
/// </summary>
public class TileScript : MonoBehaviour
{
    private Vector3 _boundsSize => GetComponent<SpriteRenderer>().bounds.size;

    /// <summary>
    /// Gets a grid position
    /// </summary>
    public Point GridPosition { get; private set; }

    /// <summary>
    /// Gets a world position of this tile
    /// </summary>
    public Vector2 WorldPos
        => new Vector2(transform.position.x + _boundsSize.x / 2, transform.position.y - _boundsSize.y / 2);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var a = GetComponent<SpriteRenderer>().bounds.size;
    }

    /// <summary>
    /// Sets tiles
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="gridPosition"></param>
    /// <param name="wrtPos"></param>
    public void Setup(Transform parent, Point gridPosition, Vector3 wrtPos)
    {
        GridPosition = gridPosition;
        transform.position = wrtPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPosition, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManger.Instance.ClickedTowerButton != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    private void PlaceTower()
    {
        var tower = Instantiate(GameManger.Instance.ClickedTowerButton.TowerPref, transform.position, Quaternion.identity);
        tower.transform.SetParent(transform);
        GameManger.Instance.BuyTower();
    }
}
