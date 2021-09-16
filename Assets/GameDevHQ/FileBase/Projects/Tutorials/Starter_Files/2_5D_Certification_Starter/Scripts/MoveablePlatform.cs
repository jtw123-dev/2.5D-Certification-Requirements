using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    [SerializeField] Transform _pointA, _pointB;
    [SerializeField]private bool _switch;
    private float _speed = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="PointB")
        {
            _switch = true;
        }
        else if (other.tag=="PointA")
        {
            _switch = false;
        }
        if (other.tag=="Player")
        {
            Debug.Log("moving platform");
            other.transform.parent = gameObject.transform.parent;
        }
    }
    private void Update()
    {
        if (_switch == false)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (_switch==true)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
    }
}
