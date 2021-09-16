using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    [SerializeField] Transform _pointA, _pointB;
    [SerializeField]private bool _switch;
    [SerializeField]private float _speed = 5;

   
    private void Update()
    {
       if (_switch==false)
        {
            transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }
            
        if (_switch==true)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="PointA")
        {
            _switch = false;
        }
        if (other.tag=="PointB")
        {
            _switch = true;
        }
        if (other.tag == "Player")
        {
            Debug.Log("Player was here");
            other.transform.parent = gameObject.transform;
        }
    }
}
