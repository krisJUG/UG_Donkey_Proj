using UnityEngine;

/// <summary>
/// Provides prefabs of additional element
/// </summary>
public class PoolObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objPrefabs;

    /// <summary>
    /// Gets prefabs
    /// </summary>
    /// <param name="typeOfObj"></param>
    /// <returns></returns>
    public GameObject GetObj(string typeOfObj)
    {
        foreach (var obj in _objPrefabs)
        {
            if (!obj.name.Equals(typeOfObj, System.StringComparison.OrdinalIgnoreCase))
                continue;

            var gameObj = Instantiate(obj);
            gameObj.name = typeOfObj;
            return gameObj;
        }

        return null;
    }
}
