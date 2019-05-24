using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Provides attributes and information of monster
/// </summary>
public class Monster : MonoBehaviour
{
    //[SerializeField]
    private float _speed = 2F;

    private Vector3 _nextPos;
    private Vector3 _position;
    private List<Vector3> _path;

    private void Update()
    {
        Movement();
    }

    /// <summary>
    /// Creates an instance of monster
    /// </summary>
    public void Spawn()
    {
        _position = transform.position = LevelManager.Instance.RatPortal.transform.position;

        _path = LevelManager.Instance.Path.ToList();
        _nextPos = new Vector3(RF(transform.position.x) + _path[0].x, RF(transform.position.y) + _path[0].y, 0);
    }

    private void Movement()
    {
        if (_path.Count == 1)
            return;

        if (SamePosition(_nextPos, _position))
        {
            _path.RemoveAt(0);
            
            _nextPos = new Vector3(_nextPos.x + _path[0].x, _nextPos.y + _path[0].y, _nextPos.z + _path[0].z);
        }

        var toMoveV = _path[0] * Time.deltaTime * _speed;
        transform.Translate(toMoveV, Space.World);
        _position += transform.TransformDirection(toMoveV);
    }

    private static float RF(float value)
    {
        float truncated = (float)(Math.Truncate((double)value * 100.0) / 100.0);
        float rounded = (float)(Math.Round((double)value, 1));
        return rounded;
    }

    private Vector3 FixVector(Vector3 toFixV)
    {
        float posX = RF(toFixV.x);
        float posY = RF(toFixV.y);
        float posZ = RF(toFixV.z);
        return new Vector3(posX, posY, posZ);
    }

    private bool SamePosition(Vector3 a, Vector3 b)
    {
        var x = Math.Abs(RF(a.x)) - Math.Abs(RF(b.x));
        var y = Math.Abs(RF(a.y)) - Math.Abs(RF(b.y));
        var z = Math.Abs(RF(a.z)) - Math.Abs(RF(b.z));

        return CompareNumber(x) && CompareNumber(y) && CompareNumber(z);
    }

    private bool CompareNumber(float a)
    {
        return (a > -0.1F && a < 0.1F);
    }
}
