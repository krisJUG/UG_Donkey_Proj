using Assets.Scripts;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manager provides core application options
/// </summary>
public class GameManger : SingletonInstance<GameManger>
{
    private TowerScript _selectedTower;

    /// <summary>
    /// Provides tower button
    /// </summary>
    public TowerButton ClickedTowerButton { get; private set; }

    /// <summary>
    /// Provides game pool
    /// </summary>
    public PoolObject Pool { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Gets tower from user
    /// </summary>
    /// <param name="towerBtn"></param>
    public void GetTower(TowerButton towerBtn)
    {
        ClickedTowerButton = towerBtn;
    }

    /// <summary>
    /// Invokes when tries to buy a tower .
    /// </summary>
    public void BuyTower()
    {
        ClickedTowerButton = null;
    }

    public void SelectTower(TowerScript tower)
    {
        _selectedTower = tower;
        _selectedTower.Select();
    }

    /// <summary>
    /// Invoces after a start wave button was clicked.
    /// </summary>
    public void StartWave()
    {
        StartCoroutine(WaveSpawn());
    }

    private void Awake()
    {
        Pool = GetComponent<PoolObject>();
    }

    /// <summary>
    /// Spawns a wave
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveSpawn()
    {
        var monsterIdx = 0;
        var monsterType = string.Empty;

        switch (monsterIdx)
        {
            case 0: monsterType = "Rat"; break;
        }

        Monster monster = Pool.GetObj(monsterType).GetComponent<Monster>();
        monster.Spawn();
        yield return new WaitForSeconds(2.5f);
    }
}
