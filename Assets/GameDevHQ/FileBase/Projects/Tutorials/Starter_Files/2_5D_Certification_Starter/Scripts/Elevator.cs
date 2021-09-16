using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
   [SerializeField] private Vector3 _level1, _level2;
   [SerializeField]private bool _switchFloors;
    private float _speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_switchFloors==false)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Level1")
        {
            _switchFloors = false;
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            _speed = 0;
            StartCoroutine("WaitForElevator");
        }
        if (other.tag=="Level2")
        {
            Debug.Log("elevator");
            _speed = 0;
            _switchFloors = true;
            StartCoroutine("WaitForElevator");          
        }
        if (other.tag=="Player")
        {
            Debug.Log("Player");
            //other.transform.parent = this.gameObject.transform.parent;
        }
    }
    private IEnumerator WaitForElevator()
    {
        yield return new WaitForSeconds(5f);
        _speed = 5;

    }
}
