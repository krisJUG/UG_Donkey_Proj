using UnityEngine;

/// <summary>
/// Provides options for game camera
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// Value of max X position
    /// </summary>
    private float xMax;

    /// <summary>
    /// Value of max Y position
    /// </summary>
    private float yMin;

    [SerializeField]
    private float _cameraSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetUserInput();
    }

    /// <summary>
    /// Moves camera
    /// </summary>
    private void GetUserInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _cameraSpeed * Time.deltaTime);
        }

        transform.position =
            new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin,0), -10);
    }

    internal void SetLimits(Vector3 maxTile)
    {
        var wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;
    }
}
