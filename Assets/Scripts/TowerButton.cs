using UnityEngine;

/// <summary>
/// Provides prefab from tower button to the core
/// </summary>
public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _towerPref;

    /// <summary>
    /// Gets a tower prefabs
    /// </summary>
    public GameObject TowerPref => _towerPref;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
