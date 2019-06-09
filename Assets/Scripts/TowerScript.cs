using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private SpriteRenderer _mySprite;
    private Monster _target; 

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D sc = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        _mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_target);
    }

    public void Select()
    {
        _mySprite.enabled = !_mySprite.enabled;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (string.Equals(collision.tag, "Monster"))
        {
            _target = collision.GetComponent<Monster>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (string.Equals(collision.tag, "Monster"))
        {
            _target = collision.GetComponent<Monster>();
        }
    }
}

